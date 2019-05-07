using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SS.UTILITIES
{
    public static class ImageUtilities
    {
        public static void ResizeImage(string savePath, string fileName, Image image, int maxImgSize, bool createThumb, int maxThumbSize = 0)
        {
            // Get new proportional image dimensions based off current image size and maxImageSize
            int[] newImageSizes = GetNewSize(image.Width, image.Height, maxImgSize);

            // Resize the image to the new dimensions returned from above
            Image newImage = DoResizeImage(newImageSizes[0], newImageSizes[1], image);

            // Save the new image to the specified path with the specified file name
            newImage.Save(savePath + fileName);

            if (createThumb)
            {
                // Calculate the proportional size for the thumbnail based off the maxThumbSize
                int[] newThumbSizes = GetNewSize(newImage.Width, newImage.Height, maxThumbSize);

                // Create the thumbnail image
                Bitmap newThumb = DoResizeImage(newThumbSizes[0], newThumbSizes[1], image);

                // Save the thumbnail image with the t_ prefix
                newThumb.Save(savePath + "t_" + fileName);

                newThumb.Dispose();
            }

            // Clean up our service
            newImage.Dispose();
            image.Dispose();
        }

        private static int[] GetNewSize(int imgWidth, int imgHeight, int maxImgSize)
        {
            // Calculate which dimension is being changed the most and use that as the aspect ratio for both sides
            float ratioX = (float)maxImgSize / (float)imgWidth;
            float ratioY = (float)maxImgSize / (float)imgHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // Calculate the new width and height based on aspect ratio
            int[] newImgSizes = new int[2];
            newImgSizes[0] = (int)(imgWidth * ratio);
            newImgSizes[1] = (int)(imgHeight * ratio);

            // Return the new proportional image sizes
            return newImgSizes;
        }

        private static Bitmap DoResizeImage(int imgWidth, int imgHeight, Image image)
        {
            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(imgWidth, imgHeight, PixelFormat.Format24bppRgb);

            // If the image format supports transparency apply transparency
            newImage.MakeTransparent();

            // Set image to screen resolution of 72 dpi (the resolution of monitors)
            newImage.SetResolution(72, 72);

            // Do the resize
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, imgWidth, imgHeight);
            }

            // Return the resized image
            return newImage;
        }

        public static void Delete(string path)
        {
            // Get info about the specified file
            FileInfo file = new FileInfo(path);

            // Check if the file exists
            if (file.Exists)
            {
                // If so delete the file
                file.Delete();
            }
        }
    }
}
