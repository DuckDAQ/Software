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

        public ArduinoForm()
        {
            InitializeComponent();
        }

        private void ArduinoForm_Load(object sender, EventArgs e)
        {
            refreshComPortBox();
            comPort.SelectedIndex = comPort.Items.Count - 1;
            baudRate.SelectedIndex = 0;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening serial port: " + ex.Message);
                Log(ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
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
            serialPort.Write("E " + numericUpDown4.Value + "\n\r");
        }
        private void button7_Click(object sender, EventArgs e) //nastavi vrednost analognega izhoda
        {
            serialPort.Write("D " + numericUpDown5.Value + "\n\r");
        }
        private void button8_Click(object sender, EventArgs e) //binarni nacin
        {
            serialPort.Write("s\n\r");
        }


        private void Log(string logMsg) {
            richTextBox1.Text = richTextBox1.Text.Insert(0, DateTime.Now.ToShortTimeString() + ": " + logMsg);
        }
    }
}
