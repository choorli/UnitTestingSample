using SimpleUnitTesting;
using System;
using Xunit;

namespace XUnitTest
{
    public class MathHelperTest
    {
        //No Parameters
        //تست تابع بررسی زوج بودن
        [Fact]
        public void IsEvenTest()
        {
            var calculate = new MathFormulas();

            int x = 1;
            int y = 2;

            var xResult = calculate.IsEven(x);
            var yResult = calculate.IsEven(y);

            Assert.False(xResult);
            Assert.True(yResult);
        }
        //With Parameters ==> (InlineData,MemberData,ClassData)
        #region--UnitTesting_InlineData--
        //تست تابع محاسبه تفاضل
        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 1)]//not true
        public void DiffTest(int x, int y, int expectedValue)
        {
            var calculate = new MathFormulas();
            var result = calculate.Diff(x, y);
            Assert.Equal(result, expectedValue);
        }

        //تست تایع جمع 
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(1, 3, 1)]//not true
        public void AddTest(int x, int y, int expectedValue)
        {
            var calculate = new MathFormulas();
            var result = calculate.Add(x, y);
            Assert.Equal(result, expectedValue);
        }

        //تست تایع مجموع اعداد
        [Theory]
        [InlineData(new int[3] { 1, 2, 3 }, 6)]
        [InlineData(new int[3] { -4, -6, 3 }, 6)]//not true

        public void SumTest(int[] values, int expectedValue)
        {
            var calculate = new MathFormulas();
            var result = calculate.Sum(values);
            Assert.Equal(result, expectedValue);
        }

        //تست تایع میانگین
        [Theory]
        [InlineData(new int[3] { 1, 2, 3 }, 2)]
        [InlineData(new int[3] { -4, -6, 3 }, 6)]//not true
        public void AverageTest(int[] values, int expectedValue)
        {
            var calculate = new MathFormulas();
            var result = calculate.Average(values);
            Assert.Equal(result, expectedValue);
        }
        #endregion

        #region--UnitTesting_MemberData--
        //تست تابع جمع با استفاده از MemberData
        [Theory]
        [MemberData(nameof(MathFormulas.Data), MemberType = typeof(MathFormulas))]
        public void AddTest_MemberData(int x, int y, int expectedValue)
        {
            var calculate = new MathFormulas();
            var result = calculate.Add(x, y);
            Assert.Equal(result, expectedValue);
        }
        #endregion


        #region--UnitTesting_ClassData--
        //تست تابع جمع با استفاده از MemberData
        //[Theory (Skip ="this is just a reason ...")]
        [Theory]
        [ClassData(typeof(MathFormulas))]
        public void AddTest_ClassData(int x, int y, int expectedValue)
        {
            var calculate = new MathFormulas();
            var result = calculate.Add(x, y);
            Assert.Equal(result, expectedValue);
        }
        #endregion
    }
}
