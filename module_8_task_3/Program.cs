using module_8;
using module_8_task_2;
using System.Runtime;

namespace module_8_task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /* код для задания 8.3
            Console.WriteLine("Введите путь до папки, которую нужно очистить: ");
            string path = Console.ReadLine();  
            if (isPathOkFilesExist)
            {
                var workFolder = new DirectoryInfo(path);
                long sizeBeforeCleaning = FilesSizeCounter.SizeCounter(workFolder, out int filesCount);
                Console.WriteLine($"Размер папки до очистки: {sizeBeforeCleaning}, файлов: {filesCount}");
                long bytesRemoved = FileRemover.UpdatedFilesRemover(workFolder, out int removedFilesCount);
                Console.WriteLine($"Во время очистки было удалено файлов: {removedFilesCount}, общий размер удаленных файлов: {bytesRemoved}");
                long sizeAfterCleaning = FilesSizeCounter.SizeCounter(workFolder, out filesCount);
                Console.WriteLine($"Размер папки после очистки: {sizeAfterCleaning}, файлов: {filesCount}");
            }*/

            //нужный мне для работы код
            string path = "C:\\Users\\Eddy\\Downloads\\wrk\\PWS\\ПРОВЕРЯЕМ";
            string readyPath = "C:\\Users\\Eddy\\Downloads\\wrk\\PWS\\проверено";
            bool isPathOkFilesExist = PathAndInsidesChecker.IsPathOkAndFilesExist(path);
            if (isPathOkFilesExist )
            {   
                var mainCourseFolder = new DirectoryInfo(path);
                try 
                {
                    string currentFlowWorkFolderPath = FileRemover.FolderPreparer(mainCourseFolder);
                    var currrentFlowWorkFolder = new DirectoryInfo(currentFlowWorkFolderPath);
                    FileRemover.ArchivesPreparer(currrentFlowWorkFolder, nameIsThisDamnMacosx: "__MACOSX");
                    FileRemover.ArchivesExtracter(currrentFlowWorkFolder);
                    FileRemover.FolderByNameRemover(currrentFlowWorkFolder, name: "__MACOSX");
                    FileRemover.FileMover(currrentFlowWorkFolder);
                    FileRemover.EmptyFolderRemover(currrentFlowWorkFolder);
                    FileRemover.FilesByExtensionMover(currrentFlowWorkFolder, extension: ".zip", subFolderName: "Оригинальные архивы");
                    Console.WriteLine("UNPACKING AND CLEANING DONE! ");
                    Console.WriteLine("PRESS ANY KEY TO MARK FOLDER AS READY: ");
                    Console.ReadKey();
                    currrentFlowWorkFolder.MoveTo($"{readyPath}\\{currrentFlowWorkFolder.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }
        }
    }
}
