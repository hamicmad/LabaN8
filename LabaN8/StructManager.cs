using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace LabaN8
{
    [Serializable]
    public class StructManager
    {
        private const string FILE_PATH = "AutoService.txt";
        public List<AutoService> autoService = new();
        public void SaveData()
        {
            var formatter = new BinaryFormatter();
            using FileStream fs = new FileStream(FILE_PATH, FileMode.OpenOrCreate);
            formatter.Serialize(fs, autoService);

        }
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

        public Dictionary<string, double> GetSumPriceMaster()
        {
            return autoService.GroupBy(x => x.MasterName, x => x.Price).ToDictionary(x => x.Key.ToString(), value => value.Sum(x => x));
        }
    }
}
