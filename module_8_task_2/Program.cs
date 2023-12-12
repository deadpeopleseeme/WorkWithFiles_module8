using module_8;

namespace module_8_task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Введите путь до папки, у которой нужно узнать размер: ");
            string path = Console.ReadLine();
            bool isPathOkFilesExist = PathAndInsidesChecker.IsPathOkAndFilesExist(path);
            if (isPathOkFilesExist)
            {
                var workFolder = new DirectoryInfo(path);    
                long workFolderSize = FilesSizeCounter.SizeCounter(workFolder, out int filesCount);
                Console.WriteLine($"Общий размер папки в байтах: {workFolderSize}, всего файлов: {filesCount} ");
            }
        }
    }
}
