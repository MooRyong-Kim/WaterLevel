namespace ArduinoSerialComm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.tb_Receive = new System.Windows.Forms.TextBox();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel4 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rg_ClientLIst = new DevExpress.XtraEditors.RadioGroup();
            this.btn_LowInfo = new System.Windows.Forms.Button();
            this.btn_HighInfo = new System.Windows.Forms.Button();
            this.btn_HighCal = new System.Windows.Forms.Button();
            this.btn_LowCal = new System.Windows.Forms.Button();
            this.btn_Go = new System.Windows.Forms.Button();
            this.btn_GraphClear = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.tb_Send = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tb_ReceiveCal = new System.Windows.Forms.TextBox();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel4.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dockPanel3.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_ClientLIst.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1056, 353);
            this.chartControl1.TabIndex = 0;
            // 
            // tb_Receive
            // 
            this.tb_Receive.Location = new System.Drawing.Point(12, 29);
            this.tb_Receive.Multiline = true;
            this.tb_Receive.Name = "tb_Receive";
            this.tb_Receive.ReadOnly = true;
            this.tb_Receive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Receive.Size = new System.Drawing.Size(687, 113);
            this.tb_Receive.TabIndex = 1;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel2,
            this.dockPanel4});
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1,
            this.dockPanel3});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dockPanel2.ID = new System.Guid("08a9cc27-a836-4d01-9770-9f5b11e0812f");
            this.dockPanel2.Location = new System.Drawing.Point(0, 200);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dockPanel2.SavedIndex = 1;
            this.dockPanel2.Size = new System.Drawing.Size(704, 200);
            this.dockPanel2.Text = "dockPanel2";
            this.dockPanel2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(696, 172);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // dockPanel4
            // 
            this.dockPanel4.Controls.Add(this.dockPanel4_Container);
            this.dockPanel4.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel4.ID = new System.Guid("31c8b694-ba8d-4e5c-ad4c-7cc741791eaf");
            this.dockPanel4.Location = new System.Drawing.Point(0, 400);
            this.dockPanel4.Name = "dockPanel4";
            this.dockPanel4.OriginalSize = new System.Drawing.Size(200, 41);
            this.dockPanel4.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel4.SavedIndex = 2;
            this.dockPanel4.Size = new System.Drawing.Size(704, 41);
            this.dockPanel4.Text = "dockPanel4";
            this.dockPanel4.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Location = new System.Drawing.Point(4, 24);
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            this.dockPanel4_Container.Size = new System.Drawing.Size(696, 13);
            this.dockPanel4_Container.TabIndex = 0;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dockPanel1.FloatSize = new System.Drawing.Size(200, 327);
            this.dockPanel1.FloatVertical = true;
            this.dockPanel1.ID = new System.Guid("60748281-c989-490c-a809-aa5c655bdff3");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 381);
            this.dockPanel1.Size = new System.Drawing.Size(1064, 381);
            this.dockPanel1.Text = "Chart";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.chartControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1056, 353);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel3.FloatVertical = true;
            this.dockPanel3.ID = new System.Guid("828da035-e9b5-4aa5-870f-37ef510ff726");
            this.dockPanel3.Location = new System.Drawing.Point(0, 408);
            this.dockPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.OriginalSize = new System.Drawing.Size(200, 273);
            this.dockPanel3.Size = new System.Drawing.Size(1064, 273);
            this.dockPanel3.Text = "Network Control";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.layoutControl1);
            this.dockPanel3_Container.Location = new System.Drawing.Point(4, 24);
            this.dockPanel3_Container.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(1056, 245);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tb_ReceiveCal);
            this.layoutControl1.Controls.Add(this.rg_ClientLIst);
            this.layoutControl1.Controls.Add(this.btn_LowInfo);
            this.layoutControl1.Controls.Add(this.btn_HighInfo);
            this.layoutControl1.Controls.Add(this.btn_HighCal);
            this.layoutControl1.Controls.Add(this.btn_LowCal);
            this.layoutControl1.Controls.Add(this.btn_Go);
            this.layoutControl1.Controls.Add(this.btn_GraphClear);
            this.layoutControl1.Controls.Add(this.btn_Stop);
            this.layoutControl1.Controls.Add(this.tb_Send);
            this.layoutControl1.Controls.Add(this.btn_Send);
            this.layoutControl1.Controls.Add(this.tb_Receive);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2561, 562, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1056, 245);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rg_ClientLIst
            // 
            this.rg_ClientLIst.Location = new System.Drawing.Point(187, 146);
            this.rg_ClientLIst.Name = "rg_ClientLIst";
            this.rg_ClientLIst.Size = new System.Drawing.Size(857, 26);
            this.rg_ClientLIst.StyleController = this.layoutControl1;
            this.rg_ClientLIst.TabIndex = 2;
            // 
            // btn_LowInfo
            // 
            this.btn_LowInfo.Location = new System.Drawing.Point(703, 176);
            this.btn_LowInfo.Name = "btn_LowInfo";
            this.btn_LowInfo.Size = new System.Drawing.Size(168, 33);
            this.btn_LowInfo.TabIndex = 2;
            this.btn_LowInfo.Text = "Low Info";
            this.btn_LowInfo.UseVisualStyleBackColor = true;
            this.btn_LowInfo.Click += new System.EventHandler(this.btn_LowInfo_Click);
            // 
            // btn_HighInfo
            // 
            this.btn_HighInfo.Location = new System.Drawing.Point(875, 176);
            this.btn_HighInfo.Name = "btn_HighInfo";
            this.btn_HighInfo.Size = new System.Drawing.Size(169, 33);
            this.btn_HighInfo.TabIndex = 2;
            this.btn_HighInfo.Text = "High Info";
            this.btn_HighInfo.UseVisualStyleBackColor = true;
            this.btn_HighInfo.Click += new System.EventHandler(this.btn_HighInfo_Click);
            // 
            // btn_HighCal
            // 
            this.btn_HighCal.Location = new System.Drawing.Point(530, 176);
            this.btn_HighCal.Name = "btn_HighCal";
            this.btn_HighCal.Size = new System.Drawing.Size(169, 33);
            this.btn_HighCal.TabIndex = 2;
            this.btn_HighCal.Text = "High Calibration";
            this.btn_HighCal.UseVisualStyleBackColor = true;
            this.btn_HighCal.Click += new System.EventHandler(this.btn_HighCal_Click);
            // 
            // btn_LowCal
            // 
            this.btn_LowCal.Location = new System.Drawing.Point(357, 176);
            this.btn_LowCal.Name = "btn_LowCal";
            this.btn_LowCal.Size = new System.Drawing.Size(169, 33);
            this.btn_LowCal.TabIndex = 2;
            this.btn_LowCal.Text = "Low Calibration";
            this.btn_LowCal.UseVisualStyleBackColor = true;
            this.btn_LowCal.Click += new System.EventHandler(this.btn_LowCal_Click);
            // 
            // btn_Go
            // 
            this.btn_Go.Location = new System.Drawing.Point(184, 176);
            this.btn_Go.Name = "btn_Go";
            this.btn_Go.Size = new System.Drawing.Size(169, 33);
            this.btn_Go.TabIndex = 2;
            this.btn_Go.Text = "Go";
            this.btn_Go.UseVisualStyleBackColor = true;
            this.btn_Go.Click += new System.EventHandler(this.btn_Go_Click);
            // 
            // btn_GraphClear
            // 
            this.btn_GraphClear.Location = new System.Drawing.Point(703, 213);
            this.btn_GraphClear.Name = "btn_GraphClear";
            this.btn_GraphClear.Size = new System.Drawing.Size(341, 20);
            this.btn_GraphClear.TabIndex = 8;
            this.btn_GraphClear.Text = "Graph Clear";
            this.btn_GraphClear.UseVisualStyleBackColor = true;
            this.btn_GraphClear.Click += new System.EventHandler(this.btn_GraphClear_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(12, 176);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(168, 33);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // tb_Send
            // 
            this.tb_Send.Location = new System.Drawing.Point(12, 213);
            this.tb_Send.Name = "tb_Send";
            this.tb_Send.Size = new System.Drawing.Size(341, 20);
            this.tb_Send.TabIndex = 21;
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(357, 213);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(342, 20);
            this.btn_Send.TabIndex = 2;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1056, 245);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tb_Receive;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(691, 134);
            this.layoutControlItem1.Text = "Network Msg";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(172, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tb_Send;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 201);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Send;
            this.layoutControlItem3.Location = new System.Drawing.Point(345, 201);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(346, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_GraphClear;
            this.layoutControlItem4.Location = new System.Drawing.Point(691, 201);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Stop;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 164);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(172, 37);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_Go;
            this.layoutControlItem6.Location = new System.Drawing.Point(172, 164);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(173, 37);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_LowCal;
            this.layoutControlItem7.Location = new System.Drawing.Point(345, 164);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(173, 37);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btn_HighCal;
            this.layoutControlItem8.Location = new System.Drawing.Point(518, 164);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(173, 37);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btn_LowInfo;
            this.layoutControlItem9.Location = new System.Drawing.Point(691, 164);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(172, 37);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btn_HighInfo;
            this.layoutControlItem10.Location = new System.Drawing.Point(863, 164);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(173, 37);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.rg_ClientLIst;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 134);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(1036, 30);
            this.layoutControlItem11.Text = "Client List";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(172, 14);
            // 
            // tb_ReceiveCal
            // 
            this.tb_ReceiveCal.Location = new System.Drawing.Point(703, 29);
            this.tb_ReceiveCal.Multiline = true;
            this.tb_ReceiveCal.Name = "tb_ReceiveCal";
            this.tb_ReceiveCal.ReadOnly = true;
            this.tb_ReceiveCal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_ReceiveCal.Size = new System.Drawing.Size(341, 113);
            this.tb_ReceiveCal.TabIndex = 1;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.tb_ReceiveCal;
            this.layoutControlItem12.Location = new System.Drawing.Point(691, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(345, 134);
            this.layoutControlItem12.Text = "Target Network Calibration Msg";
            this.layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(172, 14);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.dockPanel3);
            this.Controls.Add(this.dockPanel1);
            this.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Monitoring Server";
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel4.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel3.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rg_ClientLIst.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.TextBox tb_Receive;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel4;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
        private System.Windows.Forms.Button btn_GraphClear;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox tb_Send;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_HighCal;
        private System.Windows.Forms.Button btn_LowCal;
        private System.Windows.Forms.Button btn_Go;
        private System.Windows.Forms.Button btn_Stop;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private System.Windows.Forms.Button btn_HighInfo;
        private System.Windows.Forms.Button btn_LowInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.RadioGroup rg_ClientLIst;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private System.Windows.Forms.TextBox tb_ReceiveCal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
    }
}

