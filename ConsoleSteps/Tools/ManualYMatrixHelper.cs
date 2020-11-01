using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleSteps.Tools
{
    public class ManualYMatrixHelper
    {
        private string _projectDirectory;
        private string _pathToSerializedObjects;

        public ManualYMatrixHelper()
        {
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _pathToSerializedObjects = Path.Combine(_projectDirectory, @"SerializedObjects\");
        }

        public static void Resolve() {

            FillYTest();
            FillYTrain();
        
        }

        public static void FillYTrain()
        {
            try
            {
                float[,] y_train = DeserealizeMatrix<float[,]>("y_train");
                #region boring fill 
                y_train[0,0] = 1;
                y_train[0,1] = 1;
                y_train[0,2] = 0;
                y_train[0,3] = 1;
                y_train[0,4] = 1;
                y_train[0,5] = 1;
                y_train[0,6] = 1;
                y_train[0,7] = 0;
                y_train[0,8] = 0;
                y_train[0,9] = 0;
                y_train[0,10] = 0;

                y_train[0,11] = 1;
                y_train[0,12] = 1;
                y_train[0,13] = 1;
                y_train[0,14] = 1;
                y_train[0,15] = 0;
                y_train[0,16] = 1;
                y_train[0,17] = 0;
                y_train[0,18] = 1;
                y_train[0,19] = 1;
                y_train[0,20] = 0;

                y_train[0,21] = 0;
                y_train[0,22] = 1;
                y_train[0,23] = 1;
                y_train[0,24] = 0;
                y_train[0,25] = 1;
                y_train[0,26] = 0;
                y_train[0,27] = 0;
                y_train[0,28] = 0;
                y_train[0,29] = 0;
                y_train[0,30] = 0;

                y_train[0,31] = 0;
                y_train[0,32] = 0;
                y_train[0,33] = 1;
                y_train[0,34] = 1;
                y_train[0,35] = 1;
                y_train[0,36] = 0;
                y_train[0,37] = 1;
                y_train[0,38] = 0;
                y_train[0,39] = 0;
                y_train[0,40] = 0;

                y_train[0,41] = 0;
                y_train[0,42] = 1;
                y_train[0,43] = 1;
                y_train[0,44] = 1;
                y_train[0,45] = 0;
                y_train[0,46] = 0;
                y_train[0,47] = 0;
                y_train[0,48] = 0;
                y_train[0,49] = 0;

                y_train[0,50] = 0;
                y_train[0,51] = 0;
                y_train[0,52] = 0;
                y_train[0,53] = 0;
                y_train[0,54] = 1;
                y_train[0,55] = 1;
                y_train[0,56] = 1;
                y_train[0,57] = 0;
                y_train[0,58] = 1;
                y_train[0,59] = 1;
                y_train[0,60] = 1;

                y_train[0,61] = 1;
                y_train[0,62] = 0;
                y_train[0,63] = 0;
                y_train[0,64] = 1;
                y_train[0,65] = 1;
                y_train[0,66] = 1;
                y_train[0,67] = 0;
                y_train[0,68] = 1;
                y_train[0,69] = 0;
                y_train[0,70] = 0;

                y_train[0,71] = 1;
                y_train[0,72] = 1;
                y_train[0,73] = 0;
                y_train[0,74] = 0;
                y_train[0,75] = 0;
                y_train[0,76] = 1;
                y_train[0,77] = 1;
                y_train[0,78] = 0;
                y_train[0,79] = 0;
                y_train[0,80] = 0;

                y_train[0,81] = 0;
                y_train[0,82] = 1;
                y_train[0,83] = 1;
                y_train[0,84] = 1;
                y_train[0,85] = 1;
                y_train[0,86] = 0;
                y_train[0,87] = 0;
                y_train[0,88] = 0;
                y_train[0,89] = 1;
                y_train[0,90] = 1;

                y_train[0,91] = 1;
                y_train[0,92] = 0;
                y_train[0,93] = 0;
                y_train[0,94] = 1;
                y_train[0,95] = 1;
                y_train[0,96] = 0;
                y_train[0,97] = 0;
                y_train[0,98] = 0;
                y_train[0,99] = 0;
                y_train[0,100] = 1;

                y_train[0,101] = 0;
                y_train[0,102] = 0;
                y_train[0,103] = 1;
                y_train[0,104] = 1;
                y_train[0,105] = 0;
                y_train[0,106] = 1;
                y_train[0,107] = 0;
                y_train[0,108] = 0;
                y_train[0,109] = 0;
                y_train[0,110] = 0;

                y_train[0,111] = 1;
                y_train[0,112] = 1;
                y_train[0,113] = 1;
                y_train[0,114] = 0;
                y_train[0,115] = 0;
                y_train[0,116] = 0;
                y_train[0,117] = 1;
                y_train[0,118] = 0;
                y_train[0,119] = 0;
                y_train[0,120] = 1;

                y_train[0,121] = 0;
                y_train[0,122] = 0;
                y_train[0,123] = 0;
                y_train[0,124] = 0;
                y_train[0,125] = 0;
                y_train[0,126] = 0;
                y_train[0,127] = 0;
                y_train[0,128] = 1;
                y_train[0,129] = 0;
                y_train[0,130] = 1;

                y_train[0,131] = 1;
                y_train[0,132] = 1;
                y_train[0,133] = 0;
                y_train[0,134] = 0;
                y_train[0,135] = 0;
                y_train[0,136] = 0;
                y_train[0,137] = 0;
                y_train[0,138] = 1;
                y_train[0,139] = 1;
                y_train[0,140] = 1;

                y_train[0,141] = 1;
                y_train[0,142] = 1;
                y_train[0,143] = 1;
                y_train[0,144] = 0;
                y_train[0,145] = 1;
                y_train[0,146] = 1;
                y_train[0,147] = 0;
                y_train[0,148] = 1;
                y_train[0,149] = 1;
                y_train[0,150] = 0;

                y_train[0,151] = 0;
                y_train[0,152] = 1;
                y_train[0,153] = 1;
                y_train[0,154] = 0;
                y_train[0,155] = 0;
                y_train[0,156] = 1;
                y_train[0,157] = 1;
                y_train[0,158] = 0;
                y_train[0,159] = 1;
                y_train[0,160] = 0;

                y_train[0,161] = 1;
                y_train[0,162] = 1;
                y_train[0,163] = 0;
                y_train[0,164] = 0;
                y_train[0,165] = 0;
                y_train[0,166] = 1;
                y_train[0,167] = 1;
                y_train[0,168] = 1;
                y_train[0,169] = 0;
                y_train[0,170] = 0;

                y_train[0,171] = 1;
                y_train[0,172] = 1;
                y_train[0,173] = 1;
                y_train[0,174] = 0;
                y_train[0,175] = 0;
                y_train[0,176] = 1;
                y_train[0,177] = 0;
                y_train[0,178] = 0;
                y_train[0,179] = 1;
                y_train[0,180] = 1;

                y_train[0,181] = 1;
                y_train[0,182] = 1;
                y_train[0,183] = 0;
                y_train[0,184] = 0;
                y_train[0,185] = 0;
                y_train[0,186] = 0;
                y_train[0,187] = 1;
                y_train[0,188] = 1;
                y_train[0,189] = 0;
                y_train[0,190] = 0;

                y_train[0,191] = 1;
                y_train[0,192] = 0;
                y_train[0,193] = 1;
                y_train[0,194] = 0;
                y_train[0,195] = 1;
                y_train[0,196] = 0;
                y_train[0,197] = 0;
                y_train[0,198] = 1;


                #endregion
                SerealizeMatrix(y_train, "y_train");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
                throw;
            }
        }

        private static void FillYTest()
        {
            try
            {
                var y_test = DeserealizeMatrix<float[,]>("y_test");
                #region boring fill 
                y_test[0,0] = 1;
                y_test[0,1] = 0;
                y_test[0,2] = 0;
                y_test[0,3] = 1;
                y_test[0,4] = 1;
                y_test[0,5] = 0;
                y_test[0,6] = 1;
                y_test[0,7] = 0;
                y_test[0,8] = 1;
                y_test[0,9] = 1;
                y_test[0,10] = 0;
                y_test[0,11] = 1;
                y_test[0,12] = 0;
                y_test[0,13] = 0;
                y_test[0,14] = 0;
                y_test[0,15] = 1;
                y_test[0,16] = 1;
                y_test[0,17] = 0;
                y_test[0,18] = 1;
                y_test[0,19] = 0;
                y_test[0,20] = 0;
                y_test[0,21] = 0;
                y_test[0,22] = 0;
                y_test[0,23] = 0;
                y_test[0,24] = 1;
                y_test[0,25] = 0;
                y_test[0,26] = 1;
                y_test[0,27] = 0;
                y_test[0,28] = 0;
                y_test[0,29] = 0;
                y_test[0,30] = 0;
                y_test[0,31] = 0;
                y_test[0,32] = 0;
                y_test[0,33] = 0;
                y_test[0,34] = 1;
                y_test[0,35] = 1;
                y_test[0,36] = 1;
                y_test[0,37] = 1;
                y_test[0,38] = 0;
                y_test[0,39] = 1;
                y_test[0,40] = 1;
                y_test[0,41] = 1;
                y_test[0,42] = 0;
                y_test[0,43] = 1;
                y_test[0,44] = 1;
                y_test[0,45] = 1;
                y_test[0,46] = 0;
                y_test[0,47] = 1;
                y_test[0,48] = 0;

                #endregion
                SerealizeMatrix(y_test, "y_test");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : "+ ex.Message);
                throw;
            }
        }

        private static void SerealizeMatrix(object matrix, string matrixName)
        {
            var _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var _pathToSerializedObjects = Path.Combine(_projectDirectory, @"SerializedObjects\");

            using (StreamWriter sw = new StreamWriter(_pathToSerializedObjects + "\\" + matrixName + ".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, matrix);
            }
        }


        //Some matrixes could be integers others may be floats.
        private static T DeserealizeMatrix<T>(string matrixName)
        {
            var _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var _pathToSerializedObjects = Path.Combine(_projectDirectory, @"SerializedObjects\");

            using (StreamReader file = File.OpenText(_pathToSerializedObjects + "\\" + matrixName+ ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(file, typeof(T));
            }
        }
    }
}
