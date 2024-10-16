

using UnitTestDate;

namespace TestDate
{
    [TestClass]
    public class DateTest 
    {
        #region Day
        [TestMethod]
        public void DayTestMin()
        {
            //Arange
            int day = 1;
            bool expected = true;
            bool actual;
            var date = new MyDate();
          
            //Act
            actual= date.SetDay(day);

            //Assert
            Assert.AreEqual(expected,actual);

           
        }
        [TestMethod]
        public void DayTestMax()
        {
            //Arange
            int day = 31;
           
            bool expected;
            var date = new MyDate();

            //Act
            expected = date.SetDay(day);

            //Assert
            Assert.IsTrue(expected);


        }

        [TestMethod]
        public void DayTestFalse()
        {
            //Arange
            int day = 0;
            bool expected = false;
            bool actual;
            var date = new MyDate();

            //Act
            actual = date.SetDay(day);

            //Assert
            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void DayTestFebruary() {

            //Arange
            int day = 30;
            int month = 2;
            bool expected = false;
            bool actual;
            var date = new MyDate();

            //Act

            date.SetMonth(month);
            actual = date.SetDay(day);

            //Assert
            Assert.AreEqual(expected, actual);


        }

        #endregion

        #region Month
        [TestMethod]
        public void MonthTestMin()
        {
            //Arange
            int month = 1;
            bool expected = true;
            bool actual;
            var date = new MyDate();

            //Act
            actual = date.SetMonth(month);

            //Assert
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]
        public void MonthTestMax()
        {
            //Arange
            int month = 10;

            bool expected;
            var date = new MyDate();

            //Act
            expected = date.SetMonth(month);

            //Assert
            Assert.IsTrue(expected);


        }

        [TestMethod]
        public void MonthTestFalse()
        {
            //Arange
            int month = 0;
            bool expected = false;
            bool actual;
            var date = new MyDate();
            //Act
            actual = date.SetMonth(month);

            //Assert
            Assert.AreEqual(expected, actual);

           


        }
        #endregion

        #region Year
        [TestMethod]

        public void YearRangeMin()
        {
            //Arange
            int year = 1899;
            bool expected = false;
            bool actual;
            var date = new MyDate();

            //Act
            actual = date.SetYear(year);

            //Assert
            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void YearRangeMax()
        {
            //Arange
            int year = 3001;
            bool expected = false;
            bool actual;
            var date = new MyDate();

            //Act
            actual = date.SetYear(year);

            //Assert
            Assert.AreEqual(expected, actual);


        }
        #endregion

        #region Converter
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertToIntFormatException()
        {
            //Arange
            string value = "55.2";
            var convert = new StringToInt();
            //Act
            convert.StrToInt(value);
        }
        [TestMethod]
        public void ConvertToInt()
        {
            //Arange
            string value = "55";
            int expected = 55;
            int actual;
            var convert = new StringToInt();
            //Act
            actual= convert.StrToInt(value);
            //Assert

            Assert.AreEqual(expected, actual);
        }
        #endregion
        [TestMethod]
        public void DateResult()
        {
            //Arange
            string expected = "The date is not correct";
            string actual;
            var date = new MyDate();
            date.SetYear(2024);
            date.SetMonth(2);
            date.SetDay(29);
            //Act
            actual = date.ResultDate();

            //Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
