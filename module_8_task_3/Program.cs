using module_8;
using module_8_task_2;
using System.Runtime;

namespace module_8_task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки, которую нужно очистить: ");
            string path = Console.ReadLine();
            bool isPathOkFilesExist = PathAndInsidesChecker.IsPathOkAndFilesExist(path);
            if (isPathOkFilesExist)
            {
                var workFolder = new DirectoryInfo(path);
                long sizeBeforeCleaning = FilesSizeCounter.SizeCounter(workFolder, out int filesCount);
                Console.WriteLine($"Размер папки до очистки: {sizeBeforeCleaning}, файлов: {filesCount}");
                long bytesRemoved = FileRemover.UpdatedFilesRemover(workFolder, out int removedFilesCount);
                Console.WriteLine($"Во время очистки было удалено файлов: {removedFilesCount}, общий размер удаленных файлов: {bytesRemoved}");
                long sizeAfterCleaning = FilesSizeCounter.SizeCounter(workFolder, out filesCount);
                Console.WriteLine($"Размер папки после очистки: {sizeAfterCleaning}, файлов: {filesCount}");
            }
        }
    }
}
