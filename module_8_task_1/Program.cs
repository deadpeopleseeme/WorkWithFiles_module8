namespace module_8_task_1

{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки, которую нужно очистить: ");
            string path = Console.ReadLine();
            try 
            {
                var workFolder = new DirectoryInfo(path);
                FileRemover.RecursiveRemover(workFolder);
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }   
        }
    }
}
