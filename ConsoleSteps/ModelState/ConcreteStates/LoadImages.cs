using ConsoleSteps.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleSteps.ModelState.ConcreteStates
{
    // Concrete States implement various behaviors, associated with a state of
    // the Context.
    public class LoadImages : AbstractState
    {
        private string _projectDirectory;
        private string _pathToImagesTraining;
        private string _pathToImagesTest;
        private string _pathToImagesProcessedTraining;
        private string _pathToImagesProcessedTest;


        public LoadImages()
        {
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _pathToImagesTraining = Path.Combine(_projectDirectory, @"Images\Training\");
            _pathToImagesTest = Path.Combine(_projectDirectory, @"Images\Test\");
            _pathToImagesProcessedTraining = Path.Combine(_projectDirectory, @"ProcessedImages\Training\");
            _pathToImagesProcessedTest = Path.Combine(_projectDirectory, @"ProcessedImages\Test\");


        }
        public override void Resolve()
        {
            Console.WriteLine($"LoadImages begin");

            if (CheckIfProcessingIsNeeded())
            {
                Console.WriteLine($"We need to process images...");
                LoadAndProcessImages(_pathToImagesTest, _pathToImagesProcessedTest);
                LoadAndProcessImages(_pathToImagesTraining, _pathToImagesProcessedTraining);
            }
            else {
                Console.WriteLine($"We don't need to process images...");
            }




            
            Console.WriteLine($"LoadImages end");
            this._context.TransitionTo(new CreateMatrixes());
            this._context.ResolveModelState();

        }

        private void LoadAndProcessImages(string originPath, string desinationPath)
        {

            var imagesNameList = Directory.GetFiles(originPath, "*.*", SearchOption.AllDirectories).ToList();

            for (int i = 0; i < imagesNameList.Count; i++)
            {
                using (var image = Image.FromFile(Path.Combine(originPath, imagesNameList[i])))
                {
                    var newImage = ImageResizer.ResizeImage(image, 64, 64);
                    var fileName = Path.GetFileNameWithoutExtension(imagesNameList[i]);
                    var newPath = Path.Combine(desinationPath, fileName + ".bmp");
                    newImage.Save(newPath, ImageFormat.Bmp);
                }
            }
        }

        //Returns true if you need to process images, false otherwise.
        private bool CheckIfProcessingIsNeeded()
        {
            bool atLeastOneProcessedImageTest = false;
            bool atLeastOneProcessedImageTraining = false;

            var processedImageTraining = Directory.GetFiles(_pathToImagesProcessedTraining, "*.*", SearchOption.AllDirectories).FirstOrDefault();

            if (processedImageTraining == null)
            {
                return true;
            }

            using (var image = new Bitmap(Path.Combine(_pathToImagesProcessedTraining, processedImageTraining)))
            {

                if (image.Width == 64 && image.Height == 64)
                {
                    atLeastOneProcessedImageTraining = true;
                }

            }

            var processedImageTest = Directory.GetFiles(_pathToImagesProcessedTest, "*.*", SearchOption.AllDirectories).FirstOrDefault();

            if (processedImageTest == null)
            {
                return true;
            }

            using (var image = new Bitmap(Path.Combine(_pathToImagesProcessedTest, processedImageTest)))
            {

                if (image.Width == 64 && image.Height == 64)
                {
                    atLeastOneProcessedImageTest = true;
                }

            }


            var runAction = atLeastOneProcessedImageTest && atLeastOneProcessedImageTraining;

            return !runAction;
        }

    }
}
