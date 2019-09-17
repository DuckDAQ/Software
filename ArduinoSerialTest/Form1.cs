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
using ArduinoSerialTest.Models;

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
        int LutPosition;
        //For recieving ADC values in BIN mode
        List<byte> serialList = new List<byte>();
        bool newAdcVals = false;
        //Chart form
        Chart chart = new Chart();

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
            comboBox5.SelectedIndex = 0;
            comboBoxADCgainCH.SelectedIndex = 0;
            comboBoxADCgain.SelectedIndex = 1;
            comboBoxRes.SelectedIndex = 1;
            comboBox4.SelectedIndex = 0;
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
                button8.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
                buttonBlockSize.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
                button13.Enabled = true;
                button14.Enabled = true;
                button15.Enabled = true;
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
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                buttonBlockSize.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button15.Enabled = false;
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
                try { chart.PopulateChart(AdcList); } catch { }
                return;
            }


            //Msg pipeline
            //var bytes = Encoding.Default.GetBytes(msg);
            //Add pipeline for ADC values to chart, no need to print them to Log
            string msg = System.Text.Encoding.ASCII.GetString(serialBuffer).Trim();

            if (msg.StartsWith("$")) { //DAQ can accept new LUT valu
                if(msg.Contains("LUT")) this.Invoke((MethodInvoker)delegate { Log(msg); });
                if (LutVals.Count > 0)
                {
                    int value = LutVals.ElementAt(0);
                    if (LutChannel == 2) value |= (1 << 12); //For channel selection, set channel selection bit (bits 12:15)
                    int location = (LutLength - LutVals.Count);
                    if (LutPosition == 0 || LutPosition == 1) {
                        location *= 2;
                        if (LutPosition == 1) location += 1; //If odd index was set, add 1 to location
                    }                    
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

            //DAQ returns #{val} where val is Transmit Counter Register (TCR) value. Could be used to change LUT on the fly(with slow DAC frequency)
            if (msg.StartsWith("#"))
            {
                this.Invoke((MethodInvoker)delegate {
                    Log("TCR value: "+msg.Substring(1)); // runs on UI thread
                });
                return;
            }
            
            this.Invoke((MethodInvoker)delegate {
                Log(msg); // runs on UI thread
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
        private void button8_Click(object sender, EventArgs e) //binarni nacin
        {
            serialPort.Write("M " + comboBox2.SelectedIndex + "\n\r");
        }

        private void Log(string logMsg) {
            richTextBox1.Text = richTextBox1.Text.Insert(0, logMsg+"\n");
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
            serialPort.Write("O\n\r");
        }

        private void Button17_Click(object sender, EventArgs e) //Stop DAC
        {
            serialPort.Write("K\n\r");
        }

        private void Button13_Click(object sender, EventArgs e) //set DAC Lut repeats
        {
            serialPort.Write($"C {(int)numericUpDown6.Value}\n\r");
        }

        private void Button16_Click(object sender, EventArgs e) //Set DAC period in microseconds
        {
            serialPort.Write("H " + numericUpDown8.Value + "\n\r");
        }

        private void Button12_Click(object sender, EventArgs e) //Send LUT
        {
            var vals = richTextBox2.Text.Split(',');
            LutPosition = comboBox3.SelectedIndex;
            int maxNumberOfVals = 1024;
            if (LutPosition == 2) maxNumberOfVals *= 2;
            if (vals.Length > maxNumberOfVals) {
                Log($"Error! Number of LUT values{vals.Length} exceeded the maximum of {maxNumberOfVals}!");
                richTextBox2.Text = "";
                return;
            }
            LutLength = (int)vals.Length;
            //button12.Enabled = false; //Lock this button untill all values have been sent to uC
            SendingLut = true;
            LutChannel = (int.Parse(comboBox5.Text));
           
            foreach (var val in vals) {
                if (int.TryParse(val.Trim(), out var valInt)) LutVals.Add(valInt);
            }
            //richTextBox2.Text = "";
            Log($"Will write {LutLength} LUT values to DAQ");
            int len = LutLength;
            if (LutPosition != 2) len *= 2; //If fullmode is selected, it is 1 channel only.
            //First send LUT length command. After that, send LUT values
            serialPort.Write("N " + len + "\n\r");
        }
        private void Button11_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show( 
                "Vedno zapišemo v DAC 2 vrednosti (par) iz LUT tabele, sodo in liho. Najprej bo DAC pretvoril sodo, takoj zatem (~20 clock cycles) bo pretvoril liho vrednost."
                +"\n\nČe je izbran '1 kanal', mora biti izbran Halfmode transfer, kar pomeni da je LUT za ta kanal lahko dolg 2kB in hitrost višja (brez conversiona drugega kanala)"
                ,
                "Pozicija v LUT tabeli",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            //Todo: calculate these values
            richTextBox2.Text = @"
2000,2012,2025,2037,2049,2061,2074,2086,2098,2110,2123,2135,2147,2160,2172,2184,2196,2208,2221,2233,2245,2257,2269,2282,2294,2306,2318,2330,2342,2354,2366,2379,
2391,2403,2415,2427,2439,2451,2463,2475,2486,2498,2510,2522,2534,2546,2558,2569,2581,2593,2605,2616,2628,2640,2651,2663,2674,2686,2697,2709,2720,2732,2743,2755,
2766,2777,2789,2800,2811,2822,2834,2845,2856,2867,2878,2889,2900,2911,2922,2933,2944,2954,2965,2976,2987,2997,3008,3019,3029,3040,3050,3061,3071,3081,3092,3102,
3112,3122,3132,3143,3153,3163,3173,3183,3192,3202,3212,3222,3231,3241,3251,3260,3270,3279,3289,3298,3307,3317,3326,3335,3344,3353,3362,3371,3380,3389,3398,3407,
3415,3424,3433,3441,3450,3458,3466,3475,3483,3491,3499,3507,3516,3524,3531,3539,3547,3555,3563,3570,3578,3585,3593,3600,3608,3615,3622,3629,3636,3643,3650,3657,
3664,3671,3678,3684,3691,3697,3704,3710,3716,3723,3729,3735,3741,3747,3753,3759,3765,3771,3776,3782,3787,3793,3798,3804,3809,3814,3819,3824,3829,3834,3839,3844,
3849,3853,3858,3862,3867,3871,3876,3880,3884,3888,3892,3896,3900,3904,3907,3911,3915,3918,3922,3925,3928,3931,3935,3938,3941,3944,3946,3949,3952,3955,3957,3960,
3962,3964,3967,3969,3971,3973,3975,3977,3979,3981,3982,3984,3985,3987,3988,3989,3991,3992,3993,3994,3995,3996,3996,3997,3998,3998,3999,3999,3999,4000,4000,4000,
4000,4000,4000,4000,3999,3999,3999,3998,3997,3997,3996,3995,3994,3993,3992,3991,3990,3989,3987,3986,3985,3983,3981,3980,3978,3976,3974,3972,3970,3968,3966,3963,
3961,3958,3956,3953,3951,3948,3945,3942,3939,3936,3933,3930,3927,3923,3920,3916,3913,3909,3906,3902,3898,3894,3890,3886,3882,3878,3873,3869,3865,3860,3856,3851,
3846,3842,3837,3832,3827,3822,3817,3812,3806,3801,3796,3790,3785,3779,3773,3768,3762,3756,3750,3744,3738,3732,3726,3720,3713,3707,3701,3694,3687,3681,3674,3667,
3661,3654,3647,3640,3633,3626,3618,3611,3604,3596,3589,3582,3574,3566,3559,3551,3543,3535,3527,3520,3511,3503,3495,3487,3479,3471,3462,3454,3445,3437,3428,3420,
3411,3402,3393,3385,3376,3367,3358,3349,3340,3330,3321,3312,3303,3293,3284,3275,3265,3256,3246,3236,3227,3217,3207,3197,3187,3178,3168,3158,3148,3137,3127,3117,
3107,3097,3086,3076,3066,3055,3045,3034,3024,3013,3003,2992,2981,2971,2960,2949,2938,2927,2916,2906,2895,2884,2873,2861,2850,2839,2828,2817,2806,2794,2783,2772,
2760,2749,2738,2726,2715,2703,2692,2680,2669,2657,2645,2634,2622,2610,2599,2587,2575,2563,2552,2540,2528,2516,2504,2492,2480,2469,2457,2445,2433,2421,2409,2397,
2385,2372,2360,2348,2336,2324,2312,2300,2288,2276,2263,2251,2239,2227,2215,2202,2190,2178,2166,2153,2141,2129,2117,2104,2092,2080,2068,2055,2043,2031,2018,2006,
1994,1982,1969,1957,1945,1932,1920,1908,1896,1883,1871,1859,1847,1834,1822,1810,1798,1785,1773,1761,1749,1737,1724,1712,1700,1688,1676,1664,1652,1640,1628,1615,
1603,1591,1579,1567,1555,1543,1531,1520,1508,1496,1484,1472,1460,1448,1437,1425,1413,1401,1390,1378,1366,1355,1343,1331,1320,1308,1297,1285,1274,1262,1251,1240,
1228,1217,1206,1194,1183,1172,1161,1150,1139,1127,1116,1105,1094,1084,1073,1062,1051,1040,1029,1019,1008,997,987,976,966,955,945,934,924,914,903,893,
883,873,863,852,842,832,822,813,803,793,783,773,764,754,744,735,725,716,707,697,688,679,670,660,651,642,633,624,615,607,598,589,
580,572,563,555,546,538,529,521,513,505,497,489,480,473,465,457,449,441,434,426,418,411,404,396,389,382,374,367,360,353,346,339,
333,326,319,313,306,299,293,287,280,274,268,262,256,250,244,238,232,227,221,215,210,204,199,194,188,183,178,173,168,163,158,154,
149,144,140,135,131,127,122,118,114,110,106,102,98,94,91,87,84,80,77,73,70,67,64,61,58,55,52,49,47,44,42,39,
37,34,32,30,28,26,24,22,20,19,17,15,14,13,11,10,9,8,7,6,5,4,3,3,2,1,1,1,0,0,0,0,
0,0,0,1,1,1,2,2,3,4,4,5,6,7,8,9,11,12,13,15,16,18,19,21,23,25,27,29,31,33,36,38,
40,43,45,48,51,54,56,59,62,65,69,72,75,78,82,85,89,93,96,100,104,108,112,116,120,124,129,133,138,142,147,151,
156,161,166,171,176,181,186,191,196,202,207,213,218,224,229,235,241,247,253,259,265,271,277,284,290,296,303,309,316,322,329,336,
343,350,357,364,371,378,385,392,400,407,415,422,430,437,445,453,461,469,476,484,493,501,509,517,525,534,542,550,559,567,576,585,
593,602,611,620,629,638,647,656,665,674,683,693,702,711,721,730,740,749,759,769,778,788,798,808,817,827,837,847,857,868,878,888,
898,908,919,929,939,950,960,971,981,992,1003,1013,1024,1035,1046,1056,1067,1078,1089,1100,1111,1122,1133,1144,1155,1166,1178,1189,1200,1211,1223,1234,
1245,1257,1268,1280,1291,1303,1314,1326,1337,1349,1360,1372,1384,1395,1407,1419,1431,1442,1454,1466,1478,1490,1502,1514,1525,1537,1549,1561,1573,1585,1597,1609,
1621,1634,1646,1658,1670,1682,1694,1706,1718,1731,1743,1755,1767,1779,1792,1804,1816,1828,1840,1853,1865,1877,1890,1902,1914,1926,1939,1951,1963,1975,1988,2000
            ";
        }

        private void Button15_Click(object sender, EventArgs e) //DAC transfer mode (fullmode/halfmode). Default on DAQ: fullmode, both channels
        {
            serialPort.Write("P " + comboBox4.SelectedIndex + "\n\r");
        }

        private void Button18_Click(object sender, EventArgs e) //returns pdc_read_tx_counter. Can be used for setting LUT on the fly.
        {
            serialPort.Write("U\n\r");
        }
    }
}
