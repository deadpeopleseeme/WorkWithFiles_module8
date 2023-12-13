using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;


namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }

    }


    internal class Program
    {
        public static List<Student> ReadFromFile(BinaryFormatter bf)
        {
            List<Student> studentsFromFile = new();
            /*десериализуем студентов из файла .dat, т.к. заранее мы якобы не знаем, сколько их, и файл из урока не работает нормально,
            используем while true и try-catch, т.к. 100% будет исключение после последнего десериализованного студента
            очень костыльная реализация, но для примера подойдет
            */
            using (var fs1 = new FileStream("students.dat", FileMode.Open))
            {
                while (true)
                {
                    try
                    {
                        var student = (Student)bf.Deserialize(fs1);
                        studentsFromFile.Add(student);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            return studentsFromFile;
        }
        static void Main(string[] args)
        {
            var student1 = new Student("Иван", "ПО-15", new DateTime(2000, 3, 1));
            var student2 = new Student("Пётр", "ПО-20", new DateTime(1990, 10, 31));
            var student3 = new Student("Сергей", "ПО-20", new DateTime(1997, 8, 20));
            var student4 = new Student("Светлана", "ПО-30", new DateTime(1993, 8, 21));
            var student5 = new Student("Евгения", "ПО-15", new DateTime(1992, 2, 5));

            List<Student> studentsUnsorted = new() { student1, student2, student3, student4, student5 };
            BinaryFormatter binaryFormatter = new();

            //записываем студентов в файл .dat, предполагая, что они были сериализованы по одному перебором списка
            using (var fs = new FileStream("students.dat", FileMode.OpenOrCreate))
            {
                foreach (var student in studentsUnsorted)
                {
                    binaryFormatter.Serialize(fs, student);
                }
            }

            //получаем список студентов из "нашего" файла и формируем список групп
            var studentsFromFile = ReadFromFile(binaryFormatter);
            var groupsList = new List<string>();
            foreach (var student in studentsFromFile)
            {
                if (!groupsList.Contains(student.Group))
                {
                    groupsList.Add(student.Group);
                }
            }

            //создаём на рабочем столе папку
            var pathToStudentsFolder = "C:\\Users\\Eddy\\Desktop\\Students";
            var dirInfo = new DirectoryInfo(pathToStudentsFolder);
            if (!dirInfo.Exists)
            {
                try
                {
                    dirInfo.Create();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            
            foreach (var group in groupsList)
            {   //создаём файлы групп
                var newPath = $"{pathToStudentsFolder}\\{group}.txt";

                //перебираем в каждой группе студентов из прочитанного файла, если группа совпадает = записываем
                foreach (var student in studentsFromFile)
                {
                    if (student.Group == group)
                    {
                        var text = $"{student.Name}, дата рождения: {student.DateOfBirth.ToString("dd/MM/yyyy")}\n";
                        try
                        {
                            using StreamWriter sw = new StreamWriter(newPath, append: true);
                            sw.WriteLine(text);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                    }
                }
            }
        }
    }
}