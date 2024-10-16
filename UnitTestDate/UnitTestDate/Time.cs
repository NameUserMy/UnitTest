using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace UnitTestDate
{
    public class Time
    {
        public int Hour { get;private set; }
        public int Minute { get; private set; }

        public bool SetHour(int hour)
        {
            if (hour < 0 ||hour>23) { 
            
                return false;
            }
            Hour = hour;
            return true;
        }

        public bool SetMinute(int minute)
        {
            if (minute < 0 || minute > 59)
            {

                return false;
            }
            Minute = minute;
            return true;
        }

        public string ResultDate(bool hour,bool minute)
        {
            if (hour&&minute)
            {
                return $"Time: {Hour}:{Minute}";
            }
            return "The Time is not correct";
            
        }
    }
}
