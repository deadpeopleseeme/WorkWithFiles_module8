using module_8;

namespace module_8_task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки, которую нужно очистить: ");
            string path = Console.ReadLine();
            bool isPathOkFilesExist = PathAndInsidesChecker.IsPathOkAndFilesExist(path);
            if(isPathOkFilesExist)
            {   var workFolder = new DirectoryInfo(path);
                FileRemover.RecursiveRemover(workFolder);
            }

        }
    }
}
