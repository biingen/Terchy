namespace Terchy
{
    partial class Setting
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
            this.label_serialPort_status = new System.Windows.Forms.Label();
            this.groupBox_serialPort2 = new System.Windows.Forms.GroupBox();
            this.serialPort2_enable = new System.Windows.Forms.CheckBox();
            this.comboBox_serialPort2_baudrate = new System.Windows.Forms.ComboBox();
            this.comboBox_serialPort2_portname = new System.Windows.Forms.ComboBox();
            this.label_serialPort2_baudrate = new System.Windows.Forms.Label();
            this.label_serialPort2_portname = new System.Windows.Forms.Label();
            this.groupBox_serialPort1 = new System.Windows.Forms.GroupBox();
            this.serialPort1_enable = new System.Windows.Forms.CheckBox();
            this.comboBox_serialPort1_baudrate = new System.Windows.Forms.ComboBox();
            this.comboBox_serialPort1_portname = new System.Windows.Forms.ComboBox();
            this.label_serialPort1_baudrate = new System.Windows.Forms.Label();
            this.label_serialPort1_portname = new System.Windows.Forms.Label();
            this.groupBox_serialPort2.SuspendLayout();
            this.groupBox_serialPort1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_serialPort_status
            // 
            this.label_serialPort_status.AutoSize = true;
            this.label_serialPort_status.Location = new System.Drawing.Point(7, 16);
            this.label_serialPort_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_serialPort_status.Name = "label_serialPort_status";
            this.label_serialPort_status.Size = new System.Drawing.Size(75, 12);
            this.label_serialPort_status.TabIndex = 71;
            this.label_serialPort_status.Text = "Comport status";
            // 
            // groupBox_serialPort2
            // 
            this.groupBox_serialPort2.Controls.Add(this.serialPort2_enable);
            this.groupBox_serialPort2.Controls.Add(this.comboBox_serialPort2_baudrate);
            this.groupBox_serialPort2.Controls.Add(this.comboBox_serialPort2_portname);
            this.groupBox_serialPort2.Controls.Add(this.label_serialPort2_baudrate);
            this.groupBox_serialPort2.Controls.Add(this.label_serialPort2_portname);
            this.groupBox_serialPort2.Location = new System.Drawing.Point(4, 163);
            this.groupBox_serialPort2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_serialPort2.Name = "groupBox_serialPort2";
            this.groupBox_serialPort2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_serialPort2.Size = new System.Drawing.Size(255, 119);
            this.groupBox_serialPort2.TabIndex = 70;
            this.groupBox_serialPort2.TabStop = false;
            this.groupBox_serialPort2.Text = "SerialPort2";
            // 
            // serialPort2_enable
            // 
            this.serialPort2_enable.AutoSize = true;
            this.serialPort2_enable.Location = new System.Drawing.Point(13, 26);
            this.serialPort2_enable.Margin = new System.Windows.Forms.Padding(4);
            this.serialPort2_enable.Name = "serialPort2_enable";
            this.serialPort2_enable.Size = new System.Drawing.Size(56, 16);
            this.serialPort2_enable.TabIndex = 6;
            this.serialPort2_enable.Text = "Enable";
            this.serialPort2_enable.UseVisualStyleBackColor = true;
            this.serialPort2_enable.CheckedChanged += new System.EventHandler(this.serialPort2_enable_CheckedChanged);
            // 
            // comboBox_serialPort2_baudrate
            // 
            this.comboBox_serialPort2_baudrate.FormattingEnabled = true;
            this.comboBox_serialPort2_baudrate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.comboBox_serialPort2_baudrate.Location = new System.Drawing.Point(81, 86);
            this.comboBox_serialPort2_baudrate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_serialPort2_baudrate.Name = "comboBox_serialPort2_baudrate";
            this.comboBox_serialPort2_baudrate.Size = new System.Drawing.Size(160, 20);
            this.comboBox_serialPort2_baudrate.TabIndex = 5;
            this.comboBox_serialPort2_baudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_serialPort2_baudrate_SelectedIndexChanged);
            // 
            // comboBox_serialPort2_portname
            // 
            this.comboBox_serialPort2_portname.FormattingEnabled = true;
            this.comboBox_serialPort2_portname.Location = new System.Drawing.Point(81, 54);
            this.comboBox_serialPort2_portname.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_serialPort2_portname.Name = "comboBox_serialPort2_portname";
            this.comboBox_serialPort2_portname.Size = new System.Drawing.Size(160, 20);
            this.comboBox_serialPort2_portname.TabIndex = 4;
            this.comboBox_serialPort2_portname.SelectedIndexChanged += new System.EventHandler(this.comboBox_serialPort2_portname_SelectedIndexChanged);
            // 
            // label_serialPort2_baudrate
            // 
            this.label_serialPort2_baudrate.AutoSize = true;
            this.label_serialPort2_baudrate.Location = new System.Drawing.Point(10, 89);
            this.label_serialPort2_baudrate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_serialPort2_baudrate.Name = "label_serialPort2_baudrate";
            this.label_serialPort2_baudrate.Size = new System.Drawing.Size(50, 12);
            this.label_serialPort2_baudrate.TabIndex = 3;
            this.label_serialPort2_baudrate.Text = "Baudrate:";
            // 
            // label_serialPort2_portname
            // 
            this.label_serialPort2_portname.AutoSize = true;
            this.label_serialPort2_portname.Location = new System.Drawing.Point(8, 57);
            this.label_serialPort2_portname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_serialPort2_portname.Name = "label_serialPort2_portname";
            this.label_serialPort2_portname.Size = new System.Drawing.Size(52, 12);
            this.label_serialPort2_portname.TabIndex = 2;
            this.label_serialPort2_portname.Text = "Portname:";
            // 
            // groupBox_serialPort1
            // 
            this.groupBox_serialPort1.Controls.Add(this.serialPort1_enable);
            this.groupBox_serialPort1.Controls.Add(this.comboBox_serialPort1_baudrate);
            this.groupBox_serialPort1.Controls.Add(this.comboBox_serialPort1_portname);
            this.groupBox_serialPort1.Controls.Add(this.label_serialPort1_baudrate);
            this.groupBox_serialPort1.Controls.Add(this.label_serialPort1_portname);
            this.groupBox_serialPort1.Location = new System.Drawing.Point(4, 36);
            this.groupBox_serialPort1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_serialPort1.Name = "groupBox_serialPort1";
            this.groupBox_serialPort1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_serialPort1.Size = new System.Drawing.Size(255, 119);
            this.groupBox_serialPort1.TabIndex = 69;
            this.groupBox_serialPort1.TabStop = false;
            this.groupBox_serialPort1.Text = "SerialPort1";
            // 
            // serialPort1_enable
            // 
            this.serialPort1_enable.AutoSize = true;
            this.serialPort1_enable.Location = new System.Drawing.Point(13, 26);
            this.serialPort1_enable.Margin = new System.Windows.Forms.Padding(4);
            this.serialPort1_enable.Name = "serialPort1_enable";
            this.serialPort1_enable.Size = new System.Drawing.Size(56, 16);
            this.serialPort1_enable.TabIndex = 6;
            this.serialPort1_enable.Text = "Enable";
            this.serialPort1_enable.UseVisualStyleBackColor = true;
            this.serialPort1_enable.CheckedChanged += new System.EventHandler(this.serialPort1_enable_CheckedChanged);
            // 
            // comboBox_serialPort1_baudrate
            // 
            this.comboBox_serialPort1_baudrate.FormattingEnabled = true;
            this.comboBox_serialPort1_baudrate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.comboBox_serialPort1_baudrate.Location = new System.Drawing.Point(84, 86);
            this.comboBox_serialPort1_baudrate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_serialPort1_baudrate.Name = "comboBox_serialPort1_baudrate";
            this.comboBox_serialPort1_baudrate.Size = new System.Drawing.Size(160, 20);
            this.comboBox_serialPort1_baudrate.TabIndex = 5;
            this.comboBox_serialPort1_baudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_serialPort1_baudrate_SelectedIndexChanged);
            // 
            // comboBox_serialPort1_portname
            // 
            this.comboBox_serialPort1_portname.FormattingEnabled = true;
            this.comboBox_serialPort1_portname.Location = new System.Drawing.Point(84, 53);
            this.comboBox_serialPort1_portname.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_serialPort1_portname.Name = "comboBox_serialPort1_portname";
            this.comboBox_serialPort1_portname.Size = new System.Drawing.Size(160, 20);
            this.comboBox_serialPort1_portname.TabIndex = 4;
            this.comboBox_serialPort1_portname.SelectedIndexChanged += new System.EventHandler(this.comboBox_serialPort1_portname_SelectedIndexChanged);
            // 
            // label_serialPort1_baudrate
            // 
            this.label_serialPort1_baudrate.AutoSize = true;
            this.label_serialPort1_baudrate.Location = new System.Drawing.Point(13, 88);
            this.label_serialPort1_baudrate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_serialPort1_baudrate.Name = "label_serialPort1_baudrate";
            this.label_serialPort1_baudrate.Size = new System.Drawing.Size(50, 12);
            this.label_serialPort1_baudrate.TabIndex = 3;
            this.label_serialPort1_baudrate.Text = "Baudrate:";
            // 
            // label_serialPort1_portname
            // 
            this.label_serialPort1_portname.AutoSize = true;
            this.label_serialPort1_portname.Location = new System.Drawing.Point(10, 56);
            this.label_serialPort1_portname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_serialPort1_portname.Name = "label_serialPort1_portname";
            this.label_serialPort1_portname.Size = new System.Drawing.Size(52, 12);
            this.label_serialPort1_portname.TabIndex = 2;
            this.label_serialPort1_portname.Text = "Portname:";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 290);
            this.Controls.Add(this.label_serialPort_status);
            this.Controls.Add(this.groupBox_serialPort2);
            this.Controls.Add(this.groupBox_serialPort1);
            this.Name = "Setting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.groupBox_serialPort2.ResumeLayout(false);
            this.groupBox_serialPort2.PerformLayout();
            this.groupBox_serialPort1.ResumeLayout(false);
            this.groupBox_serialPort1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_serialPort_status;
        private System.Windows.Forms.GroupBox groupBox_serialPort2;
        private System.Windows.Forms.CheckBox serialPort2_enable;
        private System.Windows.Forms.ComboBox comboBox_serialPort2_baudrate;
        private System.Windows.Forms.ComboBox comboBox_serialPort2_portname;
        private System.Windows.Forms.Label label_serialPort2_baudrate;
        private System.Windows.Forms.Label label_serialPort2_portname;
        private System.Windows.Forms.GroupBox groupBox_serialPort1;
        private System.Windows.Forms.CheckBox serialPort1_enable;
        private System.Windows.Forms.ComboBox comboBox_serialPort1_baudrate;
        private System.Windows.Forms.ComboBox comboBox_serialPort1_portname;
        private System.Windows.Forms.Label label_serialPort1_baudrate;
        private System.Windows.Forms.Label label_serialPort1_portname;
    }
}