using Copier;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Memories.Organizer
{
    public class Helper
    {
        public static string Run()
        {
            FileCollection fileCollection = new FileCollection(@"D:\Media\Photos", @"D:\Media\Photos Organized", String.Empty);

            if (!Directory.Exists(fileCollection.sourcePath))
                return "Source Path does not exist " + fileCollection.sourcePath;

            fileCollection.GetSourceFiles("Memories");

            foreach (string key in fileCollection.fileContainer.Keys)
            {
                FileContainer fileContainer = fileCollection.fileContainer[key];

                DateTime dateTaken = GetDateTakenFromImage(fileContainer.FileA.FullName);
                //Organize depending on date taken
            }

            return "Done";
        }

        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        //retrieves the datetime WITHOUT loading the whole image
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
    }
}
