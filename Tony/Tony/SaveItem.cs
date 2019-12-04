using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SLSystem
{
    public class SaveItem
    {
        private ObjectManager OM;
        public SaveItem()
        {
            OM = OM.getObjectManager();
        }

        public void Save(string filepath)
        {

            FileStream stream = new FileStream(filepath, FileMode.Create);


            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
            List<Item> ListOfItem = OM.getItems();
            StreamWriter writer = new StreamWriter(stream);

            serializer.Serialize(writer, ListOfItem);

        }



        // BinaryFormatter bf = new BinaryFormatter();
        //  for (int i = 1; i < L.Count(); i++)
        //{
        //  bf.Serialize(fs, L.Take(i));
        //}

        // using (Stream fs = new FileStream(@"D:\VS CM\SLS\ItemSave.xml", FileMode.Create, FileAccess.Write, FileShare.None))

        // L = null;

        //XmlSerializer serializer2 = new XmlSerializer(typeof(List<Item>));

        // using (FileStream fs2 = File.OpenRead(@"D:\VS CM\SLS\ItemSave.xml"))
        // {
        //L = (List<Item>)serializer2.Deserialize(fs2);

        //@"D:\VS CM\SLS\ItemSave.xml"
    }

}




