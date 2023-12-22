using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_8_task_3
{
    internal class FoldersSuperviser
    {
        public static string FolderPreparer(DirectoryInfo directory)
        {
            if (directory.GetFiles().Length > 2)
            {
                Exception onlyTwoFilesMustBeInFolder = new("ТОЛЬКО 2 ФАЙЛА ДОЛЖНЫ БЫТЬ В ОРИГИНАЛЬНОЙ ПАПКЕ");
                {
                    throw onlyTwoFilesMustBeInFolder;
                };
            }
            string mainArchivePath = "";
            string excelFilePath = "";

            foreach (var file in directory.GetFiles())
            {
                if(file.Extension == ".zip")
                {
                    mainArchivePath = file.FullName;
                }
                else 
                { 
                    excelFilePath = file.FullName;
                }
            }

            var mainArchive = new FileInfo(mainArchivePath);
            var excelFile = new FileInfo(excelFilePath);
            string currentFlowName = $"{directory.FullName}\\{mainArchive.Name.Remove(mainArchive.Name.Length - 4, 4)}";

            if (!Directory.Exists(currentFlowName))
            {
                directory.CreateSubdirectory($"{mainArchive.Name.Remove(mainArchive.Name.Length - 4, 4)}");

            }

            var currentFlow = new DirectoryInfo(currentFlowName);
            mainArchive.MoveTo($"{currentFlow.FullName}\\{mainArchive.Name}");
            excelFile.MoveTo($"{currentFlow.FullName}\\{excelFile.Name}");
            return currentFlowName;    
        }
        public static bool FolderByNameChecker(DirectoryInfo diInfo, string name)
        {
            return true ? (diInfo.Name == name) : false;
        }
        public static void FolderByNameRemover(DirectoryInfo directory, string name = "__MACOSX", bool needToCheckNested = false)
        {
            var directories = directory.GetDirectories();
            if (!needToCheckNested)
            {
                foreach(var d in  directories) 
                {
                    if (FolderByNameChecker(d, name))
                    {
                        d.Delete(true);
                    }
                }
            }
            else
            {
                foreach (DirectoryInfo d in directories)
                {
                    var subderictories = d.GetDirectories();
                    foreach (DirectoryInfo sub in subderictories)
                    {
                        if (FolderByNameChecker(sub, name))
                        {
                            sub.Delete(true);
                        }
                    }
                }
            }

        }
        public static void EmptyFolderRemover(DirectoryInfo directory)
        {
            if (directory.GetDirectories().Length == 0 && directory.GetFiles().Length == 0)
            {
                directory.Delete(true);
            }
            else
            {
                foreach (var subDirectory in directory.GetDirectories())
                {
                    EmptyFolderRemover(subDirectory);
                }
            }
        }

        public static void FoldersCreator(string pathToMainDirectory)
        {
            var directory = new DirectoryInfo(pathToMainDirectory);
            var txtFlowsList = $"{pathToMainDirectory}\\LIST.txt";

            using StreamReader sr = new StreamReader(txtFlowsList);
            string line;
            string folderName;
            while((line = sr.ReadLine()) != null)
            {   if(line.Length == 1)
                {
                    folderName =($"100{line}");
                }
                else
                {
                    folderName = ($"10{line}");
                }
                directory.CreateSubdirectory(folderName);
            }
        }
    }
}
