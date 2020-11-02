using ConsoleSteps.Tools;
using NUnit.Framework;
using System;

namespace NUnitLogisticRegresion
{
    class MLMathMatrixFunctions
    {
        [Test]
        //Example from  https://www.mathsisfun.com/algebra/vectors-cross-product.html
        public void DotBasicTest()
        {
            var x = new double[,] { { 9, 2, 7 } };
            var y = new double[,] { { 4, 8, 10 } };
            var dotOutput = MLMath.Dot(x, y);

            Assert.AreEqual(dotOutput, 122);
        }

        [Test]
        public void DotShouldBeZero()
        {
            var x = new double[,] { { 9, 0, 7 } };
            var y = new double[,] { { 0, 8, 0 } };
            var dotOutput = MLMath.Dot(x, y);

            Assert.AreEqual(dotOutput, 0);
        }

        [Test]
        //Example from https://www.csharpstar.com/create-matrix-and-different-matrix-operations-in-csharp/
        public void TransposeBasicTest()
        {
            var x = new double[,] {
                                    { 7, 2, 9 },
                                    { 1, 6, 3 }
                                  };

            var xT = new double[,] {
                                    { 7, 1},
                                    { 2, 6},
                                    { 9, 3},
                                  };

            var transponseOutput = MLMath.Transpose(x);

            Assert.AreEqual(transponseOutput, xT);
        }

        [Test]
        public void ToSingleBasic()
        {
            double[,] testArray = new double[4, 3]
                                {
                                    {  1,  2,  3 },
                                    {  4,  5,  6 },
                                    {  7,  8,  9 },
                                    { 10, 11, 12 }
                                };

            var toSingleOutput = MLMath.ToSingle(testArray);

            Assert.AreEqual(toSingleOutput, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
        }

    }
}
