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
            comboBox2.SelectedIndex = 0;
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
            this.Invoke((MethodInvoker)delegate {
                Log(serialPort.ReadExisting()); // runs on UI thread
            });
        }

        private void button1_Click(object sender, EventArgs e) //ascii
        {
            serialPort.Write("S\n\r");
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
            totalBytes += logMsg.Length;
            richTextBox1.Text = richTextBox1.Text.Insert(0, logMsg);
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

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
