using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ConsoleSteps.Tools
{
    /// <summary>
    /// Contains mathematical functions to help the ML process
    /// </summary>
    /// <remarks>
    /// This class can calculate Sigmoid,
    /// </remarks>
    public static class MLMath
    {
        /// <summary>
        /// Calculates the sigmoid function value for a given float.
        /// </summary>
        /// <returns>
        /// A float number between {0,1} (almost zero and almost one, but never 0 or 1),
        /// representing the y axis.
        /// </returns>
        /// <param name="x">A float precision number representing the x axis.</param>
        public static double Sigmoid(double x)
        {
            var sigmoid = 1 / (1 + Math.Exp(x * (-1)));
            return sigmoid;
        }


        //Implement the cost function and its gradient for the propagation explained above
        //Arguments:
        //w -- weights, a numpy array of size(num_px* num_px * 3, 1)
        //b -- bias, a scalar
        //X -- data of size(num_px* num_px * 3, number of examples)
        //Y -- true "label" vector(containing 0 if non-cat, 1 if cat) of size(1, number of examples)
        //Return:
        //cost -- negative log-likelihood cost for logistic regression
        //dw -- gradient of the loss with respect to w, thus same shape as w
        //db -- gradient of the loss with respect to b, thus same shape as b
        //Tips:
        //- Write your code step by step for the propagation.np.log(), np.dot()
        public static PropagateOutput Propagate(double[] w, double b, double[,] X, double[] Y)
        {
            var output = new PropagateOutput();
            output.Grads = new Grads();
            var m = X.GetLength(1);


            //var A = Sigmoid((Dot(Transpose(X), w) + b));

            var A = new double[1, m];
            for (int i = 0; i < m; i++)
            {
                double[] slice = SliceColumn(X, i).ToArray();
                A[0, i] = Sigmoid((Dot(slice, w) + b)/m);
            }

            // A to list:[[0.9998766054240137, 0.9999938558253978, 0.004496273160941178]]

            //Cost
            for (int i = 0; i < Y.GetLength(0); i++)
            {
                output.Cost += Y[i] * Math.Log(A[0, i]) + (1 - Y[i]) * Math.Log(1 - A[0, i]);
            }

            output.Cost = (-1) * (output.Cost / m);

            output.Grads.dw = new double[X.GetLength(0)];
            double[] yArray = Subtract(SliceRow(A, 0).ToArray(), Y);
            for (int i = 0; i < X.GetLength(0); i++)
            {
                double[] xArray = SliceRow(X, i).ToArray();

                output.Grads.dw[i] = Dot(xArray, yArray);
                output.Grads.dw[i] = output.Grads.dw[i] / m;
            }

            //double[] yArray = Subtract(SliceRow(A, 0).ToArray(), Y);
            for (int i = 0; i < m; i++)
            {
                output.Grads.db += yArray[i];
            }
            output.Grads.db = output.Grads.db / m;




            return output;
        }

        public static double[,] Transpose(double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        public static double Dot(double[,] x, double[,] y)
        {
            double dotProduct = 0;

            int width = x.GetLength(1);
            int heigh = x.GetLength(0);

            for (int i = 0; i < heigh; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //TODO : validate dimensions or will explode

                    dotProduct += x[i, j] * y[i, j];

                }
            }


            return dotProduct;
        }

        public static double[,] Subtract(double[,] x, double[,] y)
        {
            double[,] output = new double[x.GetLength(0), x.GetLength(1)];

            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    output[i, j] = x[i, j] - y[i, j];
                }
            }

            return output;
        }

        public static double[] Subtract(double[] x, double[] y)
        {
            double[] output = new double[x.Length];


            for (int j = 0; j < x.Length; j++)
            {
                output[j] = x[j] - y[j];
            }


            return output;
        }

        public static double[,] Multiply(double[,] matrix, double constant)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[w, h];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[i, j] = matrix[i, j] * constant;
                }
            }

            return result;
        }

        public static double[] Multiply(double[] matrix, double constant)
        {

            double[] result = new double[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {

                result[i] = matrix[i] * constant;

            }

            return result;
        }

        public static double Dot(double[] x, double[] y)
        {
            double dotProduct = 0;


            for (int j = 0; j < x.Length; j++)
            {
                //TODO : validate dimensions or will explode

                dotProduct += x[j] * y[j];

            }



            return dotProduct;
        }

        public static double[] ToSingle(double[,] matrix)
        {
            double[] output = new double[matrix.Length];
            var index = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    output[index] = matrix[i, j];
                    index++;
                }

            }

            return output;
        }

        public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
        {
            for (var i = array.GetLowerBound(1); i <= array.GetUpperBound(1); i++)
            {
                yield return array[row, i];
            }
        }

        public static IEnumerable<T> SliceColumn<T>(this T[,] array, int column)
        {
            for (var i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
            {
                yield return array[i, column];
            }
        }

        //    This function optimizes w and b by running a gradient descent algorithm
        //Arguments:
        //w -- weights, a numpy array of size(num_px* num_px * 3, 1)
        //b -- bias, a scalar
        //X -- data of shape(num_px* num_px * 3, number of examples)
        //Y -- true "label" vector(containing 0 if non-cat, 1 if cat), of shape(1, number of examples)
        //num_iterations -- number of iterations of the optimization loop
        //learning_rate -- learning rate of the gradient descent update rule
        //print_cost -- True to print the loss every 100 steps

        //Returns:
        //params -- dictionary containing the weights w and bias b
        //grads -- dictionary containing the gradients of the weights and bias with respect to the cost function
        //costs -- list of all the costs computed during the optimization, this will be used to plot the learning curve.

        //Tips:
        //You basically need to write down two steps and iterate through them:
        //    1) Calculate the cost and the gradient for the current parameters.Use propagate().
        //    2) Update the parameters using gradient descent rule for w and b.

        public static OptimizeOutput Optimize(double[] w, double b, double[,] X, double[] Y, int num_iterations, double learning_rate, bool print_cost = false)
        {
            var costList = new List<double>();
            var optimizeOutput = new OptimizeOutput();
            var propagateOutput = new PropagateOutput();
            for (int i = 0; i < num_iterations; i++)
            {
                 propagateOutput = Propagate(w, b, X, Y);

                //Update weights
                // w = w -  learning_rate * dw
                w = Subtract(w, Multiply(propagateOutput.Grads.dw, learning_rate));

                //b = b - learning_rate * db
                b = b - learning_rate * propagateOutput.Grads.db;

                if (i % 100 == 0)
                {
                    costList.Add(propagateOutput.Cost);
                    if (print_cost)
                    {
                        //var Y_prediction_test = Predict(w, b, X_test);
                        //var Y_prediction_train = Predict(w, b, X_train);
                        //var test_accuracy = 100 - MeanAbsolutePercentage(ToSingle(Y_prediction_test), Y_test);
                        //var train_accuracy = 100 - MeanAbsolutePercentage(ToSingle(Y_prediction_train), Y_train);

                        // Print train/test Errors
                        //Console.WriteLine($"train accuracy: {train_accuracy} %");
                        //Console.WriteLine($"test accuracy: {test_accuracy} %");
                        Console.WriteLine($"Cost after iteration {i}: {propagateOutput.Cost}");
                    }
                }

            }
            optimizeOutput.Costs = costList.ToArray();
            optimizeOutput.Grads = propagateOutput.Grads;
            optimizeOutput.Params = new Params(w, b);
            return optimizeOutput;
        }

        //        '''
        //Predict whether the label is 0 or 1 using learned logistic regression parameters(w, b)
        //Arguments:
        //w -- weights, a numpy array of size(num_px* num_px * 3, 1)
        //b -- bias, a scalar
        //X -- data of size(num_px* num_px * 3, number of examples)
        //Returns:
        //Y_prediction -- a numpy array(vector) containing all predictions(0/1) for the examples in X
        //'''
        public static double[,] Predict(double[] w, double b, double[,] X)
        {
            var m = X.GetLength(1);
            var Y_prediction = new double[1, m];

            // Compute vector "A" predicting the probabilities of a cat being present in the picture
            //A = sigmoid(np.dot(w.T, X) + b)

            var A = new double[1, m];
            for (int i = 0; i < m; i++)
            {
                double[] slice = SliceColumn(X, i).ToArray();
                A[0, i] = Sigmoid((Dot(slice, w) + b));
            }

            for (int i = 0; i < A.GetLength(1); i++)
            {
                if (A[0, i] > 0.5)
                {
                    Y_prediction[0, i] = 1;
                }
                else {
                    Y_prediction[0, i] = 0;
                }
            }
            return Y_prediction;
        }


        //    """
        //Builds the logistic regression model by calling the function you've implemented previously


        //Arguments:
        //X_train -- training set represented by a numpy array of shape(num_px* num_px * 3, m_train)
        //Y_train -- training labels represented by a numpy array(vector) of shape(1, m_train)
        //X_test -- test set represented by a numpy array of shape(num_px* num_px * 3, m_test)
        //Y_test -- test labels represented by a numpy array(vector) of shape(1, m_test)
        //num_iterations -- hyperparameter representing the number of iterations to optimize the parameters
        //learning_rate -- hyperparameter representing the learning rate used in the update rule of optimize()
        //print_cost -- Set to true to print the cost every 100 iterations

        //Returns:
        //d -- dictionary containing information about the model.
        //"""
        public static ModelOutput Model(
            double[,] X_train,
            double[] Y_train,
            double[,] X_test,
            double[] Y_test,
            int num_iterations = 16000,
            double learning_rate = 1.5,
            bool print_cost = true)
        {
            var w = createWMatrixRandom(64 * 64 * 3);
            double b = 0;

            var optimizeOutput = Optimize(w, b, X_train, Y_train, num_iterations, learning_rate, print_cost);

            w = optimizeOutput.Params.w;
            b = optimizeOutput.Params.b;

            var Y_prediction_test = Predict(w, b, X_test);
            var Y_prediction_train = Predict(w, b, X_train);


            var modelOutput = new ModelOutput();
            modelOutput.Cost = optimizeOutput.Costs;
            modelOutput.Y_prediction_test = Y_prediction_test;
            modelOutput.Y_prediction_train = Y_prediction_train;
            modelOutput.w = w;
            modelOutput.b = b;
            modelOutput.learning_rate = learning_rate;
            modelOutput.num_iterations = num_iterations;

            modelOutput.test_accuracy = 100-MeanAbsolutePercentage(ToSingle(Y_prediction_test), Y_test);
            modelOutput.train_accuracy = 100-MeanAbsolutePercentage(ToSingle(Y_prediction_train), Y_train);

            // Print train/test Errors
            Console.WriteLine($"train accuracy: {modelOutput.train_accuracy} %");
            Console.WriteLine($"test accuracy: {modelOutput.test_accuracy} %");

            return modelOutput;
        }
        private static double[] createWMatrixRandom(int length) {

            var w = new double[length];
            var rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                w[i] = rnd.NextDouble() * (2)  - 1;
            }
            return w;
        }
        public static double MeanAbsolutePercentage(double[] x, double[] y)
        {
            double output = 0;


            for (int j = 0; j < x.Length; j++)
            {
                output += Math.Abs(x[j] - y[j]);
            }

            output = (output / x.Length) * 100;

            return output;
        }

    }

    public class ModelOutput
    {
        public double[] Cost { get; set; }

        public double[,] Y_prediction_test { get; set; }
        public double[,] Y_prediction_train { get; set; }

        public double[] w { get; set; }
        public double b { get; set; }
        public double learning_rate { get; set; }
        public double train_accuracy { get; set; }
        public double test_accuracy { get; set; }
        public int num_iterations { get; set; }

    }
    public class PropagateOutput
    {
        public double Cost { get; set; }
        public Grads Grads { get; set; }

    }

    public class OptimizeOutput
    {
        public double[] Costs { get; set; }
        public Grads Grads { get; set; }

        public Params Params { get; set; }

        public OptimizeOutput()
        {
            this.Grads = new Grads();
            this.Params = new Params();
            this.Costs = new double[0];
        }


    }

    public class Params
    {
        public double[] w { get; set; }
        public double b { get; set; }

        public Params()
        {

        }
        public Params(double[] w, double b)
        {
            this.w = w;
            this.b = b;
        }
    }

    public class Grads
    {
        public double[] dw { get; set; }
        public double db { get; set; }

    }
}
