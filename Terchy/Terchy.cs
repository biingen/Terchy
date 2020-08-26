using jini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Timers;

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

        DataTable dtTable = new DataTable();

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
                        chamber_recorder(input_ch);
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
                                    UpdateUI("Temperature Recorder: " + dataValue, label_temperatureRec);
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

        const int byteChamber_max = 36;
        byte[] byteChamber = new byte[byteChamber_max];
        int byteChamber_length = 0;

        private void chamber_recorder(byte ch)
        {
            const int packet_len = 9;
            const int header_data1_offset = -9;
            const int header_data2_offset = -8;
            const int length_data_offset = -7;
            const int data_actual2_offset = -6;
            const int data_actual1_offset = -5;
            const int data_target2_offset = -4;
            const int data_target1_offset = -3;
            const int crc16_highbit_offset = -2;
            const int crc16_lowbit_offset = -1;
            const int unit = 100;

            // If data_buffer is too long, cut off data not needed
            if (byteChamber_length >= byteChamber_max)
            {
                int destinationIndex = 0;
                for (int i = (byteTemperature_max - packet_len); i < byteTemperature_max; i++)
                {
                    byteChamber[destinationIndex++] = byteChamber[i];
                }
                byteChamber_length = destinationIndex;
            }

            byteChamber[byteChamber_length] = ch;
            byteChamber_length++;

            if (byteChamber_length == 9)
            {
                if ((byteChamber[byteChamber_length + header_data1_offset] == 0x01) &&
                    (byteChamber[byteChamber_length + header_data2_offset] == 0x03) &&
                    (byteChamber[byteChamber_length + length_data_offset] == 0x04))
                {
                    byte[] byteActual = new byte[2];
                    byte[] byteTarget = new byte[2];
                    byteActual[0] = byteChamber[byteChamber_length + data_actual2_offset];
                    byteActual[1] = byteChamber[byteChamber_length + data_actual1_offset];
                    byteTarget[0] = byteChamber[byteChamber_length + data_target2_offset];
                    byteTarget[1] = byteChamber[byteChamber_length + data_target1_offset];
                    string stringActual = BitConverter.ToString(byteActual).Replace("-", "");
                    string stringTarget = BitConverter.ToString(byteTarget).Replace("-", "");
                    float CH_actualTemperature = StrToFloat(Convert.ToInt32(stringActual, 16));
                    float CH_targetTemperature = StrToFloat(Convert.ToInt32(stringTarget, 16));
                    string dataValue = "Actual= " + (CH_actualTemperature / unit).ToString("#0.00") + " °C, Target= " + (CH_targetTemperature / unit).ToString("#0.00") + " °C";
                    UpdateUI("Chamber: " + dataValue, label_chamber);
                    byteChamber_length = 0;
                }
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

        //執行緒控制 datagriveiew
        private delegate void UpdateUICallBack1(string value, DataGridView ctl);
        private void GridUI(string i, DataGridView gv)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack1 uu = new UpdateUICallBack1(GridUI);
                Invoke(uu, i, gv);
            }
            else
            {
                dataGridView_Schedule.ClearSelection();
                gv.Rows[int.Parse(i)].Selected = true;
            }
        }

        // 執行緒控制 datagriverew的scorllingbar
        private delegate void UpdateUICallBack3(string value, DataGridView ctl);
        private void Gridscroll(string i, DataGridView gv)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack3 uu = new UpdateUICallBack3(Gridscroll);
                Invoke(uu, i, gv);
            }
            else
            {
                //DataGridView1.ClearSelection();
                //gv.Rows[int.Parse(i)].Selected = true;
                gv.FirstDisplayedScrollingRowIndex = int.Parse(i);
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            Thread MainThread = new Thread(new ThreadStart(MyRunSchedule));
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

                MainThread.Start();
                timer_chamber.Enabled = true;
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

                MainThread.Abort();
                timer_chamber.Enabled = false;
            }
        }

        private void button_set_Click(object sender, EventArgs e)
        {
            string temperature_total, humidity_total, slope_total, start_total;
            int temperature_number, temperature_value;
            int humidity_number, humidity_value;
            int slope_number, slope_value;
            bool textBox_temperature_bool = Int32.TryParse(textBox_chamberTemp.Text, out temperature_number);
            bool textBox_humidity_bool = Int32.TryParse(textBox_chamberHum.Text, out humidity_number);
            bool textBox_slope_bool = Int32.TryParse(textBox_chamberSlope.Text, out slope_number);

            if (textBox_temperature_bool)
                temperature_value = temperature_number * 100;
            else
                temperature_value = 0;

            if (textBox_humidity_bool)
                humidity_value = humidity_number * 100;
            else
                humidity_value = 0;

            if (textBox_slope_bool)
                slope_value = slope_number;
            else
                slope_value = 0;

            if (serialPort2.IsOpen == true)
            {
                temperature_total = Crc16.S8521_modbus("01 06 00 02", temperature_value);
                byte[] Outputbytes = new byte[temperature_total.Split(' ').Count()];
                Outputbytes = HexConverter.StrToByte(temperature_total);
                serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
            }

            if (humidity_value >= 0 && serialPort2.IsOpen == true)
            {
                humidity_total = Crc16.S8521_modbus("01 06 00 0C", humidity_value);
                byte[] Outputbytes = new byte[humidity_total.Split(' ').Count()];
                Outputbytes = HexConverter.StrToByte(humidity_total);
                serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
            }

            if (slope_value >= 0 && serialPort2.IsOpen == true)
            {
                slope_total = Crc16.S8521_modbus("01 06 00 33", slope_value);
                byte[] Outputbytes = new byte[slope_total.Split(' ').Count()];
                Outputbytes = HexConverter.StrToByte(slope_total);
                serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
            }

            if (temperature_value != 0 || humidity_value >= 0 || slope_value >= 0 && serialPort2.IsOpen == true)
            {
                start_total = Crc16.S8521_modbus("01 06 00 1E", 01);
                byte[] Outputbytes = new byte[start_total.Split(' ').Count()];
                Outputbytes = HexConverter.StrToByte(start_total);
                serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
            }
        }

        private void button_setting_Click(object sender, EventArgs e)
        {
            Setting Setting = new Setting();

            if (Setting.ShowDialog() == DialogResult.Cancel)
            {
                if (ini12.INIRead(Config_Path, "Schedule", "Exist", "") == "1")          //讀取Schedule
                {
                    ReadingCSV(ini12.INIRead(Config_Path, "Schedule", "Path", ""));
                }
            }
        }

        private void timer_chamber_Tick(object sender, EventArgs e)
        {
            string chamberCommand = Crc16.S8521_modbus("01 03 00 00", 02); //Read chamber temperature
            byte[] Outputbytes = new byte[chamberCommand.Split(' ').Count()];
            Outputbytes = HexConverter.StrToByte(chamberCommand);
            serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
        }

        public float StrToFloat(object FloatString)
        {
            float result;
            if (FloatString != null)
            {
                if (float.TryParse(FloatString.ToString(), out result))
                    return result;
                else
                {
                    return (float)0.00;
                }
            }
            else
            {
                return (float)0.00;
            }
        }

        private void ReadingCSV(string schedulefile)
        {
            int i = 0;
            var reader = new StreamReader(File.OpenRead(schedulefile));
            if ((File.Exists(schedulefile) == true))
            {
                dtTable.Columns.Clear();
                dtTable.Rows.Clear();
                TextFieldParser parser = new TextFieldParser(schedulefile);
                parser.Delimiters = new string[] { "," };
                string[] parts = new string[3];
                dataGridView_Schedule.DataSource = dtTable;
                dtTable.Columns.Add("Temperature", typeof(string));
                dtTable.Columns.Add("Time", typeof(string));
                dtTable.Columns.Add("Percentage", typeof(float));
                while (!parser.EndOfData)
                {
                    try
                    {
                        parts = parser.ReadFields();
                        if (parts == null)
                        {
                            break;
                        }

                        if (i != 0)
                        {
                            dtTable.Rows.Add(parts);
                        }
                        i++;
                    }
                    catch (MalformedLineException)
                    {
                        MessageBox.Show("Schedule cannot contain double quote ( \" \" ).", "Schedule foramt error");
                    }
                }
                parser.Close();
            }
        }

        private void MyRunSchedule()
        {
            int Scheduler_Row, temperature_number, temperature_value, SysDelay = 0;
            string temperature_total, slope_total;
            for (Scheduler_Row = 0; Scheduler_Row < dtTable.Rows.Count - 1; Scheduler_Row++)
            {
                string columns_temperature = dtTable.Rows[Scheduler_Row][0].ToString().Trim();
                string columns_time = dtTable.Rows[Scheduler_Row][1].ToString().Trim();
                string columns_percentage = dtTable.Rows[Scheduler_Row][2].ToString().Trim();

                if (start_button == false)
                {
                    break;
                }

                if (columns_time != "")
                {
                    if (columns_time.Contains('m'))
                    {
                        GridUI(Scheduler_Row.ToString(), dataGridView_Schedule);//控制Datagridview highlight//
                        Gridscroll(Scheduler_Row.ToString(), dataGridView_Schedule);//控制Datagridview scollbar//
                    }
                    else
                    {
                        if (int.Parse(columns_time) > 500)  //DataGridView UI update 
                        {
                            GridUI(Scheduler_Row.ToString(), dataGridView_Schedule);//控制Datagridview highlight//
                            Gridscroll(Scheduler_Row.ToString(), dataGridView_Schedule);//控制Datagridview scollbar//
                        }
                    }
                }

                if (columns_time != "" && int.TryParse(columns_time, out SysDelay) == true && columns_time.Contains('m') == false)
                    SysDelay = int.Parse(columns_time); // 指令停止時間(毫秒)
                else if (columns_time != "" && columns_time.Contains('m') == true)
                    SysDelay = int.Parse(columns_time.Replace('m', ' ').Trim()) * 60000; // 指令停止時間(分)
                else
                    SysDelay = 0;

                bool columns_temperature_bool = Int32.TryParse(columns_temperature, out temperature_number);
                if (columns_temperature_bool)
                    temperature_value = temperature_number * 100;
                else
                    temperature_value = 0;

                if (temperature_value != 0)
                {
                    temperature_total = Crc16.S8521_modbus("01 06 00 02", temperature_value);
                    byte[] Outputbytes = new byte[temperature_total.Split(' ').Count()];
                    Outputbytes = HexConverter.StrToByte(temperature_total);
                    serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
                }

                if (SysDelay > 0)
                {
                    slope_total = Crc16.S8521_modbus("01 06 00 33", SysDelay / 60000);
                    byte[] Outputbytes = new byte[slope_total.Split(' ').Count()];
                    Outputbytes = HexConverter.StrToByte(slope_total);
                    serialPort2.Write(Outputbytes, 0, Outputbytes.Length); //發送數據 Rs232 + Crc16
                    RedRatDBViewer_Delay(SysDelay);
                }
            }
        }

        // 這個主程式專用的delay的內部資料與function
        static bool Delay_TimeOutIndicator = false;
        private void Delay_OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Delay_TimeOutIndicator = true;
        }

        private void RedRatDBViewer_Delay(int delay_ms)
        {
            if (delay_ms <= 0) return;
            System.Timers.Timer Delay_Timer = new System.Timers.Timer(delay_ms);
            Delay_Timer.Elapsed += new ElapsedEventHandler(Delay_OnTimedEvent);
            Delay_TimeOutIndicator = false;
            Delay_Timer.Enabled = true;
            Delay_Timer.Start();
            while (Delay_TimeOutIndicator == false)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);//釋放CPU//
            }

            Delay_Timer.Stop();
            Delay_Timer.Dispose();
        }
    }
}
