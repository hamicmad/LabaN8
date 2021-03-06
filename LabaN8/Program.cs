using LabaN8;
using System.Runtime.Serialization.Formatters.Binary;


StructManager structManager = new StructManager();
structManager.LoadData();

while (true)
{
    Console.WriteLine("Выберите нужную операцию:");
    Console.WriteLine("0.Ввод массива структур \t 1.Изменение заданной структуры. \t 2.Удаление структуры из массива" +
        "\n 3.Вывод на экран массива структур.\t4.Вывести общий пробег по всем машинам одной марки" +
        "\n 5.Вывести общую сумму ремонта по каждому мастеру.\t6.Выход.\n");

    var input = Console.ReadKey(true);

    switch (input.KeyChar)
    {
        case '0':
            Console.WriteLine("Введите номер авто, марку машины, пробег, имя мастера, стоимость");
            string autoNumber = Console.ReadLine();
            //список машин
            Mark mark = (Mark)int.Parse(Console.ReadLine());
            int mileage = int.Parse(Console.ReadLine());
            string name = Console.ReadLine();
            double price = double.Parse(Console.ReadLine());
            AutoService service = new AutoService(autoNumber, mileage, name, price, mark);
            structManager.Create(service);
            Console.WriteLine("Добавлено");

            break;
        case '1':
            Console.WriteLine("Введите номер авто");
            var autoNumber1 = Console.ReadLine();
            structManager.Delete(autoNumber1);
            break;
        case '2':
            Console.WriteLine("Введите номер авто");
            var autoNumber2 = Console.ReadLine();
            structManager.Change(autoNumber2);
            break;
        case '3':
            Console.WriteLine("Вывод всех");
            var allStruct = structManager.ViewAll();
            foreach (var item in allStruct)
                Console.WriteLine(item.ToString());
            break;
        case '4':
            Console.WriteLine("Выберите марку машины");
            //список машин
            Mark mark4 = (Mark)int.Parse(Console.ReadLine());
            Console.WriteLine(structManager.GetSumMileAge(mark4));

            break;
        case '5':
            var PriceMaster = structManager.GetSumPriceMaster();
            foreach(var item in PriceMaster)
            {
                Console.WriteLine($"Имя: {item.Key}, Сумма: {item.Value}");
            }
            
            break;
        case '6':
            structManager.SaveData();
            return;
        default:
            Console.WriteLine("Неверная команда. Попробуйте еще раз.\n");
            break;
    }
}


[Serializable]
public class StructManager
{
    public const string FILE_PATH = "AutoService.txt";
    List<AutoService> autoService = new();

    public void LoadData()
    {
        if (File.Exists(FILE_PATH))
        {
            var formatter = new BinaryFormatter();
            using var fs = new FileStream(FILE_PATH, FileMode.Open);
            if (fs.Length > 0)
                autoService = (List<AutoService>)formatter.Deserialize(fs);
        }
        else
            Console.WriteLine("Пусто");
    }


    public void Create(AutoService service)
    {
        autoService.Add(service);
    }

    public void Delete(string autoNumber)
    {
        for (int i = 0; i < autoService.Count; i++)
        {
            if (autoService[i].AutoNumber == autoNumber)
                autoService.RemoveAt(i);
        }
    }

    public void Change(string autuNumber)
    {

        foreach (var service in autoService)
        {
            if (service.AutoNumber == autuNumber)
                //изменения
                Console.WriteLine();

        }
    }
    public List<AutoService> ViewAll()
    {
        return autoService;
    }
    public int GetSumMileAge(Mark mark)
    {
        return autoService.Where(x => x.Mark == mark).Select(x => x.Mileage).Sum();
    }

    public Dictionary<string,double> GetSumPriceMaster()
    {
            return autoService.GroupBy(x => x.MasterName, x => x.Price).ToDictionary(x=> x.Key.ToString(), value => value.Sum(x => x));
    }

    public void SaveData()
    {
        var formatter = new BinaryFormatter();
        using FileStream fs = new FileStream(FILE_PATH, FileMode.OpenOrCreate);
        formatter.Serialize(fs, autoService);

    }
}