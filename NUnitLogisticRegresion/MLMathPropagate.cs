using ConsoleSteps.Tools;
using NUnit.Framework;
using System;

namespace NUnitLogisticRegresion
{
    public class MLMathPropagate
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SigmoidShouldBeGreatherThanZeroPointFive()
        {
            double[] w = { 1, 2 };
            double b = 2;
            double[,] X = { { 1, 2, -1 }, { 3, 4, -3.2d } };
            double[] Y = {  1, 0, 1  };

            var sigmoidOutput = MLMath.Propagate(w, b, X, Y);

            //Truncate for proper assertion
            sigmoidOutput.Cost = TruncateDecimals(sigmoidOutput.Cost, 15);
            sigmoidOutput.Grads.dw[0] = TruncateDecimals(sigmoidOutput.Grads.dw[0], 9);
            sigmoidOutput.Grads.dw[1] = TruncateDecimals(sigmoidOutput.Grads.dw[1], 9);
            sigmoidOutput.Grads.db = TruncateDecimals(sigmoidOutput.Grads.db, 15);


            Assert.AreEqual(sigmoidOutput.Cost, 5.801545319394553);

            Assert.AreEqual(sigmoidOutput.Grads.dw[0], 0.99845601);
            Assert.AreEqual(sigmoidOutput.Grads.dw[1], 2.39507239);

            Assert.AreEqual(sigmoidOutput.Grads.db, 0.00145557813678);
        }

        //Based on SO answer : value = Math.Truncate(100 * value) / 100;
        private double TruncateDecimals(double value, int decimals)
        {

            var tenPowerBase = Math.Pow(10, decimals);
            value = Math.Truncate(tenPowerBase * value) / tenPowerBase;

            return value;
        }

    }
}