using Copier;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Memories.Organizer
{
    public class Helper
    {
        private static List<string> _pictureFileTypes = new List<string> { ".JPG", ".PNG",  };

        private static List<string> _movieFileTypes = new List<string> { ".MOV" };

        public static bool Run()
        {
            FileCollection fileCollection = new FileCollection(@"D:\Media\Photos", @"D:\Media\Photos Organized", String.Empty);

            if (!Directory.Exists(fileCollection.sourcePath))
                return "Source Path does not exist " + fileCollection.sourcePath;

            fileCollection.GetSourceFiles("Memories");

            foreach (string key in fileCollection.fileContainer.Keys)
            {
                FileContainer fileContainer = fileCollection.fileContainer[key];

                SetDateTaken(fileContainer);

                SetDestinationPath(fileContainer, fileCollection.destPath);

                CopyFileToDestination(fileContainer);
            }

            return true;
        }

        private static void SetDateTaken(FileContainer fileContainer)
        {
            if (_pictureFileTypes.Contains(fileContainer.FileSource.Extension))
            {
                fileContainer.FileSource.DateTaken = GetDateTakenFromImage(fileContainer.FileSource);
            }
            else if (_movieFileTypes.Contains(fileContainer.FileSource.Extension))
            {
                fileContainer.FileSource.DateTaken = GetDateTakenFromFileInfo(fileContainer.FileSource.GetFileInfo());
            }
        }

        private static Regex r = new Regex(":");

        public static DateTime GetDateTakenFromImage(FileInfoPlus fileInfoPlus)
        {
            using (FileStream fs = new FileStream(fileInfoPlus.FullName, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                try
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
                catch (Exception exception)
                {
                    return GetDateTakenFromFileInfo(fileInfoPlus.GetFileInfo());
                }
            }
        }

        public static DateTime GetDateTakenFromFileInfo(FileInfo fileInfo)
        {
            DateTime creationTime = fileInfo.CreationTime;
            DateTime lastWriteTime = fileInfo.LastWriteTime;

            if (creationTime < lastWriteTime)
                return creationTime;
            else
                return lastWriteTime;
        }

        public static void SetDestinationPath(FileContainer fileContainer, string destinationPath)
        {
            int year = fileContainer.FileSource.DateTaken.Year;
            int month = fileContainer.FileSource.DateTaken.Month;

            fileContainer.FileSource.DestinationPath = $"{destinationPath}\\{year}\\{month.ToString("00")} - {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month)}\\{fileContainer.FileSource.DateTaken.ToString("yyyy.MM.dd")} - {fileContainer.FileSource.Name}"; // Need destination root director and then from there use date taken to make a year folder, then a month folder, and then place the file there. Also consider renaming files so they display in order.

            if (File.Exists(fileContainer.FileSource.DestinationPath))
            {
                FileInfo destinationFile = new FileInfo(fileContainer.FileSource.DestinationPath);

                fileContainer.FileDestination.SetFileInfo(destinationFile);
            }
        }

        public static void CopyFileToDestination(FileContainer fileContainer)
        {
            if (!fileContainer.FileDestination.Exists)
            {
                string destinationDirectory = fileContainer.FileSource.DestinationPath.Substring(0, fileContainer.FileSource.DestinationPath.LastIndexOf("\\"));

                if (!Directory.Exists(destinationDirectory))
                    Directory.CreateDirectory(destinationDirectory);

                File.Copy(fileContainer.FileSource.FullName, fileContainer.FileSource.DestinationPath, false);
            }
            else
            {
                // Need to decide what to do if the file already exists.
            }
        }
    }
}
