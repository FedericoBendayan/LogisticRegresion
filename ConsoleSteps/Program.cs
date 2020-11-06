using ConsoleSteps.ModelState;
using ConsoleSteps.ModelState.ConcreteStates;
using ConsoleSteps.Tools;
using System;

namespace ConsoleSteps
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Project TODO:
             * FLex State Pattern : in progress
             * Flex Singleton (for paths) : to do.
             * NICE TO HAVE: common prompt output messages, it's a little bit slopy or clunky.
             */


            /*TODO:
             * Get images. Done
             * Procress them and leave them as 64 * 64 bmp file. Done
             * Create maxitr x_train = [64*64*3][m] (m = amount of images for training)
             * Create matrix y_train = [1][m]. With 0=not a cat, 1=cat
             * Create matrix x_test same as x_train, but with the images for testing
             * Create matrix y_test same as y_train, but with the images for testing
             * Ecualize values (divide every cell in x matrixes by 255 )
            */
            //GetImages();
            //OpenOneImage();

            //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string pathToImagesTraining = Path.Combine(projectDirectory, @"ProcessedImages\Training\");
            //string pathToImagesTest = Path.Combine(projectDirectory, @"ProcessedImages\Test\");

            //Uncoment for a new batch processing 
            //FileMoverAndSeparator.RenameSecuentallyAllFIlesInFolder();

            //TODO: replace this awfull method for a better approach.
            // This (awfull) method will fill the Y test and train matrixes load by hand (omg)
            //ManualYMatrixHelper.Resolve();


            var context = new Context(new ImageSortAndYMatrixCreator());
            context.ResolveModelState();

            Console.WriteLine("###############");
            Console.WriteLine("ENDING PROGRAM");
            Console.WriteLine("###############");
            Console.ReadKey();
            Environment.Exit(0);

        }


    }
}
