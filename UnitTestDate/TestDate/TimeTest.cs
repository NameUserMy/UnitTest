using UnitTestDate;

namespace TestDate
{

    [TestClass]
    public class TimeTest
    {

        [TestMethod]
        public void Hours()
        {
            //Arange
            int hour =23;
            bool actual;
            string message= "Expected Tru";
            var date = new Time();
            //Act
            actual = date.SetHour(hour);
            //Assert
            Assert.IsTrue(actual, message);
        }

        [TestMethod]
        public void Minutes()
        {
            //Arange
            int hour = 59;
            bool actual;
            string message = "Expected Tru";
            var date = new Time();
            //Act
            actual = date.SetMinute(hour);
            //Assert
            Assert.IsTrue(actual, message);
        }

        [TestMethod]
        public void Result()
        {
            //Arange
            bool hour = true;
            bool minute=false;
            string actual;
            string expected = "The Time is not correct";
            var date = new Time();
            //Act
            actual = date.ResultDate(hour,minute);
            //Assert
            Assert.AreEqual(expected,actual);
        }
    }
}
