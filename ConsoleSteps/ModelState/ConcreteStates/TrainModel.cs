using ConsoleSteps.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleSteps.ModelState.ConcreteStates
{
    public class TrainModel : AbstractState
    {
        private string _projectDirectory;
        private string _pathToSerializedObjects;

        public TrainModel()
        {
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _pathToSerializedObjects = Path.Combine(_projectDirectory, @"SerializedObjects\");
        }
        public override void Resolve()
        {
            var matrixesFiles = Directory.GetFiles(_pathToSerializedObjects, "*.*", SearchOption.AllDirectories).ToList();

            Console.WriteLine();
            Console.WriteLine($"STEP 4 BEGIN: TrainModel ");
            //TODO move logic to "CheckIfprocessIsNeeded method, like the other states
            if (matrixesFiles.Count == 4)
            {
                Console.WriteLine("We can process");
                var x_test = DeserealizeMatrix<double[,]>("x_test");
                var x_train = DeserealizeMatrix<double[,]>("x_train");
                var y_test = DeserealizeMatrix<double[]>("y_test");
                var y_train = DeserealizeMatrix<double[]>("y_train");

                var modelOutput = MLMath.Model(x_train, y_train, x_test, y_test);

                Console.WriteLine("Done with the model");
                JsonSerealize(modelOutput, "modelOutput",Formatting.Indented);


            }
            else
            {
                //We can't process
                Console.WriteLine("We can't process");

            }
            Console.WriteLine();
            Console.WriteLine($"STEP 4 END: TrainModel ");
        }

        //Some matrixes could be integers others may be floats.
        private T DeserealizeMatrix<T>(string matrixName)
        {
            using (StreamReader file = File.OpenText(_pathToSerializedObjects + "\\" + matrixName + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(file, typeof(T));
            }
        }

        private void JsonSerealize(object matrix, string matrixName, Formatting formatting = Formatting.None)
        {
            using (StreamWriter sw = new StreamWriter(_pathToSerializedObjects + "\\" + matrixName + ".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = formatting;
                serializer.Serialize(writer, matrix);
            }
        }
    }
}
