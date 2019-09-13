using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArduinoSerialTest.Models;

namespace ArduinoSerialTest
{
    public partial class Chart : Form
    {
        long LastTicks;
        UInt64 time = 0;
        System.Timers.Timer ChartT;
        long TimerTicks;
        public Chart()
        {
            InitializeComponent();
        }
        private void TimerStart()
        {
            TimerTicks = 0;
            ChartT = new System.Timers.Timer();
            ChartT.Elapsed += Timer_Tick;
            ChartT.Interval = 10;
            ChartT.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerTicks++;
        }

        public void PopulateChart(List<AdcMeasurement> list)
        {
            //Najprej najstarejsega poslje, seprau na koncu lista je najstarejsi 
            for (byte ch = 0; ch < 7; ch += 2)
            {
                if (ch == 0) time += (UInt64)numericUpDown1.Value;

                var chVals = list.Where(x => x.channel == ch).ToList();
                if (chVals.Count == 0) break;

                if (LastTicks == 0)
                {
                    //First measurement, only show newest values
                    this.Invoke((MethodInvoker)delegate { chart1.Series[ch / 2].Points.AddXY(time / 100, AdcToVoltage(chVals.Last().value)); });
                }
                else
                {
                    //var deltaTicks = (TimerTicks - LastTicks) / chVals.Count;
                    for (byte i = 0; i < chVals.Count; i++)
                    {
                        this.Invoke((MethodInvoker)delegate { chart1.Series[ch / 2].Points.AddXY(time / 100, AdcToVoltage(chVals[i].value)); });
                    }
                }

                //Remove outdated values
                this.Invoke((MethodInvoker)delegate {
                    int cnt = chart1.Series[ch / 2].Points.Count;
                    if (cnt > (numericUpDownBlockSize.Value * 4))
                        for (int i = 0; i < (cnt - (numericUpDownBlockSize.Value * 4)); i++)
                            chart1.Series[ch / 2].Points.RemoveAt(i);
                });

            }
            this.Invoke((MethodInvoker)delegate {
                chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;
                //chart1.ChartAreas[0].AxisX.Maximum = TimerTicks;
                chart1.ChartAreas[0].AxisX.Maximum = time / 100;
            });

            //LastTicks = TimerTicks;
        }

        int AdcToVoltage(int measurment)
        {
            measurment *= 20000;
            measurment /= 4095;
            measurment = 10000 - measurment;
            return measurment;
        }
    }
}
