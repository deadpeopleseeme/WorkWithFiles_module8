using module_8_task_2;

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
    }
}
