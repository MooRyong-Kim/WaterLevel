#include <SoftwareSerial.h> //Software Serial Port
#include <EEPROM.h>
#include <SPI.h>
#include "WizFi250.h"

char ssid[] = "stratoit2";    // your network SSID (name)
char pass[] = "strato1010";          // your network password
int status = WL_IDLE_STATUS;       // the Wifi radio's status

char server[] = "192.168.0.2";

// Initialize the Ethernet client object
WiFiClient client;

// Pin Number - Water Measurement
#define IN_LOW 2
#define IN_HIGH 3
#define OUT_SIGNAL 9

union twobyte {
  uint32_t word;
  uint8_t  byte[2];
} timevalue;

// EEPROM Address
#define HIGH_WLEVEL 1
#define LOW_WLEVEL 2

// Debouncing
unsigned long lastDebounceTime = 0;  // the last time the output pin was toggled
unsigned long debounceDelay = 50;    // the debounce time; increase if the output flickers\

// Calibration
int buttonState;             // the current reading from the input pin
int lastButtonState = LOW;   // the previous reading from the input pin

int calSignal = 0;
int address = 0;
byte value;

// Water Measurement
unsigned long LastT = 0;
unsigned long Cycle = 500000;
int outSig = 0;
int past_SL = 0;
int past_SH = 0;
unsigned long startT = 0;
unsigned long endT = 0;

unsigned int clock_cnt = 0;

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

void printWifiStatus();

void setup() {
  // put your setup code here, to run once:
  //  Serial.begin(9600);
  Serial.begin(115200);

  WiFi.init();
  Serial.println("TEST");

  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("WiFi shield not present");
    // don't continue
    while (true);
  }
  Serial.println("You're connected to the network");

  printWifiStatus();
  //  Serial.print("Attempting to start AP ");
  //  Serial.println(ssid);
  //  status = WiFi.beginAP(ssid, 10, pass, ENC_TYPE_WPA2_PSK);
  // attempt to connect to WiFi network
  while ( status != WL_CONNECTED) {
    Serial.print("Attempting to connect to WPA SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network
    status = WiFi.begin(ssid, pass);
  }

  Serial.println("You're connected to the network");

  if (client.connect(server, 7777)) {
    Serial.println("Connected to server");
    // Make a HTTP request
    client.println("GET /asciilogo.txt HTTP/1.1");
    client.println("Host: ");
    client.println("Connection: close");
    client.println();
  }

  pinMode(IN_HIGH, INPUT_PULLUP);
  pinMode(IN_LOW, INPUT_PULLUP);
  pinMode(OUT_SIGNAL, OUTPUT);
  pinMode(13, OUTPUT);

  //  EEPROM.write(HH, 3);
  //  EEPROM.write(HL, 232);
  //  EEPROM.write(HD, 79);
  attachInterrupt(digitalPinToInterrupt(IN_LOW), lowUp, RISING);

  LastT = micros();
}

EEPROMAddr eAddr = Unknown;
String str;
bool mloof_flag = false;
int reconn_cnt = 0;
int test_cnt = 0;
void loop() {

  while (client.available()) {
    reconn_cnt = 0;
    //    char c = client.read();
    client.println("Hi");
    Serial.println(test_cnt++);
    delay(500);
    //    Serial.println(c);
  }

  // if the server's disconnected, stop the client
  if (!client.connected()) {
    Serial.println();
    Serial.println("Disconnecting from server...");
    client.stop();

    if (client.connect(server, 7777))
    {
      Serial.println("Reconnect Success");
    }
    else
    {
      reconn_cnt++;
      String str = "Try reconnecting" + String(reconn_cnt);
      Serial.println(str);
      delay(2000);
    }
  }
  /*
    DataManagement();

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

      String str = "20180611245959" + WaterLevel_Format(result_val) + "9999\r\nclock_cnt : " + String(clock_cnt);\
      if(!mloof_flag)
      {
        Serial.println(str);
      }
    //      Serial.println(clock_cnt);
      }
    }
    //*/
}

void printWifiStatus()
{
  // print the SSID of the network you're attached to
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your WiFi shield's IP address
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  // print the received signal strength
  long rssi = WiFi.RSSI();
  Serial.print("Signal strength (RSSI):");
  Serial.print(rssi);
  Serial.println(" dBm");
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
  TCCR1B = (0 << ICNC1) | (1 << ICES1) | (1 << CS10);
  TCCR1C = 0;
  TCNT1 = 0;

  // Interrupt setup
  // ICIE1: Input capture
  // TOIE1: Timer1 overflow
  TIFR1 = (1 << ICF1) | (1 << TOV1);    // clear pending
  TIMSK1 = (1 << ICIE1) | (0 << TOIE1); // and enable

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

  TIFR1 |= (1 << ICF1);
  //  TIMSK1 = (0<<ICIE1) | (0<<TOIE1); // and enable
  TIMSK1 = (0 << ICIE1); // and enable
  //  digitalWrite(13, 0);
}

void DataManagement()
{
  if (Serial.available())
  {
    char ch = toupper(Serial.read());
    int temp_val = 0;
    switch (ch)
    {
      case 'S':
        mloof_flag = true;
        Serial.println("[Loof Stop Water Level High Low Data Management Mode\r\nLOW : L\r\nHIGH : H\r\nRead : R\r\nWrite : W\r\nRestart(Go) : G]");
        break;
      case 'G':
        mloof_flag = false;
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

        while (!Serial.available())
        {

        }

        {
          String temp_str = Serial.readString();
          int temp_int = temp_str.toInt();
          if (temp_str.toInt() != 0)
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

String WaterLevel_Format(float wl)
{
  String str = "";
  if (wl > 100)
  {
    str = "100.00";
  }
  else if (wl < 100 && wl >= 10)
  {
    str = "0" + String(roundf(wl * 100.0) / 100.0);
  }
  else if (wl < 10 && wl >= 0)
  {
    str = "00" + String(roundf(wl * 100.0) / 100.0);
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
