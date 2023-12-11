namespace module_8_task_1

{ 
    internal class Program
    {
        // метод, проверяющий наличие файлов и папок в директории
        public static bool IsEmptyChecker(DirectoryInfo directoryInfo)
        {
            if (directoryInfo.GetDirectories().Length == 0 && directoryInfo.GetFiles().Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void RecursiveRemover(DirectoryInfo diInfo, bool isRootFolder = true)
        {
            bool isEmpty = false;

            // проверяем, есть ли папка по указанному пути, если нет, дальше ничего не сработает
            if (!diInfo.Exists) { Console.WriteLine("НЕТ ТАКОЙ ПАПКИ"); }

            // если папка есть, проверяем, пуста ли она
            else
            {   
                isEmpty = IsEmptyChecker(diInfo);
            }
            // если пуста, сообщаем, программа отработала, при этом булевая переменная нужна для того, чтоб не выдавать такое же сообщение при удалении из вложенных папок
            if (diInfo.Exists && isEmpty && isRootFolder)
            {
                Console.WriteLine("Папка пуста, нечего проверять ");
            }
            // если не пуста, идём к проверке
            else if (diInfo.Exists && !isEmpty)
            {   
                //проверяем каждый файл в искомой папке
                foreach (FileInfo fileInfo in diInfo.GetFiles())
                {
                    try
                    {
                        var interval = DateTime.Now - fileInfo.LastAccessTime;
                        if (interval.TotalMinutes > 30)
                        {
                            Console.WriteLine($"Файл {fileInfo} не использовался {interval.TotalMinutes} минут и был удалён");
                            fileInfo.Delete();
                        }       
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine($"{exc.Message}\n");
                    }
                }

                //далее рекурсивно проверяем файлы во всех вложенных папках
                foreach (DirectoryInfo dir in diInfo.GetDirectories())
                {
                    RecursiveRemover(dir, isRootFolder = false);
                    try
                    {
                        var interval = DateTime.Now - dir.LastAccessTime;
                        if (interval.TotalMinutes > 30)
                        {
                            Console.WriteLine($"Папка {dir} не использовалась {interval} и была удалена");
                            dir.Delete(true);
                        }
                    }
                    catch (Exception exc) 
                    { 
                        Console.WriteLine(exc.Message); 
                    }
                }
            }
        }
            static void Main(string[] args)
        {
            var workFolder = new DirectoryInfo("E:\\Волянск мейнпапк\\testDirectory");
            RecursiveRemover(workFolder);
        }
    }
}
