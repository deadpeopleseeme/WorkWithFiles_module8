namespace module_8
{
    public static class PathAndInsidesChecker
    {
        public static bool IsPathOkAndFilesExist(string path)
        {
            if (path == "")
            {
                Console.WriteLine("Не введён адрес папки, закрываем программу");
                return false;
            }
            var workFolder = new DirectoryInfo(path);
            if (!workFolder.Exists)
            {
                Console.WriteLine("НЕТ ТАКОЙ ПАПКИ");
                return false;
            }
            else
            {
                if (workFolder.GetDirectories().Length != 0 || workFolder.GetFiles().Length != 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Папка пуста! ");
                    return false;
                }
            }
        }
    }
}

