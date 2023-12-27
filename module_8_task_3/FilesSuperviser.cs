using module_8_task_2;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace module_8_task_3
{
    internal class FilesSuperviser
    {
        public static bool FileExtensionChecker(FileInfo file, string extension)
        {
            return true ? file.Extension == extension : false;
        }

        public static void FixingUnnecessaryNest(DirectoryInfo currentFlowDirectory)
        {
            var studentsDirectories = currentFlowDirectory.GetDirectories();
            foreach(var studentDirectory in studentsDirectories)
            {
                if(studentDirectory.GetDirectories().Length > 0)
                {
                    foreach(var subDirectory in studentDirectory.GetDirectories())
                    {   if(subDirectory.Name == studentDirectory.Name)
                        {
                            foreach (var subSubDirectory in subDirectory.GetDirectories())
                            {
                                string newName = $"{currentFlowDirectory.FullName}\\{studentDirectory.Name}\\{subSubDirectory.Name}";
                                subSubDirectory.MoveTo(newName);
                            }
                            foreach (FileInfo file in subDirectory.GetFiles())
                            {
                                string newName = $"{currentFlowDirectory.FullName}\\{studentDirectory.Name}\\{file.Name}";
                                file.MoveTo(newName);
                            }
                        }
                    }
                }
            }
        }

        public static void FilesByExtensionMover(DirectoryInfo directory, string extension, string subFolderName)
        {
            foreach(FileInfo file in directory.GetFiles())
            {
                if(FileExtensionChecker(file, extension))
                {   string newName = $"{directory.FullName}\\{subFolderName}\\{file.Name}";
                    file.MoveTo(newName);
                }
            }
        }

        public static void FilesByExtensionRemover(DirectoryInfo directory, string extension, string subFolderName)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                if (FileExtensionChecker(file, extension))
                {
                    file.Delete();
                }
            }
        }
    
        public static bool ArchivesPreparer(DirectoryInfo directory, string nameIsThisDamnMacosx = "__MACOSX", string extension = ".zip")
        {
            bool isOk = true;
            string subfolderForOriginalArchivesName = "Оригинальный архив";
            if (!Directory.Exists($"{directory.FullName}\\{subfolderForOriginalArchivesName}"))
            {
                directory.CreateSubdirectory(subfolderForOriginalArchivesName);
            }

            foreach (var file in directory.GetFiles())
            {
                if(FileExtensionChecker(file, extension))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(file.FullName, directory.FullName);
                        string newMainZipFileName = $"{directory.FullName}\\{subfolderForOriginalArchivesName}\\{file.Name}";
                        file.MoveTo(newMainZipFileName);
                    }
                    catch(Exception ex)
                    {
                        Misc.DisplayErrorMessages(message: $"Файл {file.Name} вызвал ошибку и не разархивировался нормально! Тип ошибки: \n ");
                        Console.WriteLine(ex.GetType());
                        isOk = false;
                    }
                }
            }
            if(isOk)
            {
                FoldersSuperviser.FolderByNameRemover(directory);
            }
            return isOk;
            
        }

        public static void ArchivesExtracter(DirectoryInfo directory, string extension = ".zip")
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
            {   if(FileExtensionChecker(file, extension))
                {
                    string pathToArchive = file.FullName;
                    var tempString = file.Name.Remove(file.Name.Length - 4, 4);
                    try
                    {
                        ZipFile.ExtractToDirectory(pathToArchive, $"{directory.FullName}\\{tempString}");
                    }
                    catch(Exception ex)
                    {
                        Misc.DisplayErrorMessages(message: $"Файл {file.Name} вызвал ошибку и не разархивировался нормально! Тип ошибки: \n ");
                        Console.WriteLine(ex.GetType());
                    }    
                } 
            }
        }

        public static void BeginnigQueryCreator(string pathToDirectory)
        {
            DirectoryInfo directory = new DirectoryInfo(pathToDirectory);
            FileInfo file = new FileInfo($"{directory.FullName}\\query_result_2023-12-19T13_56_41.139749Z.xlsx");
            foreach(var subDir in directory.GetDirectories())
            {
                file.CopyTo($"{subDir.FullName}\\{file.Name}");
            }
        }
    }
}
