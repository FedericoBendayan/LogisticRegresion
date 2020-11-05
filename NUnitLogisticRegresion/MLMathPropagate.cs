using ConsoleSteps.Tools;
using NUnit.Framework;
using System;

namespace NUnitLogisticRegresion
{
    public class MLMathPropagate
    {
        double[] w = { 1, 2 };
        double b = 2;
        double[,] X = { { 1, 2, -1 }, { 3, 4, -3.2d } };
        double[] Y = { 1, 0, 1 };

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void PropagateBasic()
        {


            var sigmoidOutput = MLMath.Propagate(w, b, X, Y);

            //Truncate for proper assertion
            sigmoidOutput.Cost = TruncateDecimals(sigmoidOutput.Cost, 15);
            sigmoidOutput.Grads.dw[0] = TruncateDecimals(sigmoidOutput.Grads.dw[0], 8);
            sigmoidOutput.Grads.dw[1] = TruncateDecimals(sigmoidOutput.Grads.dw[1], 6);
            sigmoidOutput.Grads.db = TruncateDecimals(sigmoidOutput.Grads.db, 14);


            Assert.AreEqual(sigmoidOutput.Cost, 5.801545319394553);

            Assert.AreEqual(sigmoidOutput.Grads.dw[0], 0.99845601);
            Assert.AreEqual(sigmoidOutput.Grads.dw[1], 2.395072);

            Assert.AreEqual(sigmoidOutput.Grads.db, 0.00145557813678);
        }
        [Test]
        public void OptimizeBasic()
        {

            var num_iterations = 100;
            var learning_rate = 0.009;
            var print_cost = false;
            var optimizeOutput = MLMath.Optimize(w, b, X, Y, num_iterations, learning_rate, print_cost);

            var bOutput = TruncateDecimals(optimizeOutput.Params.b, 11);
            var wOutput = optimizeOutput.Params.w;
            wOutput[0] = TruncateDecimals(wOutput[0], 7);
            wOutput[1] = TruncateDecimals(wOutput[1], 8);

            var dwOutput = optimizeOutput.Grads.dw;
            dwOutput[0] = TruncateDecimals(dwOutput[0], 8);
            dwOutput[1] = TruncateDecimals(dwOutput[1], 8);

            var dbOutput = TruncateDecimals(optimizeOutput.Grads.db, 11);



            Assert.AreEqual(bOutput, 1.92535983008);
            Assert.AreEqual(wOutput[0], 0.1903359);
            Assert.AreEqual(wOutput[1], 0.12259159);


            Assert.AreEqual(dwOutput[0], 0.67752042);
            Assert.AreEqual(dwOutput[1], 1.41625495);
            Assert.AreEqual(dbOutput, 0.21919450454);

        }

        [Test]
        public void PredictBasic() {

            var w = new double[2] { 0.1124579, 0.23106775 };
            var b = -0.3;
            var X = new double[2,3] { { 1, -1.1, -3.2 }, { 1.2, 2, 0.1 } };

            var predicOutput = MLMath.Predict(w,b,X);

            Assert.AreEqual(predicOutput[0,0], 1);
            Assert.AreEqual(predicOutput[0,1], 1);
            Assert.AreEqual(predicOutput[0,2], 0);
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