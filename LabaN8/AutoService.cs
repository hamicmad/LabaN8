using System;

namespace LabaN8
{

    [Serializable]
    public struct AutoService
    {
        public string AutoNumber { get; set; }
        public int Mileage { get; set; }
        public string MasterName { get; set; }
        public double Price { get; set; }
        public Mark Mark { get; set; }


        public AutoService(string autoNumber, int mileage, string masterName, double price, Mark mark)
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
