using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDate
{
    public class StringToInt
    {
        public int value;

        public int StrToInt(string incomming) {
            value=Convert.ToInt32(incomming);
            return value;
        }
    }
}
