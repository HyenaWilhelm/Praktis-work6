using Newtonsoft.Json;
using System.Xml.Serialization;
using Текстовый_конвертер_пр6;

namespace Текстовый_конвертер_пр6
{
    class Program
    {
        public static List<Class1>? Classes { get; private set; }

        static void Main()
        {
            List<Class1> convert1 = new List<Class1>();
            Console.WriteLine("Введите путь до файла, который вы хотите открыть");
            Console.WriteLine("-------------------------------------------------");
            string reader1 = Console.ReadLine();
            string format1 = Path.GetExtension(reader1);
            switch (format1)
            {
                case ".txt":
                    using (StreamReader stream = new StreamReader(reader1))
                    {
                        while (!stream.EndOfStream)
                        {
                            string name = stream.ReadLine();
                            Console.WriteLine(name);
                            int age = int.Parse(stream.ReadLine());
                            Console.WriteLine(age);
                            convert1.Add(new Class1(name, age));
                        }
                    }
                    break;
                case ".json":
                    string json = File.ReadAllText(reader1);
                    convert1 = JsonConvert.DeserializeObject<List<Class1>>(json);
                    break;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Class1>));
                    using (FileStream input = new FileStream(reader1, FileMode.Open))
                    {
                        Classes = (List<Class1>)serializer.Deserialize(input);
                    }
                    break;
            }
            Console.WriteLine("Нажмите F1 для сохранения, Escape для выхода");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.F1)
                {
                    Console.WriteLine("Введите путь к файлу и формат в котором хотите сохранить(txt,json,xml)");
                    Console.WriteLine("--------------------------------------------------------------------");
                    string reader2 = Console.ReadLine();
                    string format2 = Path.GetExtension(reader2);
                    switch (format2)
                    {
                        case ".txt":
                            using (StreamWriter writer = new StreamWriter(reader2))
                            {
                                foreach (var convert2 in convert1)
                                {
                                    writer.WriteLine(convert2.name);
                                    writer.WriteLine(convert2.age);
                                }
                            }
                            Console.WriteLine("Сохранено в txt");
                            return;
                        case ".json":
                            string result = JsonConvert.SerializeObject(convert1);
                            File.WriteAllText(reader2, result);
                            Console.WriteLine("Сохранено в json");
                            return;
                        case ".xml":
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Class1>));
                            using (FileStream output = new FileStream(reader2, FileMode.Create))
                            {
                                serializer.Serialize(output, convert1);
                            }
                            Console.WriteLine("Сохранено в xml");
                            return;
                    }
                }
            } while (key != ConsoleKey.Escape);
            Console.WriteLine("Успешно сохранено! Спасибо что воспользовались текстовым редактором!!!");
        }
    }
}