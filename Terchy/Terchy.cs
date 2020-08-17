using jini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terchy
{
    public partial class Terchy : Form
    {
        private string Config_Path = Application.StartupPath + "\\Config.ini";
        string serialPort1_text, serialPort2_text;
        bool start_button;

        const int byteMessage_max_Hex = 16;
        const int byteMessage_max_Ascii = 256;

        byte[] byteMessage_A = new byte[Math.Max(byteMessage_max_Ascii, byteMessage_max_Hex)];
        int byteMessage_length_A = 0;
        byte[] byteMessage_B = new byte[Math.Max(byteMessage_max_Ascii, byteMessage_max_Hex)];
        int byteMessage_length_B = 0;

        const int byteTemperature_max = 64;
        byte[] byteTemperature = new byte[byteTemperature_max];
        int byteTemperature_length = 0;
        double currentTemperature = 0;

        public Terchy()
        {
            InitializeComponent();
        }

        //  開啟SerialPort
        protected void Open_serialPort1()
        {
            try
            {
                if (serialPort1.IsOpen == false)
                {
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.PortName = ini12.INIRead(Config_Path, "serialPort1", "PortName", "");
                    serialPort1.BaudRate = int.Parse((ini12.INIRead(Config_Path, "serialPort1", "BaudRate", "")));
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = (Parity)0;
                    serialPort1.ReceivedBytesThreshold = 1;
                    serialPort1.Open();
                }
            }
            catch (Exception Ex)
            {
                ini12.INIWrite(Config_Path, "serialPort1", "Exist", "0");
                MessageBox.Show(Ex.Message.ToString(), "SerialPort1 Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  關閉SerialPort
        protected void Close_serialPort1()
        {
            serialPort1.Dispose();
            serialPort1.Close();
        }

        //  SerialPort分析
        private void serialPort1_analysis()
        {
            while (serialPort1.IsOpen == true)
            {
                int data_to_read = serialPort1.BytesToRead;
                if (data_to_read > 0)
                {
                    byte[] dataset = new byte[data_to_read];
                    serialPort1.Read(dataset, 0, data_to_read);

                    for (int index = 0; index < data_to_read; index++)
                    {
                        byte input_ch = dataset[index];
                        serialPort1_recorder(input_ch);
                        btm4208sd_temperature(input_ch);
                    }
                }
            }
        }

        //  SerialPort記錄
        private void serialPort1_recorder(byte ch, bool SaveToLog = false)
        {
            if ((ch == 0x0A) || (byteMessage_length_A >= byteMessage_max_Ascii))
            {
                string dataValue = Encoding.ASCII.GetString(byteMessage_A).Substring(0, byteMessage_length_A);
                DateTime dt = DateTime.Now;
                string logValue = "[Receive_serialport1] [" + dt.ToString("yyyy/MM/dd HH:mm:ss.fff") + "]  " + dataValue + "\r\n"; //OK
                serialPort1_text = string.Concat(serialPort1_text, logValue);
                byteMessage_length_A = 0;
            }
            else
            {
                byteMessage_A[byteMessage_length_A] = ch;
                byteMessage_length_A++;
            }
        }

        //  開啟SerialPort
        protected void Open_serialPort2()
        {
            try
            {
                if (serialPort2.IsOpen == false)
                {
                    serialPort2.StopBits = StopBits.One;
                    serialPort2.PortName = ini12.INIRead(Config_Path, "serialPort2", "PortName", "");
                    serialPort2.BaudRate = int.Parse((ini12.INIRead(Config_Path, "serialPort2", "BaudRate", "")));
                    serialPort2.DataBits = 8;
                    serialPort2.Parity = (Parity)0;
                    serialPort2.ReceivedBytesThreshold = 1;
                    serialPort2.Open();
                }
            }
            catch (Exception Ex)
            {
                ini12.INIWrite(Config_Path, "serialPort2", "Exist", "0");
                MessageBox.Show(Ex.Message.ToString(), "serialPort2 Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  關閉SerialPort
        protected void Close_serialPort2()
        {
            serialPort2.Dispose();
            serialPort2.Close();
        }

        //  SerialPort分析
        private void serialPort2_analysis()
        {
            while (serialPort2.IsOpen == true)
            {
                int data_to_read = serialPort2.BytesToRead;
                if (data_to_read > 0)
                {
                    byte[] dataset = new byte[data_to_read];
                    serialPort2.Read(dataset, 0, data_to_read);

                    for (int index = 0; index < data_to_read; index++)
                    {
                        byte input_ch = dataset[index];
                        serialPort2_recorder(input_ch);
                    }
                }
            }
        }

        //  SerialPort記錄
        private void serialPort2_recorder(byte ch, bool SaveToLog = false)
        {
            if ((ch == 0x0A) || (byteMessage_length_B >= byteMessage_max_Ascii))
            {
                string dataValue = Encoding.ASCII.GetString(byteMessage_B).Substring(0, byteMessage_length_B);
                DateTime dt = DateTime.Now;
                string logValue = "[Receive_serialport2] [" + dt.ToString("yyyy/MM/dd HH:mm:ss.fff") + "]  " + dataValue + "\r\n"; //OK
                serialPort2_text = string.Concat(serialPort2_text, logValue);
                byteMessage_length_B = 0;
            }
            else
            {
                byteMessage_B[byteMessage_length_B] = ch;
                byteMessage_length_B++;
            }
        }

        private void btm4208sd_temperature(byte ch)
        {
            const int packet_len = 16;
            const int header_offset_1 = -16;
            const int header_offset_2 = -15;
            const int temp_ch_offset = -14;
            const int temp_unit_02 = -13;
            const int temp_unit_01 = -12;
            const int temp_polarity_offset = -11;
            const int temp_dp_offset = -10;
            const int temp_data8_offset = -9;
            const int temp_data7_offset = -8;
            const int temp_data6_offset = -7;
            const int temp_data5_offset = -6;
            const int temp_data4_offset = -5;
            const int temp_data3_offset = -4;
            const int temp_data2_offset = -3;
            const int temp_data1_offset = -2;
            const double temp_abs_value = 0.05;

            // If data_buffer is too long, cut off data not needed
            if (byteTemperature_length >= byteTemperature_max)
            {
                int destinationIndex = 0;
                for (int i = (byteTemperature_max - packet_len); i < byteTemperature_max; i++)
                {
                    byteTemperature[destinationIndex++] = byteTemperature[i];
                }
                byteTemperature_length = destinationIndex;
            }

            byteTemperature[byteTemperature_length] = ch;
            byteTemperature_length++;

            if (ch == 0x0D)
            {
                if (((byteTemperature_length + header_offset_1) >= 0) &&
                     (byteTemperature[byteTemperature_length + header_offset_1] == 0x02) &&
                     (byteTemperature[byteTemperature_length + header_offset_2] == '4'))
                {
                    // Channel number is checked and ok here
                    if ((byteTemperature[byteTemperature_length + temp_unit_02] == '0'))
                    {
                        if ((byteTemperature[byteTemperature_length + temp_unit_01] == '1')
                            || (byteTemperature[byteTemperature_length + temp_unit_01] == '2'))
                        {
                            if ((byteTemperature[byteTemperature_length + temp_data1_offset] != 0x18))
                            {
                                // data is valid
                                int DP_convert = '0';
                                int byteArray_position = 0;
                                byte[] byteArray = new byte[8];
                                for (int pos = byteTemperature_length + temp_data8_offset;
                                            pos <= (byteTemperature_length + temp_data1_offset);
                                            pos++)
                                {
                                    byteArray[byteArray_position] = byteTemperature[pos];
                                    byteArray_position++;
                                }

                                string tempSubstring = System.Text.Encoding.Default.GetString(byteArray);
                                double digit = Math.Pow(10, Convert.ToInt64(byteTemperature[byteTemperature_length + temp_dp_offset] - DP_convert));
                                currentTemperature = Convert.ToDouble(Convert.ToInt32(tempSubstring) / digit);

                                // is value negative?
                                if (byteTemperature[byteTemperature_length + temp_polarity_offset] == '1')
                                {
                                    currentTemperature = -currentTemperature;
                                }

                                // is value Fahrenheit?
                                if (byteTemperature[byteTemperature_length + temp_unit_01] == '2')
                                {
                                    currentTemperature = (currentTemperature - 32) / 1.8;
                                    currentTemperature = Math.Round((currentTemperature), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                    }


                    string dataValue;
                    // Channel number is checked and ok here
                    if ((byteTemperature[byteTemperature_length + temp_unit_02] == '0'))
                    {
                        if ((byteTemperature[byteTemperature_length + temp_unit_01] == '1')
                            || (byteTemperature[byteTemperature_length + temp_unit_01] == '2'))
                        {
                            if ((byteTemperature[byteTemperature_length + temp_data1_offset] != 0x18))
                            {
                                int DP_convert = '0';
                                int byteArray_position = 0;
                                byte[] byteArray = new byte[8];
                                for (int pos = byteTemperature_length + temp_data8_offset;
                                            pos <= (byteTemperature_length + temp_data1_offset);
                                            pos++)
                                {
                                    byteArray[byteArray_position] = byteTemperature[pos];
                                    byteArray_position++;
                                }

                                string tempSubstring = System.Text.Encoding.Default.GetString(byteArray);
                                double digit = Math.Pow(10, Convert.ToInt64(byteTemperature[byteTemperature_length + temp_dp_offset] - DP_convert));
                                if (tempSubstring != "000\u0018\u0018\u0018\u0018\n")
                                {
                                    double nowTemperature = Convert.ToDouble(Convert.ToInt32(tempSubstring) / digit);

                                    // is value negative?
                                    if (byteTemperature[byteTemperature_length + temp_polarity_offset] == '1')
                                    {
                                        nowTemperature = -nowTemperature;
                                    }

                                    // is value Fahrenheit?
                                    if (byteTemperature[byteTemperature_length + temp_unit_01] == '2')
                                    {
                                        nowTemperature = (nowTemperature - 32) / 1.8;
                                        nowTemperature = Math.Round((nowTemperature), 2, MidpointRounding.AwayFromZero);
                                    }

                                    // is channel value?
                                    int vOut = Convert.ToInt32(byteTemperature[byteTemperature_length + temp_ch_offset]) - 48;
                                    if (vOut > 10)
                                        vOut = vOut - 7;
                                    string channel = vOut.ToString("#00");

                                    dataValue = "CH" + channel + " = " + nowTemperature.ToString("#00.0") + " °C";
                                    UpdateUI("Temperature: " + dataValue, label_temperature);
                                }
                                else
                                {
                                    dataValue = "CH:--" + "=----";
                                }
                            }
                            else
                            {
                                // is channel value?
                                int vOut = Convert.ToInt32(byteTemperature[byteTemperature_length + temp_ch_offset]) - 48;
                                if (vOut > 10)
                                    vOut = vOut - 7;
                                string channel = vOut.ToString("#00");

                                dataValue = "CH:" + channel + "=----";
                            }
                        }
                    }
                }
                byteTemperature_length = 0;
            }
        }

        //執行緒控制label.text
        private delegate void UpdateUICallBack_Text(string value, Control ctl);
        private void UpdateUI(string value, Control ctl)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack_Text uu = new UpdateUICallBack_Text(UpdateUI);
                Invoke(uu, value, ctl);
            }
            else
            {
                ctl.Text = value;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            Thread LogAThread = new Thread(new ThreadStart(serialPort1_analysis));
            Thread LogBThread = new Thread(new ThreadStart(serialPort2_analysis));

            start_button = !start_button;
            if (start_button == true)
            {
                button_start.Text = "Stop";
                if (ini12.INIRead(Config_Path, "serialPort1", "Exist", "") == "1" && serialPort1.IsOpen == false)          //送至Comport
                {
                    Open_serialPort1();
                    LogAThread.Start();
                }

                if (ini12.INIRead(Config_Path, "serialPort2", "Exist", "") == "1" && serialPort2.IsOpen == false)          //送至Comport
                {
                    Open_serialPort2();
                    LogBThread.Start();
                }
            }
            else
            {
                button_start.Text = "Start";
                if (serialPort1.IsOpen == true)          //送至Comport
                {
                    LogAThread.Abort();
                    Close_serialPort1();
                }
                if (serialPort2.IsOpen == true)          //送至Comport
                {
                    LogBThread.Abort();
                    Close_serialPort2();
                }
            }
        }

        private void button_setting_Click(object sender, EventArgs e)
        {
            Setting Setting = new Setting();

            if (Setting.ShowDialog() == DialogResult.Cancel)
            {
                
            }
        }
    }
}
