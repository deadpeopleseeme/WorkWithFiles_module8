using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace module_8_task_2
{
    internal class FilesSizeCounter
    {
        public static long SizeCounter(DirectoryInfo diInfo)
        {
            long size = 0;
            foreach (DirectoryInfo dir in diInfo.GetDirectories())
            {
                size = SizeCounter(dir);
            }
            foreach (FileInfo fiInfo in diInfo.GetFiles())
            {
                //на случай, если у какого-то из файлов не будет считываться размер?
                try
                {
                    size += fiInfo.Length;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }  
            }
            return size;
        }
    }
}
