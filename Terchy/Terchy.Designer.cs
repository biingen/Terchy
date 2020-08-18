namespace Terchy
{
    partial class Terchy
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
            this.button_setting = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.button_start = new System.Windows.Forms.Button();
            this.label_temperatureRec = new System.Windows.Forms.Label();
            this.textBox_chamberTemp = new System.Windows.Forms.TextBox();
            this.button_set = new System.Windows.Forms.Button();
            this.textBox_chamberHum = new System.Windows.Forms.TextBox();
            this.label_temperatureChamber = new System.Windows.Forms.Label();
            this.label_hum = new System.Windows.Forms.Label();
            this.label_slope = new System.Windows.Forms.Label();
            this.textBox_chamberSlope = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_setting
            // 
            this.button_setting.Location = new System.Drawing.Point(197, 12);
            this.button_setting.Name = "button_setting";
            this.button_setting.Size = new System.Drawing.Size(75, 23);
            this.button_setting.TabIndex = 0;
            this.button_setting.Text = "Setting";
            this.button_setting.UseVisualStyleBackColor = true;
            this.button_setting.Click += new System.EventHandler(this.button_setting_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(197, 98);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 1;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label_temperatureRec
            // 
            this.label_temperatureRec.AutoSize = true;
            this.label_temperatureRec.Location = new System.Drawing.Point(12, 17);
            this.label_temperatureRec.Name = "label_temperatureRec";
            this.label_temperatureRec.Size = new System.Drawing.Size(116, 12);
            this.label_temperatureRec.TabIndex = 2;
            this.label_temperatureRec.Text = "Temperature Recorder: ";
            // 
            // textBox_chamberTemp
            // 
            this.textBox_chamberTemp.Location = new System.Drawing.Point(91, 41);
            this.textBox_chamberTemp.Name = "textBox_chamberTemp";
            this.textBox_chamberTemp.Size = new System.Drawing.Size(100, 22);
            this.textBox_chamberTemp.TabIndex = 3;
            // 
            // button_set
            // 
            this.button_set.Location = new System.Drawing.Point(197, 41);
            this.button_set.Name = "button_set";
            this.button_set.Size = new System.Drawing.Size(75, 51);
            this.button_set.TabIndex = 4;
            this.button_set.Text = "Set";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.button_set_Click);
            // 
            // textBox_chamberHum
            // 
            this.textBox_chamberHum.Location = new System.Drawing.Point(91, 70);
            this.textBox_chamberHum.Name = "textBox_chamberHum";
            this.textBox_chamberHum.Size = new System.Drawing.Size(100, 22);
            this.textBox_chamberHum.TabIndex = 5;
            // 
            // label_temperatureChamber
            // 
            this.label_temperatureChamber.AutoSize = true;
            this.label_temperatureChamber.Location = new System.Drawing.Point(12, 44);
            this.label_temperatureChamber.Name = "label_temperatureChamber";
            this.label_temperatureChamber.Size = new System.Drawing.Size(70, 12);
            this.label_temperatureChamber.TabIndex = 6;
            this.label_temperatureChamber.Text = "Temperature: ";
            // 
            // label_hum
            // 
            this.label_hum.AutoSize = true;
            this.label_hum.Location = new System.Drawing.Point(12, 73);
            this.label_hum.Name = "label_hum";
            this.label_hum.Size = new System.Drawing.Size(55, 12);
            this.label_hum.TabIndex = 7;
            this.label_hum.Text = "Humidity: ";
            // 
            // label_slope
            // 
            this.label_slope.AutoSize = true;
            this.label_slope.Location = new System.Drawing.Point(12, 101);
            this.label_slope.Name = "label_slope";
            this.label_slope.Size = new System.Drawing.Size(37, 12);
            this.label_slope.TabIndex = 8;
            this.label_slope.Text = "Slope: ";
            // 
            // textBox_chamberSlope
            // 
            this.textBox_chamberSlope.Location = new System.Drawing.Point(91, 98);
            this.textBox_chamberSlope.Name = "textBox_chamberSlope";
            this.textBox_chamberSlope.Size = new System.Drawing.Size(100, 22);
            this.textBox_chamberSlope.TabIndex = 9;
            // 
            // Terchy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBox_chamberSlope);
            this.Controls.Add(this.label_slope);
            this.Controls.Add(this.label_hum);
            this.Controls.Add(this.label_temperatureChamber);
            this.Controls.Add(this.textBox_chamberHum);
            this.Controls.Add(this.button_set);
            this.Controls.Add(this.textBox_chamberTemp);
            this.Controls.Add(this.label_temperatureRec);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_setting);
            this.Name = "Terchy";
            this.Text = "Terchy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_setting;
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label_temperatureRec;
        private System.Windows.Forms.TextBox textBox_chamberTemp;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.TextBox textBox_chamberHum;
        private System.Windows.Forms.Label label_temperatureChamber;
        private System.Windows.Forms.Label label_hum;
        private System.Windows.Forms.Label label_slope;
        private System.Windows.Forms.TextBox textBox_chamberSlope;
    }
}

