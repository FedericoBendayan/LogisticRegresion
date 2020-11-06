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
    //This class does not handle the step of filling y_train and y_test
    //I should think a better way of doing it.
    public class CreateMatrixes : AbstractState
    {
        private string _projectDirectory;
        private string _pathToSerializedObjects;

        private int amountOfPixels = (64 * 64 * 3);

        public CreateMatrixes()
        {
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _pathToSerializedObjects = Path.Combine(_projectDirectory, @"SerializedObjects\");
            //_pathToXtrain = Path.Combine(_pathToSerializedObjects, @"x_train");
        }
        public override void Resolve()
        {

            Console.WriteLine();
            Console.WriteLine($"STEP 3 BEGIN: CreateMatrixes ");

            if (CheckIfProcessingIsNeeded())
            {
                var x_train = CreateXMatrix(@"ProcessedImages\Training\");
                var x_test = CreateXMatrix(@"ProcessedImages\Test\");

                SerealizeMatrix(x_train, "x_train");
                SerealizeMatrix(x_test, "x_test");

                Console.WriteLine($"We created the four matrixes");
            }
            else {
                Console.WriteLine($"We don't need to create matrixes");
            }


            Console.WriteLine();
            Console.WriteLine($"STEP 3 END: CreateMatrixes ");

            this._context.TransitionTo(new TrainModel());
            this._context.ResolveModelState();
        }

        private void SerealizeMatrix(object matrix, string matrixName)
        {
            using (StreamWriter sw = new StreamWriter(_pathToSerializedObjects + "\\" + matrixName + ".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, matrix);
            }
        }


        //Some matrixes could be integers others may be doubles.
        private T DeserealizeMatrix<T>(string matrixName)
        {
            using (StreamReader file = File.OpenText(_pathToSerializedObjects + "\\" + matrixName))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(file, typeof(T));
            }
        }

        private double[,] CreateXMatrix(string environmentPath)
        {
            List<double[,]> returnList = new List<double[,]>();
            var _pathToImagesProcessedTraining = Path.Combine(_projectDirectory, environmentPath);

            // Project highlight: idk about performance, but code works!. It sorts the files 0,1,2,..,199 .
            var imagesNameList = Directory.GetFiles(_pathToImagesProcessedTraining, "*.*", SearchOption.AllDirectories).OrderBy(name => Convert.ToInt32(Path.GetFileNameWithoutExtension(name))).ToList();

            var x_maxtrix = new double[amountOfPixels, imagesNameList.Count];

            for (int z = 0; z < imagesNameList.Count; z++)
            {
                var count = 0;
                using (var img = new Bitmap(imagesNameList[z]))
                {
                    for (int i = 0; i < img.Width; i++)
                    {
                        for (int j = 0; j < img.Height; j++)
                        {
                            //We divide by 255 to notmalize the data.
                            Color pixel = img.GetPixel(i, j);
                            x_maxtrix[count, z] = (double) pixel.R / 255;
                            count++;
                            x_maxtrix[count, z] = (double) pixel.G / 255;
                            count++;
                            x_maxtrix[count, z] = (double) pixel.B / 255;
                            count++;
                        }

                    }
                }
            }

            return x_maxtrix;

        }

        //If we have 4 serialized matrixes, we don't need to reprocess all images.
        private bool CheckIfProcessingIsNeeded()
        {

            var matrixesFiles = Directory.GetFiles(_pathToSerializedObjects, "*.*", SearchOption.AllDirectories).ToList();

            if (matrixesFiles.Count == 4)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
