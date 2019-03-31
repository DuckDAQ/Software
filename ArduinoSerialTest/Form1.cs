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
                serialPort1 = new SerialPort(com, bRate, Parity.None, 8, StopBits.One);
                
                //serialPort1.PortName = com;
                //serialPort1.BaudRate = bRate;
                //serialPort1.Parity = Parity.Odd;
                //serialPort1.StopBits = StopBits.One;
                serialPort1.Open();
                serialPort1.Encoding = System.Text.Encoding.ASCII;
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
                serialPort1.Handshake = Handshake.None;

                Log("Port opened: "+serialPort1.IsOpen);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening serial port: " + ex.Message);
                Log(ex.Message);
            }
        }


        private void portsRefresh_Click(object sender, EventArgs e)
        {
            refreshComPortBox();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e) //onDataReceived
        {
            Log(e.ToString());
        }

        private void button1_Click(object sender, EventArgs e) //ascii
        {
            serialPort1.Write("S");
        }
        private void button2_Click(object sender, EventArgs e) //prekini vzorcenje
        {
            serialPort1.Write("T");
        }
        private void button3_Click(object sender, EventArgs e) //nastavi periodo
        {
            serialPort1.Write("R " + numericUpDown1.Value);
        }
        private void button4_Click(object sender, EventArgs e) //st vzorcev za povprecenje
        {
            serialPort1.Write("A " + numericUpDown2.Value);
        }
        private void button5_Click(object sender, EventArgs e) //st zaporednih meritev
        {
            serialPort1.Write("F " + numericUpDown3.Value);
        }
        private void button6_Click(object sender, EventArgs e) //nastavitev zaporedje kanalov
        {
            serialPort1.Write("E " + numericUpDown4.Value);
        }
        private void button7_Click(object sender, EventArgs e) //nastavi vrednost analognega izhoda
        {
            serialPort1.Write("D " + numericUpDown5.Value);
        }
        private void button8_Click(object sender, EventArgs e) //binarni nacin
        {
            serialPort1.Write("s");
        }


        private void Log(string logMsg) {
            richTextBox1.Text = richTextBox1.Text.Insert(0, DateTime.Now.ToShortTimeString() + ": " + logMsg);
        }
    }
}
