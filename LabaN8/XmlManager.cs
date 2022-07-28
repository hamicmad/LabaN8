using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LabaN8
{
    public class XmlManager
    {
        private const string FILE_PATH = "xmx8laba.xml";
        static public void XmlSaveData<T>(List<T> list)
        {
            var formatter = new XmlSerializer(typeof(List<AutoService>));
            using FileStream fs = new FileStream(FILE_PATH, FileMode.OpenOrCreate);
            formatter.Serialize(fs, list);
        }

        static public List<T> XmlLoadData<T>(string path)
        {
            List<T> list = null;
            if (File.Exists(path))
            {
                var formatter = new XmlSerializer(typeof(List<AutoService>));
                using var fs = new FileStream(FILE_PATH, FileMode.Open);
                if (fs.Length > 0)
                    list = (List<T>)formatter.Deserialize(fs);
                return list;
            }
            else
                return new List<T>();
        }

        public static XDocument xdoc = XDocument.Load(FILE_PATH);
        // удаление каждого 2го элемента, поиск по марке автомобиля.

        public static void ChangeXml(XDocument xdoc)
        {
            var elToDel = xdoc.Element("ArrayOfAutoService").Elements("AutoService").Where((el, index) => index % 2 != 0);

            if (elToDel != null)
            {
                elToDel.Remove();
                xdoc.Save(FILE_PATH);
            }
        }

        public static void SearchEl(XDocument xdoc, string autoNum)
        {
            var elems = xdoc.Element("ArrayOfAutoService").Elements("AutoService")
                            .Where(x => x.Element("AutoNumber").Value == autoNum);

            foreach (var item in elems)
            {
                var Mark = item.Element("Mark").Value;
                var AutoNumber = item?.Element("AutoNumber")?.Value;
                var MileAge = item?.Element("Mileage").Value;

                Console.WriteLine($"AutoNumber: {AutoNumber} Mark: {Mark} MileAge: {MileAge} ");                
            }
            Console.WriteLine("-----------------");
        }

        //вывод в обратном порядке пробега
        public static void ShowOrderEl(XDocument xdoc)
        {
            var elems = xdoc.Element("ArrayOfAutoService").Elements("AutoService").
                            OrderByDescending(el => el.Element("Mileage").Value);
            
            foreach (var item in elems)
            {
                var Mark = item.Element("Mark").Value;
                var AutoNumber = item?.Element("AutoNumber")?.Value;
                var MileAge = item?.Element("Mileage")?.Value;

                Console.WriteLine($"AutoNumber: {AutoNumber} Mark: {Mark} MileAge: {MileAge} ");
            }
            Console.WriteLine("-----------------");
        }




    }
}
