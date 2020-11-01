using ConsoleSteps.Tools;
using NUnit.Framework;
using System;

namespace NUnitLogisticRegresion
{
    public class MLMathSigmoid
    {

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void SigmoidShouldBeGreatherThanZeroPointFive(float value)
        {
            var sigmoidOutput = MLMath.Sigmoid(value);

            Assert.IsTrue(sigmoidOutput > 0.5);
        }

        [Test]
        [TestCase(0)]
        public void SigmoidShouldBeZeroPointFive(float value)
        {
            var sigmoidOutput = MLMath.Sigmoid(value);

            Assert.IsTrue(sigmoidOutput == 0.5);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-10)]
        public void SigmoidShouldBeAlmostZero(float value)
        {
            var sigmoidOutput = MLMath.Sigmoid(value);
            Assert.IsTrue(Math.Round(sigmoidOutput) == 0);
        }
    }
}