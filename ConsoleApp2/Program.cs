using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task2
{
    class Program
    {
        [Serializable]
        class Contact
        {
            public string Name { get; set; }
            public long PhoneNumber { get; set; }
            public string Email { get; set; }

            public Contact(string name, long phoneNumber, string email)
            {
                Name = name;
                PhoneNumber = phoneNumber;
                Email = email;
            }
        }
        public static void Main()
        {
            var contact = new Contact("Валян", 7921, "@gmail.com");
            Console.WriteLine($"Объект {contact.Name} успешно создан ");

            BinaryFormatter bf = new();

            using var fs = new FileStream("persons.dat", FileMode.OpenOrCreate);
#pragma warning disable SYSLIB0011
            bf.Serialize(fs, contact);
            Console.WriteLine($"Объект {contact.Name} сериализирован");
            fs.Close();

            using var fs1 = new FileStream("persons.dat", FileMode.Open); 
            var newContact = (Contact)bf.Deserialize(fs1);
            Console.WriteLine("Объект десериализован");
            Console.WriteLine($"Имя: {newContact.Name} --- Номер телефона: {newContact.PhoneNumber} --- Почта: {newContact.Email}");
#pragma warning restore SYSLIB0011
           
        }
    }

}