using System;
using System.Linq.Expressions;
using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DifferentiateTests
{
    [TestClass]
    public class DifferentiateTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            Expression<Func<double, double>> f = x => x * x;
            var df = f.DifferentiateAndCompile();

            var result = df(12);

            Assert.AreEqual(24, result);
        }

        [TestMethod]
        public void SinTest()
        {
            Expression<Func<double, double>> f = x => (10 + Math.Sin(x)) * x;
            var df = f.DifferentiateAndCompile();
            
            var result = df(12);

            Assert.AreEqual(19.5897, Math.Round(result, 4));
        }
    }
}
