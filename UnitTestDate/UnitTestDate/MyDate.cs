

namespace UnitTestDate
{
    
    public class MyDate
    {

        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public bool SetDay(int day)
        {
            if (day > 31 || day <1) 
            {  
                return false;
            }else  if (Month == 2) {

                if (day > 28 || day < 1)
                {
                    return false;
                }
                else
                {
                    Day = day;
                }
            }
            else
            {

                Day = day;

            }
            return true;
        }
        public bool SetMonth(int month) {

            if (month > 12 || month < 1)
            {
                return false;
            }
            else
            {
                Month = month;
            }

            return true;
        }
        public bool SetYear(int year) {


            if (year < 1900 || year > 3000)
            {
                return false;
            }
            else {

                Year = year;
                
                return true; 
            }
            
        }


        public string ResultDate()
        {
            if (Day*Month*Year==0)
            {
                return "The date is not correct";
            }

            return $"Day: {Day}  Month:{Month}  Year: {Year}";
        }
    }
}
