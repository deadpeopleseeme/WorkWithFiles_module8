using module_8;
using module_8_task_2;
using System.Runtime;
using System.Security.Cryptography;

namespace module_8_task_3
{
    internal class Program
    {   public static void MainMethod(string path)
        {
            while (true)
            {
                string gigaMainFolderPath = path;
                string mainFolderPath = $"{gigaMainFolderPath}\\ПРОВЕРЯЕМ";
                string readyFolderPath = $"{gigaMainFolderPath}\\проверено";
                if (!Directory.Exists(readyFolderPath))
                {
                    Directory.CreateDirectory(readyFolderPath);
                }
                var mainFolder = new DirectoryInfo(mainFolderPath);


                Console.WriteLine("Начинаем подготовку папки потока: ");
                string currentFlowWorkFolderPath = "";
                try
                {
                    currentFlowWorkFolderPath = FoldersSuperviser.FolderPreparer(mainFolder);
                }
                catch (Exception)
                {
                    Console.WriteLine("\n!!! Путь к папке потока остался пустой, программа не сработает. Возможно, не было нужного архива в папке 'ПРОВЕРЯЕМ' !!!");
                }
                if (currentFlowWorkFolderPath != "")
                {
                    var currrentFlowWorkFolder = new DirectoryInfo(currentFlowWorkFolderPath);
                    FilesSuperviser.ArchivesPreparer(currrentFlowWorkFolder);

                    Console.WriteLine("Извлекаем содержимое архива, переносим его в отдельную папку: ");
                    FilesSuperviser.ArchivesExtracter(currrentFlowWorkFolder);

                    Console.WriteLine("Ищем и удаляем ненужные папки-дубликаты: ");
                    FoldersSuperviser.FolderByNameRemover(currrentFlowWorkFolder, needToCheckNested: true);

                    Console.WriteLine("Формируем адекватную структуру папок студента без ненужных подпапок: ");
                    FilesSuperviser.FixingUnnecessaryNest(currrentFlowWorkFolder);

                    Console.WriteLine("Ищем и удаляем пустые папки: ");
                    FoldersSuperviser.EmptyFolderRemover(currrentFlowWorkFolder);

                    Console.WriteLine("Удаляем распакованные архивы: ");
                    FilesSuperviser.FilesByExtensionRemover(currrentFlowWorkFolder, extension: ".zip", subFolderName: "Оригинальные архивы");

                    Console.WriteLine("Папка готова к работе!\n\n---нажмите любую клавишу, чтоб переместить папку потока в проверенные---");
                    Console.WriteLine("!!! НЕ ЗАБУДЬТЕ ВЫЙТИ ИЗ ПАПКИ ПОТОКА И ЗАКРЫТЬ ЭКСЕЛЬ ФАЙЛ, ИНАЧЕ ПЕРЕМЕСТИТЬ НЕ ПОЛУЧИТСЯ !!!");

                    Console.ReadKey();
                    try
                    {
                        currrentFlowWorkFolder.MoveTo($"{readyFolderPath}\\{currrentFlowWorkFolder.Name}");
                        Console.WriteLine($"\nПапка {currrentFlowWorkFolder.Name} успешно перемещена в проверенные.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("*** Теперь ОБЯЗАТЕЛЬНО положите архив и xlsx файл в папку и нажмите любую клавишу для подготовки нового потока ***");
                    Console.ReadKey();
                }
            }
        }

        public static void CleaningAfterFlowFinished(string folderToCleanPath)
        {
            var folderToClean = new DirectoryInfo(folderToCleanPath);
            foreach(var flowDirectory in folderToClean.GetDirectories()) 
            {
                foreach(var studentDirectory in flowDirectory.GetDirectories())
                {
                    if (studentDirectory.Name == "Оригинальный архив")
                    {
                        foreach(FileInfo file in studentDirectory.GetFiles())
                        {
                            string newName = $"{flowDirectory.FullName}\\{file.Name}";
                            file.MoveTo(newName);
                            Console.WriteLine($"\nПереместили файл {file.Name}");
                        }
                    }
                    Console.WriteLine($"Удаляем папку {studentDirectory}");
                    studentDirectory.Delete(true);
                }
            }
        }

        public static void CreatingShitToUpload(string path)
        {
            FoldersSuperviser.FoldersCreator(path);
            FilesSuperviser.BeginnigQueryCreator(path);
        }
        static void Main(string[] args)
        {
            //основной метод, разархивирует-удаляет мусорные папки-перемещает в проверено-тд
            //MainMethod(@"E:\wrk\SDA\");

            //зачищаем папки от проверенных архивов, где всё ок, после полной проверки курса
            CleaningAfterFlowFinished(@"E:\wrk\SDA\проверено\всёок");

            //метод для изначальной подготовки папок к скачиванию отчётов по KIM-Start-etc
            //CreatingShitToUpload(@"E:\wrk\ГРУЗИМ ГОВНО ЦС\SDA");
        }
    }
}
