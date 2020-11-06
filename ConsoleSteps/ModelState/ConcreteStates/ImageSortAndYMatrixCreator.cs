using ConsoleSteps.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleSteps.ModelState.ConcreteStates
{
    /*
 * This class will grab Images/Test & training, rename all files
 * and put them weighted manner again in the same folders.
 * 
 * Motivation: While downloading images, i will have to 
 * rename by hand some baths, and this class helpme to not do
 * that = )
 * 
 */
    public class ImageSortAndYMatrixCreator : AbstractState
    {
        //Relative size of test sample based on total m inputs.
        private const float _TEST_RELATIVE_SIZE = 0.2f;
        string projectDirectory;
        string pathToImagesTraining;
        string pathToImagesTest;
        string pathToImagesTarget;
        string pathToImagesNonTarget;
        string pathToSerializedObjects;

        public ImageSortAndYMatrixCreator()
        {
            projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            pathToImagesTraining = Path.Combine(projectDirectory, @"Images\Training\");
            pathToImagesTest = Path.Combine(projectDirectory, @"Images\Test\");
            pathToImagesTarget = Path.Combine(projectDirectory, @"Images\Target\");
            pathToImagesNonTarget = Path.Combine(projectDirectory, @"Images\NonTarget\");
            pathToSerializedObjects = Path.Combine(projectDirectory, @"SerializedObjects\");
        }

        /*
             * 1)Get DirInfo target
             * 2)Get DirInfo non target
             * 
             * Create LabeledImagesForML which has:
             * source path - source of where it comes, to copy and paste on the future
             * Bool Label- to map yo Y matrix with one or zero 
             * 
             * Create a testList = add _TEST_RELATIVE_SIZE of target and non target LabeledImagesForML
             * Create a traingList= With the rest of LabeledImagesForML
             * 
             * Create y_test with testList.lenght -> Save LabeledImagesForML in order, put same order on y_test matrix
             * Create y_train with traingList.lenght -> Save LabeledImagesForML in order, put same order on y_train matrix
             * 
             */
        public override void Resolve()
        {
            Console.WriteLine();
            Console.WriteLine($"STEP 1 BEGIN: ImageSortAndYMatrixCreator ");

            if (CheckIfProcessingIsNeeded())
            {
                DirectoryInfo nonTargetDirectoryInfo = new DirectoryInfo(pathToImagesNonTarget);

                Queue<LabeledImageForML> targetImagesQueue = CreateTargetImagesList();
                Queue<LabeledImageForML> nonTargetImagesQueue = CreateNonTargetImagesList();

                List<LabeledImageForML> testList = CreateTestImagesList(targetImagesQueue, nonTargetImagesQueue);
                List<LabeledImageForML> traingList = CreateTrainingImagesList(targetImagesQueue, nonTargetImagesQueue);

                //So far both listas are made of targetImages + nonTargetImages IN ORDER, we want them to be UNORDERED for better training.

                var random = new Random();

                testList = testList.OrderBy(a => random.Next()).ToList();
                traingList =traingList.OrderBy(a => random.Next()).ToList();


                SaveTestListAndCreateYTestMatrix(testList);
                SaveTrainingListAndCreateYTrainingMatrix(traingList);
                Console.WriteLine($"We need to create and move images and Y matrixes");
            }
            else
            {
                Console.WriteLine($"We don't need to create and move images and  Y matrixes");
            }

            Console.WriteLine();
            Console.WriteLine($"STEP 1 END: ImageSortAndYMatrixCreator ");
      
            this._context.TransitionTo(new TransformImages());
            this._context.ResolveModelState();
        }

        private bool CheckIfProcessingIsNeeded()
        {
            bool atLeastOneImageTest = false;
            bool atLeastOneImageTraining = false;

            var imageTrainingDirectoryInformationList = Directory.GetFiles(pathToImagesTraining, "*.*", SearchOption.AllDirectories).FirstOrDefault();

            if (imageTrainingDirectoryInformationList == null || imageTrainingDirectoryInformationList.Length == 0)
            {
                return true;
            }

          
            var imageTestDirectoryInformationList = Directory.GetFiles(pathToImagesTest, "*.*", SearchOption.AllDirectories).FirstOrDefault();

            if (imageTestDirectoryInformationList == null || imageTestDirectoryInformationList.Length == 0)
            {
                return true;
            }
           
            return atLeastOneImageTest && atLeastOneImageTraining;
        }

        private void SaveTrainingListAndCreateYTrainingMatrix(List<LabeledImageForML> traingList)
        {
            var testCount = traingList.Count;//for not iterating twice
            var y_training = new double[testCount];

            for (int i = 0; i < testCount; i++)
            {
                File.Move(traingList[i].Path, Path.Combine(pathToImagesTraining, i + ".jpeg"));
                y_training[i] = Convert.ToDouble(traingList[i].Label);
            }

            //TODO make "y_train" a constant please, i had a silly bug becasue i used "y_training".
            SerealizeMatrix(y_training, "y_train");
        }

        private void SaveTestListAndCreateYTestMatrix(List<LabeledImageForML> testList)
        {
            var testCount = testList.Count;//for not iterating twice
            var y_test = new double[testCount];

            for (int i = 0; i < testCount; i++)
            {
                File.Move(testList[i].Path, Path.Combine(pathToImagesTest, i + ".jpeg"));
                y_test[i] = Convert.ToDouble(testList[i].Label);
            }
            //TODO make this a constant also.
            SerealizeMatrix(y_test, "y_test");

        }

        private void SerealizeMatrix(object matrix, string matrixName)
        {
            using (StreamWriter sw = new StreamWriter(pathToSerializedObjects + "\\" + matrixName + ".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, matrix);
            }
        }

        private List<LabeledImageForML> CreateTrainingImagesList(Queue<LabeledImageForML> targetImagesQueue, Queue<LabeledImageForML> nonTargetImagesQueue)
        {
            var trainingImagesList = new List<LabeledImageForML>();

            var count = targetImagesQueue.Count;
            for (int i = 0; i < count; i++)
            {
                trainingImagesList.Add(targetImagesQueue.Dequeue());
            }

            count = nonTargetImagesQueue.Count;
            for (int i = 0; i < count; i++)
            {
                trainingImagesList.Add(nonTargetImagesQueue.Dequeue());
            }

            return trainingImagesList;
        }

        private List<LabeledImageForML> CreateTestImagesList(Queue<LabeledImageForML> targetImagesQueue, Queue<LabeledImageForML> nonTargetImagesQueue)
        {
            var testImagesList = new List<LabeledImageForML>();

            for (int i = 0; i < (int)targetImagesQueue.Count * _TEST_RELATIVE_SIZE; i++)
            {
                testImagesList.Add(targetImagesQueue.Dequeue());
            }

            for (int i = 0; i < (int)nonTargetImagesQueue.Count * _TEST_RELATIVE_SIZE; i++)
            {
                testImagesList.Add(nonTargetImagesQueue.Dequeue());
            }

            return testImagesList;
        }

        private Queue<LabeledImageForML> CreateNonTargetImagesList()
        {
            var nonTargetImagesList = Directory.GetFiles(pathToImagesNonTarget, "*.*", SearchOption.AllDirectories).ToList();

            var queue = new Queue<LabeledImageForML>();

            for (int i = 0; i < nonTargetImagesList.Count; i++)
            {
                queue.Enqueue(new LabeledImageForML { Path = nonTargetImagesList[i], Label = false });
            }

            return queue;
        }

        private Queue<LabeledImageForML> CreateTargetImagesList()
        {
            var targetImagesList = Directory.GetFiles(pathToImagesTarget, "*.*", SearchOption.AllDirectories).ToList();

            var queue = new Queue<LabeledImageForML>();

            for (int i = 0; i < targetImagesList.Count; i++)
            {
                queue.Enqueue(new LabeledImageForML { Path = targetImagesList[i], Label = true });
            }

            return queue;
        }

        public void RenameSecuentallyAllFIlesInFolder()
        {

            DirectoryInfo testDirectoryInfo = new DirectoryInfo(pathToImagesTest);
            DirectoryInfo trainingDirectoryInfo = new DirectoryInfo(pathToImagesTraining);

            var testFiles = testDirectoryInfo.GetFiles();
            var trainingFiles = trainingDirectoryInfo.GetFiles();

            int testLength = (int)(testFiles.Length * _TEST_RELATIVE_SIZE);
            int trainingLength = (int)(trainingFiles.Length * _TEST_RELATIVE_SIZE);

            var testCount = 0;
            var trainingCount = 0;


            //Move some files to testing and others to training from test folder
            for (int i = 0; i < testFiles.Length; i++)
            {
                if (i < testLength)
                {
                    File.Move(testFiles[i].FullName, Path.Combine(pathToImagesTest, System.Guid.NewGuid().ToString() + ".jpeg"));
                    testCount++;
                }
                else
                {
                    File.Move(testFiles[i].FullName, Path.Combine(pathToImagesTraining, System.Guid.NewGuid().ToString() + ".jpeg"));
                    trainingCount++;
                }

            }

            //Move some files to testing and others to training from training folder
            for (int i = 0; i < trainingFiles.Length; i++)
            {
                if (i < trainingLength)
                {
                    File.Move(trainingFiles[i].FullName, Path.Combine(pathToImagesTest, System.Guid.NewGuid().ToString() + ".jpeg"));
                    testCount++;
                }
                else
                {
                    File.Move(trainingFiles[i].FullName, Path.Combine(pathToImagesTraining, System.Guid.NewGuid().ToString() + ".jpeg"));
                    trainingCount++;
                }

            }

        }
    }
}
