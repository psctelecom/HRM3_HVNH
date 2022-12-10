namespace PSC_HRM.Module.Win.QuyTrinh
{
    partial class QuyTrinhBaseController
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.btnKetThuc = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            this.btnBatDau = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNotification = new DevExpress.XtraEditors.TextEdit();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.btnHelp = new DevExpress.XtraEditors.SimpleButton();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotification.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.mainPanel);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(418, 258);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Quy trình nghỉ hưu";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.btnKetThuc);
            this.mainPanel.Controls.Add(this.btnBatDau);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(2, 21);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(414, 203);
            this.mainPanel.TabIndex = 8;
            // 
            // btnKetThuc
            // 
            this.btnKetThuc.BackColor = System.Drawing.Color.White;
            this.btnKetThuc.Caption = "Kết thúc";
            this.btnKetThuc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKetThuc.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnKetThuc.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnKetThuc.Location = new System.Drawing.Point(6, 120);
            this.btnKetThuc.Name = "btnKetThuc";
            this.btnKetThuc.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnKetThuc.Radial = 20;
            this.btnKetThuc.Size = new System.Drawing.Size(69, 39);
            this.btnKetThuc.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem1.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Stop_32x32;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Stop_32x32;
            toolTipTitleItem1.Text = "Kết thúc quy trình";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Kết thúc quy trình cho năm học hiện tại để bắt đầu quy trình cho năm học mới.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.toolTipController1.SetSuperTip(this.btnKetThuc, superToolTip1);
            this.btnKetThuc.TabIndex = 2;
            this.btnKetThuc.Click += new System.EventHandler(this.btnKetThuc_Click);
            // 
            // btnBatDau
            // 
            this.btnBatDau.BackColor = System.Drawing.Color.White;
            this.btnBatDau.Caption = "Bắt đầu";
            this.btnBatDau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatDau.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnBatDau.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnBatDau.Location = new System.Drawing.Point(6, 75);
            this.btnBatDau.Name = "btnBatDau";
            this.btnBatDau.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnBatDau.Radial = 20;
            this.btnBatDau.Size = new System.Drawing.Size(69, 39);
            this.btnBatDau.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Normal;
            toolTipTitleItem2.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Start_32x32;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Start_32x32;
            toolTipTitleItem2.Text = "Bắt đầu quy trình";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Dùng để cấu hình quy trình theo năm học";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.toolTipController1.SetSuperTip(this.btnBatDau, superToolTip2);
            this.btnBatDau.TabIndex = 3;
            this.btnBatDau.Click += new System.EventHandler(this.btnBatDau_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtNotification);
            this.panelControl1.Controls.Add(this.btnOpen);
            this.panelControl1.Controls.Add(this.btnHelp);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 224);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(414, 32);
            this.panelControl1.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(55, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Thông báo:";
            // 
            // txtNotification
            // 
            this.txtNotification.EditValue = "";
            this.txtNotification.Location = new System.Drawing.Point(67, 7);
            this.txtNotification.Name = "txtNotification";
            this.txtNotification.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtNotification.Properties.Appearance.Options.UseForeColor = true;
            this.txtNotification.Properties.ReadOnly = true;
            this.txtNotification.Size = new System.Drawing.Size(285, 20);
            this.txtNotification.TabIndex = 5;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::PSC_HRM.Module.Win.Properties.Resources.Action_Open;
            this.btnOpen.Location = new System.Drawing.Point(358, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(24, 22);
            toolTipTitleItem3.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.Action_Open_32x32;
            toolTipTitleItem3.Appearance.Options.UseImage = true;
            toolTipTitleItem3.Image = global::PSC_HRM.Module.Win.Properties.Resources.Action_Open_32x32;
            toolTipTitleItem3.Text = "Xem dữ liệu";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Xem dữ liệu của quy trình hiện tại";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.btnOpen.SuperTip = superToolTip3;
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = global::PSC_HRM.Module.Win.Properties.Resources.help;
            this.btnHelp.Location = new System.Drawing.Point(385, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(24, 22);
            toolTipTitleItem4.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.help_32x32;
            toolTipTitleItem4.Appearance.Options.UseImage = true;
            toolTipTitleItem4.Image = global::PSC_HRM.Module.Win.Properties.Resources.help_32x32;
            toolTipTitleItem4.Text = "Trợ giúp";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "Mở file hướng dẫn sử dụng";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.btnHelp.SuperTip = superToolTip4;
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // QuyTrinhBaseController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "QuyTrinhBaseController";
            this.Size = new System.Drawing.Size(418, 258);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotification.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.SimpleButton btnHelp;
        protected DevExpress.XtraEditors.TextEdit txtNotification;
        protected DevExpress.Utils.ToolTipController toolTipController1;
        protected DevExpress.XtraEditors.PanelControl mainPanel;
        protected DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.GroupControl groupControl1;
        protected DevExpress.XtraEditors.SimpleButton btnOpen;
        protected CustomButton btnKetThuc;
        protected CustomButton btnBatDau;
    }
}
