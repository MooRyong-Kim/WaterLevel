namespace ArduinoSerialComm
{
    partial class UC_NetworkMsgPage
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tc_NetworkMsg = new DevExpress.XtraTab.XtraTabControl();
            this.rg_ClientLIst = new DevExpress.XtraEditors.RadioGroup();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lc_ClientList = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.button1 = new System.Windows.Forms.Button();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tc_NetworkMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_ClientLIst.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_ClientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.button1);
            this.layoutControl1.Controls.Add(this.tc_NetworkMsg);
            this.layoutControl1.Controls.Add(this.rg_ClientLIst);
            this.layoutControl1.Controls.Add(this.richTextBox2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2420, 302, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(720, 480);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tc_NetworkMsg
            // 
            this.tc_NetworkMsg.Location = new System.Drawing.Point(12, 29);
            this.tc_NetworkMsg.Name = "tc_NetworkMsg";
            this.tc_NetworkMsg.Size = new System.Drawing.Size(346, 381);
            this.tc_NetworkMsg.TabIndex = 5;
            // 
            // rg_ClientLIst
            // 
            this.rg_ClientLIst.Location = new System.Drawing.Point(187, 414);
            this.rg_ClientLIst.Name = "rg_ClientLIst";
            this.rg_ClientLIst.Size = new System.Drawing.Size(521, 30);
            this.rg_ClientLIst.StyleController = this.layoutControl1;
            this.rg_ClientLIst.TabIndex = 4;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(362, 29);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(346, 381);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.lc_ClientList,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(720, 480);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.richTextBox2;
            this.layoutControlItem2.Location = new System.Drawing.Point(350, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(350, 402);
            this.layoutControlItem2.Text = "Target Network Calibration Msg";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(172, 14);
            // 
            // lc_ClientList
            // 
            this.lc_ClientList.Control = this.rg_ClientLIst;
            this.lc_ClientList.Location = new System.Drawing.Point(0, 402);
            this.lc_ClientList.Name = "lc_ClientList";
            this.lc_ClientList.Size = new System.Drawing.Size(700, 34);
            this.lc_ClientList.Text = "Client List";
            this.lc_ClientList.TextSize = new System.Drawing.Size(172, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tc_NetworkMsg;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(350, 402);
            this.layoutControlItem3.Text = "Network Msg";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(172, 14);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 448);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(696, 20);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.button1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 436);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(700, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // UC_NetworkMsgPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UC_NetworkMsgPage";
            this.Size = new System.Drawing.Size(720, 480);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tc_NetworkMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_ClientLIst.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_ClientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.RadioGroup rg_ClientLIst;
        private DevExpress.XtraLayout.LayoutControlItem lc_ClientList;
        private DevExpress.XtraTab.XtraTabControl tc_NetworkMsg;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
