using module_8_task_2;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace module_8_task_3
{
    internal class FileRemover
    {
        public static long UpdatedFilesRemover(DirectoryInfo diInfo, out int removedFilesCount)
        {
            long sizeOfRemovedFiles = 0;
            removedFilesCount = 0;
            foreach (DirectoryInfo dir in diInfo.GetDirectories()) 
            {
                sizeOfRemovedFiles = UpdatedFilesRemover(dir, out removedFilesCount);
            }
            foreach(FileInfo fileInfo in diInfo.GetFiles())
            {
                try
                {
                    var interval = DateTime.Now - fileInfo.LastAccessTime;
                    if (interval.TotalMinutes > 30)
                    {
                        Console.WriteLine($"Файл {fileInfo} не использовался {interval.TotalMinutes} минут и был удалён");
                        sizeOfRemovedFiles += fileInfo.Length;
                        fileInfo.Delete();
                        removedFilesCount++;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"{exc.Message}\n");
                }
            }
            return sizeOfRemovedFiles;
        }

        public static bool FolderByNameChecker(DirectoryInfo diInfo, string name)
        {
            return true ? (diInfo.Name == name) : false;   
        }
        public static void FolderByNameRemover(DirectoryInfo directory, string name)
        {
            var directories = directory.GetDirectories();
            foreach (DirectoryInfo d in directories)
            {
                var subderictories = d.GetDirectories();
                foreach(DirectoryInfo sub in subderictories)
                {
                    if(FolderByNameChecker(sub, name))
                    {
                        sub.Delete(true);
                    }
                }
            }
        }

        public static void FileMover(DirectoryInfo mainDirectory)
        {
            var directoriesInStudentFolder = mainDirectory.GetDirectories();
            foreach( DirectoryInfo oneLevelDeeper in directoriesInStudentFolder)
            {
                foreach(DirectoryInfo TwoLevelDeeper in oneLevelDeeper.GetDirectories())
                { 
                    if(TwoLevelDeeper.Name == oneLevelDeeper.Name)
                    {
                        foreach(FileInfo file in TwoLevelDeeper.GetFiles())
                        {
                            string newName = $"{oneLevelDeeper.FullName}\\{file.Name}";
                            file.MoveTo(newName);
                        }
                    }
                }
            }
        }

        public static void EmptyFolderRemover(DirectoryInfo directory)
        {
            if(directory.GetDirectories().Length == 0 && directory.GetFiles().Length == 0)
            {
                directory.Delete(true);
            }
            else
            {
                foreach(var subDirectory in directory.GetDirectories())
                {
                    EmptyFolderRemover (subDirectory);
                }
            }
        }

        public static void FilesByExtensionMover(DirectoryInfo directory, string extension, string subFolderName)
        {
            foreach(FileInfo file in directory.GetFiles())
            {
                if(file.Extension == extension)
                {   string newName = $"{directory.FullName}\\{subFolderName}\\{file.Name}";
                    file.MoveTo(newName);
                }
            }
        }

        public static void ArchivesPreparer(DirectoryInfo directory, string nameIsThisDamnMacosx)
        { 
            var zipfiles = directory.GetFiles();
            var zipfilePath = zipfiles[0].FullName;
            ZipFile.ExtractToDirectory(zipfilePath, directory.FullName);
            foreach(var dir in directory.GetDirectories())
            {
                if(FolderByNameChecker(dir, nameIsThisDamnMacosx))
                {
                    dir.Delete(true);
                }
            }
            string subfolderForOriginalArchivesName = "Оригинальные архивы";
            if (!Directory.Exists($"{directory.FullName}\\{subfolderForOriginalArchivesName}"))
            {
                directory.CreateSubdirectory(subfolderForOriginalArchivesName);
            }
            string newMainZipFileName = $"{directory.FullName}\\{subfolderForOriginalArchivesName}\\{zipfiles[0].Name}";
            zipfiles[0].MoveTo(newMainZipFileName);
        }

        public static void ArchivesExtracter(DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                var tempString = file.Name.Remove(file.Name.Length - 4, 4);
                var dirForStudent = $"{directory.FullName}\\{tempString}";
                if (!Directory.Exists(dirForStudent))
                {
                    directory.CreateSubdirectory(tempString);
                }
            }
            foreach (FileInfo file in directory.GetFiles())
            {
                string pathToArchive = file.FullName;
                var tempString = file.Name.Remove(file.Name.Length - 4, 4);
                ZipFile.ExtractToDirectory (pathToArchive, $"{directory.FullName}\\{tempString}");
            }
        }

        public static string FolderPreparer(DirectoryInfo directory) 
        {
            if (directory.GetFiles().Length > 1)
            {
                Exception onlyOneFileMustBeInFolder = new("НЕ БОЛЬШЕ ОДНОГО ФАЙЛА");
                {
                    throw onlyOneFileMustBeInFolder;
                    
                };
                
            }
            else
            {
                var mainArchive = directory.GetFiles()[0];
                string currentFlowName = $"{directory.FullName}\\{mainArchive.Name.Remove(mainArchive.Name.Length - 4, 4)}";
                if (!Directory.Exists(currentFlowName))
                {
                    directory.CreateSubdirectory($"{mainArchive.Name.Remove(mainArchive.Name.Length - 4, 4)}");
                    
                }
                var currentFlow = new DirectoryInfo(currentFlowName);
                mainArchive.MoveTo($"{currentFlow.FullName}\\{mainArchive.Name}");
                return currentFlowName;
            }

        }
    }
}
