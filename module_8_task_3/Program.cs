using module_8;
using module_8_task_2;
using System.Runtime;
using System.Security.Cryptography;

namespace module_8_task_3
{
    internal class Program
    {   public static void MainMethod()
        {
            Console.WriteLine("Программа предназначена для распаковки архивов в ЦС, устранения излишней вложенности и тд\n");
            Console.WriteLine("Введите путь до папки потока: ");
            string gigaMainFolderPath = Console.ReadLine();
            while (true)
            {   
                
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
                    Misc.DisplayErrorMessages(message: "!!! Путь к папке потока остался пустой, программа не сработает. Возможно, не было нужного архива в папке 'ПРОВЕРЯЕМ' !!!");
                    break;
                }
                if (currentFlowWorkFolderPath != "")
                {
                    var currrentFlowWorkFolder = new DirectoryInfo(currentFlowWorkFolderPath);
                    bool canMoveFurther = FilesSuperviser.ArchivesPreparer(currrentFlowWorkFolder);
                    if(!canMoveFurther)
                    {
                        Misc.DisplayErrorMessages(message: "Архив факапнут, нужно разархивировать вручную :(");
                        break;
                    }

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
                        Misc.DisplayErrorMessages(message: $"{ex.Message}\nНУЖНО ПЕРЕНЕСТИ АРХИВ В ПРОВЕРЕННЫЕ ВРУЧНУЮ!!!\n");
                        
                    }
                    Console.WriteLine("*** Если архив уже перенесен в проверенные, ОБЯЗАТЕЛЬНО положите новые архив и xlsx файл в папку и нажмите любую клавишу для подготовки нового потока ***");
                    Console.ReadKey();
                }
            }
        }

        public static void CleaningAfterFlowFinished()
        {
            Console.WriteLine("Программа предназначена для очистки папок после проверки всего потока\n");
            Console.WriteLine("Введите путь до папки, КОТОРУЮ НУЖНО ОЧИСТИТЬ: ");
            string folderToCleanPath = Console.ReadLine();
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

        public static void CreatingShitToUpload()
        {
            Console.WriteLine("Программа создаёт папки в потоке, нужные для сбора KIM и Final отчётов\n");
            Console.WriteLine("Введите путь до папки потока: ");
            string path = Console.ReadLine();
            FoldersSuperviser.FoldersCreator(path);
            FilesSuperviser.BeginnigQueryCreator(path);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            //основной метод, разархивирует-удаляет мусорные папки-перемещает в проверено-тд
            //MainMethod();

            //зачищаем папки от проверенных архивов, где всё ок, после полной проверки курса
            //CleaningAfterFlowFinished();
            //Console.ReadKey();

            //метод для изначальной подготовки папок к скачиванию отчётов по KIM-Start-etc
            CreatingShitToUpload(); 
        }
    }
}
