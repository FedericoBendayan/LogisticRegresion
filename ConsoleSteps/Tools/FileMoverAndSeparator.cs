using System;
using System.IO;

namespace ConsoleSteps.Tools
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

    public class FileMoverAndSeparator
    {
        //Relative size of test sample based on total m inputs.
        private const float _TEST_RELATIVE_SIZE = 0.2f;

        public static void RenameSecuentallyAllFIlesInFolder()
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var pathToImagesTraining = Path.Combine(projectDirectory, @"Images\Training\");
            var pathToImagesTest = Path.Combine(projectDirectory, @"Images\Test\");

            DirectoryInfo testDirectoryInfo = new DirectoryInfo(pathToImagesTest);
            DirectoryInfo trainingDirectoryInfo = new DirectoryInfo(pathToImagesTraining);

            var testFiles = testDirectoryInfo.GetFiles();
            var trainingFiles = trainingDirectoryInfo.GetFiles();

            int testLength = (int) (testFiles.Length * _TEST_RELATIVE_SIZE);
            int trainingLength = (int) (trainingFiles.Length * _TEST_RELATIVE_SIZE);

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
                else {
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
