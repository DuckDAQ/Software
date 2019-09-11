using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace ArduinoSerialTest
{
    public partial class ArduinoForm : Form
    {

        SerialPort serialPort;
        float totalBytes;
        byte numOfCh = 4;
        //For sending DAC LookUpTable values
        bool SendingLut = false;
        List<int> LutVals = new List<int>();
        int LutLength = 0;
        int LutChannel = 0;
        bool LutEvenIndexes = false;
        //For recieving ADC values in BIN mode
        List<byte> serialList = new List<byte>();
        bool newAdcVals = false;
        //For Chart 
        System.Timers.Timer ChartT;
        long TimerTicks;
        long LastTicks;
        UInt64 time = 0;
        public ArduinoForm()
        {
            InitializeComponent();
        }

        private void ArduinoForm_Load(object sender, EventArgs e)
        {
            refreshComPortBox();
            comPort.SelectedIndex = comPort.Items.Count - 1;
            baudRate.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBoxADCgainCH.SelectedIndex = 0;
            comboBoxADCgain.SelectedIndex = 1;
            comboBoxRes.SelectedIndex = 1;
        }

        private void refreshComPortBox ()
        {
            string[] ports = SerialPort.GetPortNames();
            comPort.Items.Clear();
            comPort.Items.AddRange(ports);
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string com = (string)comPort.SelectedItem;
                int bRate = int.Parse((string)baudRate.SelectedItem);
                serialPort = new SerialPort(com, bRate, Parity.None, 8, StopBits.One);

                //serialPort.PortName = com;
                //serialPort.BaudRate = bRate;
                //serialPort.Parity = Parity.Odd;
                //serialPort.StopBits = StopBits.One;
                //serialPort.DataReceived += SerialPort_DataReceived;

                serialPort.Encoding = System.Text.Encoding.ASCII;
                serialPort.ReadTimeout = 500;
                serialPort.WriteTimeout = 500;
                serialPort.Handshake = Handshake.None;

                serialPort.Open();
                Log("Port opened: "+ serialPort.IsOpen);

                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
                buttonBlockSize.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
                button13.Enabled = true;
                button14.Enabled = true;
                button16.Enabled = true;
                button17.Enabled = true;
            }
            catch (Exception ex)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                buttonBlockSize.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button14.Enabled = false;
                button16.Enabled = false;
                button17.Enabled = false;
                MessageBox.Show("Error opening serial port: " + ex.Message);
                Log(ex.Message);
            }
        }

        private void portsRefresh_Click(object sender, EventArgs e)
        {
            refreshComPortBox();
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) //onDataReceived
        {
            //string msg = serialPort.ReadExisting();
            //char[] charArr = msg.ToCharArray();
            //this.Invoke((MethodInvoker)delegate { Log(msg); });
            
            int bytesToRead = serialPort.BytesToRead;
            totalBytes += bytesToRead;
            //this.Invoke((MethodInvoker)delegate { Log(bytesToRead.ToString()); });
            var serialBuffer = new byte[bytesToRead];
            serialPort.Read(serialBuffer, 0, bytesToRead);
            if (bytesToRead == 2) { //Could be Sync bytes
                if (serialBuffer[0] >> 7 == 1 && serialBuffer[1] >> 7 == 1){ //Search for sync bytes
                    newAdcVals = true;
                    return;
                }
            }
            if (newAdcVals) { //Get values
                newAdcVals = false;
                List<AdcMeasurement> AdcList = new List<AdcMeasurement>();

                for (int i = 0; i < serialBuffer.Length; i += 2) { //create short value from two bytes
                    short num = (short)((serialBuffer[i+1] << 8) | serialBuffer[i]);
                    AdcList.Add(new AdcMeasurement()
                    {
                        value = (short)(num & 0b111111111111),
                        channel = (byte)(num >> 12)
                    });
                }
                PopulateChart(AdcList);
                return;
            }


            //Msg pipeline
            //var bytes = Encoding.Default.GetBytes(msg);
            //Add pipeline for ADC values to chart, no need to print them to Log
            string msg = System.Text.Encoding.ASCII.GetString(serialBuffer).Trim();

            if (msg.Contains('$')) { //DAQ can accept new LUT valu
                if(msg.Contains("LUT")) this.Invoke((MethodInvoker)delegate { Log(msg); });
                if (LutVals.Count > 0)
                {
                    int value = LutVals.ElementAt(0);
                    if (LutChannel == 2) value |= (1 << 12); //For channel selection, set channel selection bit (bits 12:15)
                    int location = (LutLength - LutVals.Count)*2;
                    if (!LutEvenIndexes) location += 1; //If odd index was set, add 1 to location
                    string write = $"J {location}, {value}\n\r";
                    serialPort.Write(write);
                    this.Invoke((MethodInvoker)delegate { Log(write); });
                    LutVals.RemoveAt(0);
                    
                }
                else { //setting LUT values has finished.
                    LutVals = new List<int>();
                    SendingLut = false;
                    LutLength = 0;
                    this.Invoke((MethodInvoker)delegate { button12.Enabled = true; });
                    LutChannel = 0;
                    this.Invoke((MethodInvoker)delegate {
                        Log("Setting LUT values has finished!"); // runs on UI thread
                    });
                }
                return;
            }
            
            this.Invoke((MethodInvoker)delegate {
                Log(msg); // runs on UI thread
            });
        }

        private void button1_Click(object sender, EventArgs e) //ascii
        {
            TimerTicks = 0;
            ChartT = new System.Timers.Timer();
            ChartT.Elapsed += Timer_Tick;
            ChartT.Interval = 10;
            ChartT.Start();

            serialPort.Write("S\n\r");
        }
        private void Timer_Tick(object sender, EventArgs e) {
            TimerTicks++;
        }
        private void button2_Click(object sender, EventArgs e) //prekini vzorcenje
        {
            serialPort.Write("T\n\r");
        }
        private void button3_Click(object sender, EventArgs e) //nastavi periodo
        {
            serialPort.Write("R " + numericUpDown1.Value + "\n\r");
        }
        private void button4_Click(object sender, EventArgs e) //st vzorcev za povprecenje
        {
            serialPort.Write("A " + numericUpDown2.Value + "\n\r");
        }
        private void button5_Click(object sender, EventArgs e) //st zaporednih meritev
        {
            serialPort.Write("F " + numericUpDown3.Value + "\n\r");
        }
        private void button6_Click(object sender, EventArgs e) //nastavitev zaporedje kanalov
        {
            serialPort.Write("E " + comboBox1.Text + "\n\r");
        }
        private void button7_Click(object sender, EventArgs e) //nastavi vrednost analognega izhoda
        {
            float voltage = (float)Convert.ToDecimal(numericUpDown5.Value);
            //0 = -9.74V
            //4095 = 9.68V
            //delta(1) = 4.74mV
            int bin = (int)Math.Round((voltage + 9.74) / 0.00474);
            serialPort.Write("D " + numericUpDown4.Value + ", " + bin.ToString() + "\n\r");
        }
        private void button8_Click(object sender, EventArgs e) //binarni nacin
        {
            serialPort.Write("M " + comboBox2.SelectedIndex + "\n\r");
        }

        private void Log(string logMsg) {
            //richTextBox1.Text = richTextBox1.Text.Insert(0, DateTime.Now.ToShortTimeString() + ": " + logMsg);
            richTextBox1.Text = richTextBox1.Text.Insert(0, logMsg+"\n");
            //richTextBox1.Text = logMsg;
        }

        private void ArduinoForm_Leave(object sender, EventArgs e)
        {
            serialPort.DataReceived -= new SerialDataReceivedEventHandler(serialPort_DataReceived);
            if (serialPort.IsOpen)
            {
                serialPort.Write("T\n\r");
                serialPort.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            serialPort.Write("G " + comboBoxADCgainCH.Text + ", " + comboBoxADCgain.SelectedIndex + "\n\r");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            serialPort.Write("L " + comboBoxRes.SelectedIndex + "\n\r");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelSpeed.Text = (totalBytes/1024.0).ToString("#0.00") + " kB/s";

            if(comboBox2.SelectedIndex > 0)
            {
                if(numericUpDown2.Value > 1)
                {
                    UInt16 mesCount = (UInt16)(((UInt16)numericUpDownBlockSize.Value / 4) / (UInt16)numericUpDown2.Value);
                    labelSampleRate.Text = ((totalBytes * (numOfCh * mesCount)) / ((numOfCh * mesCount) * 2) + 2).ToString("#0.00") + " S/s";
                }
                else
                {
                    labelSampleRate.Text = ((totalBytes * (float)(numericUpDownBlockSize.Value)) / ((float)(numericUpDownBlockSize.Value * 2) + 2.0)).ToString("#0") + " S/s";
                }
            }
            else
            {
                labelSampleRate.Text = ((totalBytes * numOfCh) / 64.0).ToString("#0.00") + " S/s";
            }
            
            totalBytes = 0;
        }

        private void buttonBlockSize_Click(object sender, EventArgs e)
        {
            serialPort.Write("B " + numericUpDownBlockSize.Value + "\n\r");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numOfCh = 4;

            for(byte i = 0; i < comboBox1.Text.Length; i++)
            {
                if (comboBox1.Text[i] == '0') numOfCh--;
            }
        }

        private void Button14_Click(object sender, EventArgs e) //Start DAC
        {

        }

        private void Button17_Click(object sender, EventArgs e) //Stop DAC
        {

        }

        private void Button13_Click(object sender, EventArgs e) //set DAC Lut repeats
        {

        }

        private void Button16_Click(object sender, EventArgs e) //Set DAC freq
        {

        }

        private void Button12_Click(object sender, EventArgs e) //Send LUT
        {
            var vals = richTextBox2.Text.Split(',');
            if (vals.Length > 1024) {
                Log($"Error! Number of LUT values{vals.Length} exceeded the maximum of 1024!");
                richTextBox2.Text = "";
                return;
            }
            LutLength = (int)vals.Length;
            //button12.Enabled = false; //Lock this button untill all values have been sent to uC
            SendingLut = true;
            LutChannel = (int.Parse(comboBox5.Text));
            LutEvenIndexes = comboBox3.SelectedIndex == 0;
            foreach (var val in vals) {
                if (int.TryParse(val.Trim(), out var valInt)) LutVals.Add(valInt);
            }
            //richTextBox2.Text = "";
            Log($"Will write {LutLength} LUT values to DAQ");
            //First send LUT length command. After that, send LUT values
            serialPort.Write("N " + LutLength*2 + "\n\r");
        }

        private void PopulateChart(List<AdcMeasurement> list) {
            //Najprej najstarejsega poslje, seprau na koncu lista je najstarejsi 
            for (byte ch = 0; ch < 7; ch += 2)
            {
                if (ch == 0) time += (UInt64)numericUpDown1.Value;

                var chVals = list.Where(x => x.channel == ch).ToList();
                if (chVals.Count == 0) break;

                if (LastTicks == 0)
                {
                    //First measurement, only show newest values
                    this.Invoke((MethodInvoker)delegate { chart1.Series[ch / 2].Points.AddXY(time/100, AdcToVoltage(chVals.Last().value)); });
                }
                else
                {
                    //var deltaTicks = (TimerTicks - LastTicks) / chVals.Count;
                    for (byte i = 0; i < chVals.Count; i++)
                    {
                        this.Invoke((MethodInvoker)delegate { chart1.Series[ch / 2].Points.AddXY(time/100, AdcToVoltage(chVals[i].value)); });
                    }
                }

                //Remove outdated values
                this.Invoke((MethodInvoker)delegate {
                    int cnt = chart1.Series[ch / 2].Points.Count;
                    if (cnt > (numericUpDownBlockSize.Value * 4)) 
                        for(int i = 0; i < (cnt- (numericUpDownBlockSize.Value * 4)); i++)
                            chart1.Series[ch / 2].Points.RemoveAt(i);
                });
   
            }
            this.Invoke((MethodInvoker)delegate {
                chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;
                //chart1.ChartAreas[0].AxisX.Maximum = TimerTicks;
                chart1.ChartAreas[0].AxisX.Maximum = time/100;
            });
            
            //LastTicks = TimerTicks;
        }

        int AdcToVoltage(int measurment) {
            measurment *= 20000;
            measurment /= 4095;
            measurment = 10000 - measurment;
            return measurment;
        }

        class AdcMeasurement {
            public short value { get; set; }
            public byte channel { get; set; }
        }

        private void Button11_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show( 
                "Vedno zapišemo v DAC 2 vrednosti (par) iz LUT tabele, sodo in liho. Najprej bo DAC pretvoril sodo, takoj zatem (~20 clock cycles) bo pretvoril liho vrednost.",
                "Pozicija v LUT tabeli",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
