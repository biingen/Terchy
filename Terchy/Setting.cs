using jini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terchy
{
    public partial class Setting : Form
    {
        private string Config_Path = Application.StartupPath + "\\Config.ini";

        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            comboBox_serialPort1_portname.DataSource = System.IO.Ports.SerialPort.GetPortNames();
            comboBox_serialPort2_portname.DataSource = System.IO.Ports.SerialPort.GetPortNames();

            if (ini12.INIRead(Config_Path, "serialPort1", "Exist", "") == "1")
            {
                serialPort1_enable.Checked = true;
                comboBox_serialPort1_portname.Enabled = true;
                comboBox_serialPort1_baudrate.Enabled = true;
            }
            else
            {
                serialPort1_enable.Checked = false;
                comboBox_serialPort1_portname.Enabled = false;
                comboBox_serialPort1_baudrate.Enabled = false;
            }

            if (ini12.INIRead(Config_Path, "serialPort2", "Exist", "") == "1")
            {
                serialPort2_enable.Checked = true;
                comboBox_serialPort2_baudrate.Enabled = true;
                comboBox_serialPort2_portname.Enabled = true;
            }
            else
            {
                serialPort2_enable.Checked = false;
                comboBox_serialPort2_baudrate.Enabled = false;
                comboBox_serialPort2_portname.Enabled = false;
            }

            comboBox_serialPort1_baudrate.Text = ini12.INIRead(Config_Path, "serialPort1", "BaudRate", "");
            comboBox_serialPort1_portname.Text = ini12.INIRead(Config_Path, "serialPort1", "PortName", "");
            comboBox_serialPort2_baudrate.Text = ini12.INIRead(Config_Path, "serialPort2", "BaudRate", "");
            comboBox_serialPort2_portname.Text = ini12.INIRead(Config_Path, "serialPort2", "PortName", "");

            textBox_Schedule.Text = ini12.INIRead(Config_Path, "Schedule", "Path", "");
        }

        private void serialPort1_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort1_enable.Checked == true)
            {
                ini12.INIWrite(Config_Path, "serialPort1", "Exist", "1");
                comboBox_serialPort1_portname.Enabled = true;
                comboBox_serialPort1_baudrate.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Config_Path, "serialPort1", "Exist", "0");
                comboBox_serialPort1_portname.Enabled = false;
                comboBox_serialPort1_baudrate.Enabled = false;
            }
        }

        private void serialPort2_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort2_enable.Checked == true)
            {
                ini12.INIWrite(Config_Path, "serialPort2", "Exist", "1");
                comboBox_serialPort2_baudrate.Enabled = true;
                comboBox_serialPort2_portname.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Config_Path, "serialPort2", "Exist", "0");
                comboBox_serialPort2_baudrate.Enabled = false;
                comboBox_serialPort2_portname.Enabled = false;
            }
        }

        private void comboBox_serialPort1_portname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort1_enable.Checked == true)
            {
                if (ini12.INIRead(Config_Path, "AutoKit", "PortName", "") != comboBox_serialPort1_portname.Text.Trim() && comboBox_serialPort1_portname.Text.Trim() != comboBox_serialPort2_portname.Text.Trim())
                {
                    ini12.INIWrite(Config_Path, "serialPort1", "PortName", comboBox_serialPort1_portname.Text.Trim());
                    label_serialPort_status.Text = "serialPort1 portname already save.";
                }
                else
                {
                    label_serialPort_status.Text = "serialPort1 portname can't save to config.";
                }
            }
        }

        private void comboBox_serialPort2_portname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort2_enable.Checked == true)
            {
                if (ini12.INIRead(Config_Path, "AutoKit", "PortName", "") != comboBox_serialPort2_portname.Text.Trim() && comboBox_serialPort1_portname.Text.Trim() != comboBox_serialPort2_portname.Text.Trim())
                {
                    ini12.INIWrite(Config_Path, "serialPort2", "PortName", comboBox_serialPort2_portname.Text.Trim());
                    label_serialPort_status.Text = "serialPort2 portname already save.";
                }
                else
                {
                    label_serialPort_status.Text = "serialPort2 portname can't save to config.";
                }
            }
        }

        private void comboBox_serialPort1_baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Config_Path, "serialPort1", "BaudRate", comboBox_serialPort1_baudrate.Text.Trim());
        }

        private void comboBox_serialPort2_baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Config_Path, "serialPort2", "BaudRate", comboBox_serialPort2_baudrate.Text.Trim());
        }

        private void button_Schedule_Click(object sender, EventArgs e)
        {
            SchOpen1.Filter = "CSV files (*.csv)|*.CSV";
            SchOpen1.ShowDialog();
            if (SchOpen1.FileName == "SchOpen1")
                textBox_Schedule.Text = textBox_Schedule.Text;
            else
                textBox_Schedule.Text = SchOpen1.FileName;
        }

        private void textBox_Schedule_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_Schedule.Text.Trim()) == true)
            {
                ini12.INIWrite(Config_Path, "Schedule", "Path", textBox_Schedule.Text.Trim());
                ini12.INIWrite(Config_Path, "Schedule", "Exist", "1");
            }
            else
            {
                ini12.INIWrite(Config_Path, "Schedule", "Path", "");
                ini12.INIWrite(Config_Path, "Schedule", "Exist", "0");
            }
        }
    }
}
