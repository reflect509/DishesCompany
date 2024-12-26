namespace SF2022User_NN_Lib.Tests
{
    [TestFixture]
    public class CalculationsTests
    {
        [Test]
        public void AvailablePeriods_ThrowsArgumentOutOfRangeException_consultationTime()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0) };
            int[] durations = new int[] { 10, 20 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(20, 0, 0);
            int consultationTime = 0;

            Calculations calculations = new Calculations();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [Test]
        public void AvailablePeriods_ThrowsArgumentOutOfRangeException_durations()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0) };
            int[] durations = new int[] { 0, -5 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(20, 0, 0);
            int consultationTime = 20;

            Calculations calculations = new Calculations();

            Assert.Throws<ArgumentOutOfRangeException>(() => calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [Test]
        public void AvailablePeriods_beginWorkingTimeGreaterThanEndWorkingTime_ReturnsEmptyArray()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0) };
            int[] durations = new int[] { 10, 20 };
            TimeSpan beginWorkingTime = new TimeSpan(15, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(10, 0, 0);
            int consultationTime = 20;

            Calculations calculations = new Calculations();

            Assert.That(calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime), Is.Empty);
        }

        [Test]
        public void AvailablePeriods_ValidData_NoException()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0) };
            int[] durations = new int[] { 10, 25 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(16, 0, 0);
            int consultationTime = 40;

            Calculations calculations = new Calculations();

            var result = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void AvailablePeriods_CorrectData_CorrectAvailablePeriods(
            TimeSpan[] startTimes,
            int[] durations,
            TimeSpan beginWorkingTime,
            TimeSpan endWorkingTime,
            int consultationTime,
            string[] expectedResult)
        {
            Calculations calculations = new Calculations();

            var result = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        public static IEnumerable<Object[]> TestData()
        {
            yield return new object[] {
                new TimeSpan[] { new TimeSpan(8, 0, 0) },
                new int[] {40},
                new TimeSpan(8, 0, 0),
                new TimeSpan(10, 0, 0),
                30,
                new string[] {"08:40-09:10", "09:10-09:40"}
            };

            yield return new object[] {
                new TimeSpan[] { new TimeSpan(11, 40, 0) },
                new int[] {20},
                new TimeSpan(10, 20, 0),
                new TimeSpan(12, 0, 0),
                30,
                new string[] {"10:20-10:50", "10:50-11:20"}
            };

            yield return new object[] {
                new TimeSpan[] { new TimeSpan(11, 0, 0), new TimeSpan(13, 20, 0), new TimeSpan(13, 50, 0) },
                new int[] {40, 30, 60},
                new TimeSpan(10, 0, 0),
                new TimeSpan(15, 0, 0),
                35,
                new string[] {"10:00-10:35", "11:40-12:15", "12:15-12:50"}
            };

            yield return new object[] {
                new TimeSpan[] { new TimeSpan(11, 10, 0) },
                new int[] {20},
                new TimeSpan(10, 20, 0),
                new TimeSpan(12, 0, 0),
                30,
                new string[] {"10:20-10:50", "11:30-12:00"}
            };

            yield return new object[] {
                new TimeSpan[] { new TimeSpan(8, 50, 0), new TimeSpan(12, 20, 0), new TimeSpan(15, 0, 0) },
                new int[] {40, 50, 70},
                new TimeSpan(8, 0, 0),
                new TimeSpan(16, 0, 0),
                60,
                new string[] {"09:30-10:30", "10:30-11:30", "13:10-14:10"}
            };

            yield return new object[] {
                new TimeSpan[] { new TimeSpan(9, 40, 0), new TimeSpan(10, 40, 0) },
                new int[] {10, 20},
                new TimeSpan(9, 20, 0),
                new TimeSpan(11, 0, 0),
                30,
                new string[] {"09:50-10:20"}
            };
        }
    }
}