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
                double[] y_train = DeserealizeMatrix<double[]>("y_train");
                #region boring fill 
                y_train[0] = 1;
                y_train[1] = 1;
                y_train[2] = 0;
                y_train[3] = 1;
                y_train[4] = 1;
                y_train[5] = 1;
                y_train[6] = 1;
                y_train[7] = 0;
                y_train[8] = 0;
                y_train[9] = 0;
                y_train[10] = 0;

                y_train[11] = 1;
                y_train[12] = 1;
                y_train[13] = 1;
                y_train[14] = 1;
                y_train[15] = 0;
                y_train[16] = 1;
                y_train[17] = 0;
                y_train[18] = 1;
                y_train[19] = 1;
                y_train[20] = 0;

                y_train[21] = 0;
                y_train[22] = 1;
                y_train[23] = 1;
                y_train[24] = 0;
                y_train[25] = 1;
                y_train[26] = 0;
                y_train[27] = 0;
                y_train[28] = 0;
                y_train[29] = 0;
                y_train[30] = 0;

                y_train[31] = 0;
                y_train[32] = 0;
                y_train[33] = 1;
                y_train[34] = 1;
                y_train[35] = 1;
                y_train[36] = 0;
                y_train[37] = 1;
                y_train[38] = 0;
                y_train[39] = 0;
                y_train[40] = 0;

                y_train[41] = 0;
                y_train[42] = 1;
                y_train[43] = 1;
                y_train[44] = 1;
                y_train[45] = 0;
                y_train[46] = 0;
                y_train[47] = 0;
                y_train[48] = 0;
                y_train[49] = 0;

                y_train[50] = 0;
                y_train[51] = 0;
                y_train[52] = 0;
                y_train[53] = 0;
                y_train[54] = 1;
                y_train[55] = 1;
                y_train[56] = 1;
                y_train[57] = 0;
                y_train[58] = 1;
                y_train[59] = 1;
                y_train[60] = 1;

                y_train[61] = 1;
                y_train[62] = 0;
                y_train[63] = 0;
                y_train[64] = 1;
                y_train[65] = 1;
                y_train[66] = 1;
                y_train[67] = 0;
                y_train[68] = 1;
                y_train[69] = 0;
                y_train[70] = 0;

                y_train[71] = 1;
                y_train[72] = 1;
                y_train[73] = 0;
                y_train[74] = 0;
                y_train[75] = 0;
                y_train[76] = 1;
                y_train[77] = 1;
                y_train[78] = 0;
                y_train[79] = 0;
                y_train[80] = 0;

                y_train[81] = 0;
                y_train[82] = 1;
                y_train[83] = 1;
                y_train[84] = 1;
                y_train[85] = 1;
                y_train[86] = 0;
                y_train[87] = 0;
                y_train[88] = 0;
                y_train[89] = 1;
                y_train[90] = 1;

                y_train[91] = 1;
                y_train[92] = 0;
                y_train[93] = 0;
                y_train[94] = 1;
                y_train[95] = 1;
                y_train[96] = 0;
                y_train[97] = 0;
                y_train[98] = 0;
                y_train[99] = 0;
                y_train[100] = 1;

                y_train[101] = 0;
                y_train[102] = 0;
                y_train[103] = 1;
                y_train[104] = 1;
                y_train[105] = 0;
                y_train[106] = 1;
                y_train[107] = 0;
                y_train[108] = 0;
                y_train[109] = 0;
                y_train[110] = 0;

                y_train[111] = 1;
                y_train[112] = 1;
                y_train[113] = 1;
                y_train[114] = 0;
                y_train[115] = 0;
                y_train[116] = 0;
                y_train[117] = 1;
                y_train[118] = 0;
                y_train[119] = 0;
                y_train[120] = 1;

                y_train[121] = 0;
                y_train[122] = 0;
                y_train[123] = 0;
                y_train[124] = 0;
                y_train[125] = 0;
                y_train[126] = 0;
                y_train[127] = 0;
                y_train[128] = 1;
                y_train[129] = 0;
                y_train[130] = 1;

                y_train[131] = 1;
                y_train[132] = 1;
                y_train[133] = 0;
                y_train[134] = 0;
                y_train[135] = 0;
                y_train[136] = 0;
                y_train[137] = 0;
                y_train[138] = 1;
                y_train[139] = 1;
                y_train[140] = 1;

                y_train[141] = 1;
                y_train[142] = 1;
                y_train[143] = 1;
                y_train[144] = 0;
                y_train[145] = 1;
                y_train[146] = 1;
                y_train[147] = 0;
                y_train[148] = 1;
                y_train[149] = 1;
                y_train[150] = 0;

                y_train[151] = 0;
                y_train[152] = 1;
                y_train[153] = 1;
                y_train[154] = 0;
                y_train[155] = 0;
                y_train[156] = 1;
                y_train[157] = 1;
                y_train[158] = 0;
                y_train[159] = 1;
                y_train[160] = 0;

                y_train[161] = 1;
                y_train[162] = 1;
                y_train[163] = 0;
                y_train[164] = 0;
                y_train[165] = 0;
                y_train[166] = 1;
                y_train[167] = 1;
                y_train[168] = 1;
                y_train[169] = 0;
                y_train[170] = 0;

                y_train[171] = 1;
                y_train[172] = 1;
                y_train[173] = 1;
                y_train[174] = 0;
                y_train[175] = 0;
                y_train[176] = 1;
                y_train[177] = 0;
                y_train[178] = 0;
                y_train[179] = 1;
                y_train[180] = 1;

                y_train[181] = 1;
                y_train[182] = 1;
                y_train[183] = 0;
                y_train[184] = 0;
                y_train[185] = 0;
                y_train[186] = 0;
                y_train[187] = 1;
                y_train[188] = 1;
                y_train[189] = 0;
                y_train[190] = 0;

                y_train[191] = 1;
                y_train[192] = 0;
                y_train[193] = 1;
                y_train[194] = 0;
                y_train[195] = 1;
                y_train[196] = 0;
                y_train[197] = 0;
                y_train[198] = 1;


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
                var y_test = DeserealizeMatrix<double[]>("y_test");
                #region boring fill 
                y_test[0] = 1;
                y_test[1] = 0;
                y_test[2] = 0;
                y_test[3] = 1;
                y_test[4] = 1;
                y_test[5] = 0;
                y_test[6] = 1;
                y_test[7] = 0;
                y_test[8] = 1;
                y_test[9] = 1;
                y_test[10] = 0;
                y_test[11] = 1;
                y_test[12] = 0;
                y_test[13] = 0;
                y_test[14] = 0;
                y_test[15] = 1;
                y_test[16] = 1;
                y_test[17] = 0;
                y_test[18] = 1;
                y_test[19] = 0;
                y_test[20] = 0;
                y_test[21] = 0;
                y_test[22] = 0;
                y_test[23] = 0;
                y_test[24] = 1;
                y_test[25] = 0;
                y_test[26] = 1;
                y_test[27] = 0;
                y_test[28] = 0;
                y_test[29] = 0;
                y_test[30] = 0;
                y_test[31] = 0;
                y_test[32] = 0;
                y_test[33] = 0;
                y_test[34] = 1;
                y_test[35] = 1;
                y_test[36] = 1;
                y_test[37] = 1;
                y_test[38] = 0;
                y_test[39] = 1;
                y_test[40] = 1;
                y_test[41] = 1;
                y_test[42] = 0;
                y_test[43] = 1;
                y_test[44] = 1;
                y_test[45] = 1;
                y_test[46] = 0;
                y_test[47] = 1;
                y_test[48] = 0;

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


        //Some matrixes could be integers others may be doubles.
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
