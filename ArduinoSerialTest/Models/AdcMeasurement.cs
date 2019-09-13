using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoSerialTest.Models
{
    public class AdcMeasurement
    {
        public short value { get; set; }
        public byte channel { get; set; }
    }
}
