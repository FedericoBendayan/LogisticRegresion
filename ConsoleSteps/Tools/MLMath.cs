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
            var sigmoid = 1 / (1 + ((Math.Exp(x * (-1)))));
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
        public static PropagateOutput Propagate(double[] w, double b, double[,] X, double[,] Y)
        {
            var output = new PropagateOutput();
            var m = X.GetLength(1);


            //var A = Sigmoid((Dot(Transpose(X), w) + b));

            var A = new double[1, m];
            for (int i = 0; i < m; i++)
            {
                double[] slice = SliceColumn(X,i).ToArray();
                A[0, i] = Sigmoid((Dot(slice, w) +b));
            }

            //Cost

            for (int i = 0; i < Y.GetLength(0); i++)
            {
                output.Cost += Y[0, i] * Math.Log(A[0, i]) + (1 - Y[0, i]) * Math.Log(1 - A[0, i]);
            }

            output.Cost += output.Cost * (-1 / m);

            return output;
        }

        private static double[,] CalculateA(double[,] w, double[,] X)
        {
            //var wT = Transpose(w);

            //var test = wT.
            //for (int i = 0; i < X.GetLength(1); i++)
            //{

            //}
            //sigmoid(np.dot(w.T, X) + b)
            return null;
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

    }

    public class PropagateOutput {
        public double Cost { get; set; }
        public Grads Grads { get; set; }

    }

    public class Grads
    {
        public double dw { get; set; }
        public double db { get; set; }

    }
}
