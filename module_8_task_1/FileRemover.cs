using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_8_task_1
{
    public static class FileRemover
    {
       public static void RecursiveRemover(DirectoryInfo diInfo)
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
                    RecursiveRemover(dir);
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
}
