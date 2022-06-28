using System;

namespace LabaN8
{
    public interface Enums
    {
        enum Marks
        {
            Audi,
            Acura,
            AlfaRomeo,
            Bentley,
            BMW,
        }
    }

    [Serializable]
    public struct AutoService : Enums
    {
        public string AutoNumber { get; set; }
        public int Mileage { get; set; }
        public string MasterName { get; set; }
        public double Price { get; set; }
        public Enums.Marks Mark { get; set; }
        

        public AutoService(string autoNumber, int mileage, string masterName, double price, Enums.Marks mark)
        {
            AutoNumber = autoNumber;
            Mileage = mileage;
            MasterName = masterName;
            Price = price;
            Mark = mark;
        }

        public override string ToString()
        {
            return $"номер {AutoNumber},пробег {Mileage}, мастер {MasterName},стоимость {Price} ,марка {Mark}";
        }
    }
    
}
