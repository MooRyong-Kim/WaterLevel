#include <SoftwareSerial.h> //Software Serial Port
#include <EEPROM.h>
#include <Time.h>
#include <DS1307RTC.h>

// Pin Number - Water Measurement
#define IN_LOW 2
#define IN_HIGH 3
#define OUT_SIGNAL 9

// Water Measurement
unsigned long LastT = 0;
unsigned long Cycle = 500000;
int outSig = 0;
unsigned long startT = 0;
unsigned long endT = 0;
unsigned int clock_cnt = 0;

union twobyte {
  uint32_t word;
  uint8_t  byte[2];
} timevalue;

// EEPROM Address
enum EEPROMAddr
{
  Unknown = 0, 
  LH = 1,
  LL = 2,
  LD = 3,
  HH = 4,
  HL = 5,
  HD = 6,
  ZH = 100,
  ZL = 101
};

// ID(0~9999)
String ID = "1234";

bool Test_flag = true;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  
  pinMode(IN_HIGH, INPUT_PULLUP);
  pinMode(IN_LOW, INPUT_PULLUP);
  pinMode(OUT_SIGNAL, OUTPUT);
  pinMode(13, OUTPUT);
  
//  EEPROM.write(HH, 3);
//  EEPROM.write(HL, 232);
//  EEPROM.write(HD, 79);
  attachInterrupt(digitalPinToInterrupt(IN_LOW), lowUp, RISING);

//  String str = "[ID : " + ID + "]";
//  Serial.println(str);

  LastT = micros();
  
  // (hour, minute, second, day, month, year)
  setTime(12,59,50,19,6,2018);
}

EEPROMAddr eAddr = Unknown;
String str;
bool mloof_flag = false;
void loop() {
//  DataManagement();
  DataManagement_Improve();

  unsigned long nowT = micros();
  if ((nowT - LastT) > Cycle)
  {
    outSig = !outSig;

    digitalWrite(13, outSig);
    digitalWrite(OUT_SIGNAL, outSig);
    LastT = nowT;
    if(outSig == 0)
    {
    float result_val = ConvertToDepth(clock_cnt);
  
    if(!mloof_flag)
    {
      String str = "";

      if(Test_flag)
      {
//        int random_id = random(1000, 9999);
        int random_id = 5678;
        ID = (String)random_id;
  
        int test_num = random(0, 100);
        test_num = test_num + 10;
        str = FullDate() + WaterLevel_Format(test_num) + ID + "\r\nclock_cnt : " + String(clock_cnt);
      }
      else
      {
        str = FullDate() + WaterLevel_Format(result_val) + ID + "\r\nclock_cnt : " + String(clock_cnt);
      }
      
      Serial.println(str);
    }
//      Serial.println(clock_cnt);
    }
  }  
}

void lowUp() {
//  Serial.println("Low RISING");
//  digitalWrite(13, 1);
  initTimer();
}

void highUp() {
//  Serial.println("High RISING");
  endT = micros() - startT;
  double Capacitance = 0;
  Capacitance = endT * 1000 / 390.0;
  startT = 0;
  endT = 0;
}

void initTimer(void) {
//  Serial.println("Timer set");

  // Input Capture setup
  // ICNC1: Enable Input Capture Noise Canceler
  // ICES1: =1 for trigger on rising edge
  // CS10: =1 set prescaler to 1x system clock (F_CPU)
  TCCR1A = 0;
  TCCR1B = (0<<ICNC1) | (1<<ICES1) | (1<<CS10);
  TCCR1C = 0;
  TCNT1 = 0;
  
  // Interrupt setup
  // ICIE1: Input capture
  // TOIE1: Timer1 overflow
  TIFR1 = (1<<ICF1) | (1<<TOV1);        // clear pending
  TIMSK1 = (1<<ICIE1) | (0<<TOIE1); // and enable

  pinMode(8, INPUT);
}

ISR(TIMER1_CAPT_vect) {
  clock_cnt = 0;

  timevalue.byte[0] = ICR1L;        // grab captured timer value
  timevalue.byte[1] = ICR1H;        // grab captured timer value

  clock_cnt = timevalue.byte[1] << 8;
  clock_cnt += timevalue.byte[0];

//  Serial.println(timevalue.byte[1]);
//  Serial.println(timevalue.byte[0]);
//  Serial.println();
  
  TIFR1 |= (1<<ICF1);
//  TIMSK1 = (0<<ICIE1) | (0<<TOIE1); // and enable
  TIMSK1 = (0<<ICIE1); // and enable
//  digitalWrite(13, 0);
}

void DataManagement()
{
  if(Serial.available())
  {
    char ch = toupper(Serial.read());
    int temp_val = 0;
    switch(ch)
    {
      case 'S':
        mloof_flag = true;
        Serial.println("[Loof Stop Water Level High Low Data Management Mode\r\nLOW : L\r\nHIGH : H\r\nRead : R\r\nWrite : W\r\nRestart(Go) : G]");
//        digitalWrite(OUT_SIGNAL, 1);
        break;
      case 'G':
        mloof_flag = false;
//        digitalWrite(OUT_SIGNAL, 0);
        break;
      case 'L':
        str = "[Low Value ";
        Serial.println("[Read or Write? : ]");
        eAddr = LH;
        break;
      case 'H':
        str = "[High Value ";
        Serial.println("[Read or Write? : ]");
        eAddr = HH;
        break;
      case 'W':
        EEPROM.write(eAddr, int(timevalue.byte[1]));
        EEPROM.write(eAddr + 1, int(timevalue.byte[0]));
        temp_val = Converti8To16(int(timevalue.byte[1]), int(timevalue.byte[0]));
        
        str += "[Write : " + String(temp_val) + "]";
        Serial.println(str);
        Serial.println("[Please Input Depth : ]");
        
        while(!Serial.available())
        {
          
        }

        {
          String temp_str = Serial.readString();
          int temp_int = temp_str.toInt();
          if(temp_str.toInt() != 0)
          {
            EEPROM.write(eAddr + 2, temp_int);
            String str = "[Input Depth is " + String(temp_int) + "]";
            Serial.println(str);
          }
          else
          {
            Serial.print("[Input Depth is wrong]");
          }
        }
        
        str = "";
        eAddr = Unknown;
        break;
      case 'R':
        temp_val = Converti8To16(EEPROM.read(eAddr), EEPROM.read(eAddr + 1));
        
        str += "Read : " + String(temp_val) + " | Depth : " + EEPROM.read(eAddr + 2) + "]";
        Serial.println(str);
//        Serial.println(EEPROM.read(eAddr + 2));
        str = "";
        eAddr = Unknown;
        break;
      case 'Z':
        str = "[Zero Value ";
        Serial.println("[Read/Write or Clear? : ]");
        eAddr = ZH;
        break;
      case 'C':
        EEPROM.write(ZH, 0);
        EEPROM.write(ZL, 0);
        
        {
          float result_val = ConvertToDepth(clock_cnt);
//          Serial.print("clock_cnt : ");
//          Serial.println(clock_cnt);
//          Serial.print("result : ");
//          Serial.print(result_val);
//          Serial.println(" cm");
        }

        str += "Clear]";
//        Serial.println(clock_cnt);
//        Serial.println(str);
        eAddr = Unknown;
        str = "";
        break;
      default:
        str = "[Wrong Input]";
        str = "";
        break;
    }
    
//      Serial.print(clock_cnt);
//      EEPROM.write(address, int(timevalue.byte[1]));
//      EEPROM.write(address + 1, int(timevalue.byte[0]));
//      Serial.println(" Finish");
//      Serial.print("Input Depth : ");
  }
}

void WriteCalValue(int eAdress)
{
  String str = "";
  int temp_val = 0;
  EEPROM.write(eAdress, int(timevalue.byte[1]));
  EEPROM.write(eAdress + 1, int(timevalue.byte[0]));
  temp_val = Converti8To16(int(timevalue.byte[1]), int(timevalue.byte[0]));
  
  str += "[Low Write : " + String(temp_val) + "]";
  Serial.println(str);
  Serial.println("[Please Input Depth : ]");

  WaitRead();
  
  {
    String temp_str = Serial.readString();
    int temp_int = temp_str.toInt();
    if(temp_str.toInt() != 0)
    {
      EEPROM.write(eAdress + 2, temp_int);
      String str = "[Input Depth is " + String(temp_int) + "]";
      Serial.println(str);
    }
    else
    {
      Serial.print("[Input Depth is wrong]");
    }
  }
}

void WaitRead()
{
  while(!Serial.available())
  {
    
  }
}

void DataManagement_Improve()
{
  if(Serial.available())
  {
    char ch = toupper(Serial.read());
    int temp_val = 0;

    // G : Go(Restart Send Water Level)
    // L : Low(Shallowest) Data Read
    // H : High(Deepest) Data Read
    // M : Most Shallow
    // D : Deepest
    switch(ch)
    {
      case 'S':
        mloof_flag = true;
        Serial.println("[Loof Stop Water Level High Low Data Management Mode]");
        break;
      case 'G':
        mloof_flag = false;
        Serial.println("[Management Mode Finish, Loof Restart]");
        break;
      case 'L':
        eAddr = LH;

        temp_val = Converti8To16(EEPROM.read(eAddr), EEPROM.read(eAddr + 1));
        str += "[Low Value Read : " + String(temp_val) + " | Depth : " + EEPROM.read(eAddr + 2) + "]";
        Serial.println(str);

        str = "";
        eAddr = Unknown;
        break;
      case 'H':
        eAddr = HH;

        temp_val = Converti8To16(EEPROM.read(eAddr), EEPROM.read(eAddr + 1));
        str += "[High Value Read : " + String(temp_val) + " | Depth : " + EEPROM.read(eAddr + 2) + "]";
        Serial.println(str);

        str = "";
        eAddr = Unknown;
        break;
      case 'M':
        eAddr = LH;
        WriteCalValue(eAddr);
        str = "";
        eAddr = Unknown;
        break;
      case 'D':
        eAddr = HH;
        WriteCalValue(eAddr);
        str = "";
        eAddr = Unknown;
        break;
      default:
        str = "[Wrong Input]";
        Serial.println(str);
        str = "";
        break;
    }
  }
}

String FullDate()
{
  String str = String(year()) + DateFormat(month()) + DateFormat(day())
  + DateFormat(hour()) + DateFormat(minute()) + DateFormat(second());
  
  return str;
}

String DateFormat(int num)
{
  String str = "";
  if (num < 10)
  {
    return str = "0" + String(num);
  }

  return String(num);
}

String WaterLevel_Format(float wl)
{
  String str = "";
  if(wl > 100)
  {
    str = "100.00";
  }
  else if(wl < 100 && wl >= 10)
  {
    str = "0" + String(roundf(wl*100.0)/100.0);
  }
  else if(wl < 10 && wl >= 0)
  {
    str = "00" + String(roundf(wl*100.0)/100.0);
  }
  else
  {
    str = "000.00";
  }
  
  return str;
}

int Converti8To16(int high, int low)
{
  int temp = high << 8;
  temp += low;
  return temp;
}

float ConvertToTime(int clockCnt)
{
  return clockCnt * 62.5;
}

float ConvertToCapacity(int clockCnt)
{
  float T = ConvertToTime(clockCnt);
  int low = EEPROM.read(1) << 8;
  low += EEPROM.read(2);
  int high = EEPROM.read(3) << 8;
  high += EEPROM.read(4);

  float zero = EEPROM.read(100);
  zero +=  float(EEPROM.read(101)) / 100;

  float result = (T * ConvertToTime(high) / (ConvertToTime(high - low))) + ConvertToTime(zero);
  
  return result;
}

float ConvertToDepth(int clockCnt)
{
  int low_Time = Converti8To16(EEPROM.read(LH), EEPROM.read(LL));
  int high_Time = Converti8To16(EEPROM.read(HH), EEPROM.read(HL));
//  int zero = Converti8To16(EEPROM.read(ZH), EEPROM.read(ZL));
  int low_Depth = EEPROM.read(LD);
  int high_Depth = EEPROM.read(HD);

  float slope = float(high_Depth - low_Depth) / float(high_Time - low_Time);
  float result = high_Depth - (slope * (high_Time - clockCnt));
//  Serial.println(result);
  
  return result;
}
