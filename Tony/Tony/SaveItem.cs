using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Tony
{
    public class SaveItem
    {
         ObjectManager OM;
         
        public SaveItem()
        {
          

           string testItem = "testItem";
            Item x = new Item(testItem, 1);
            ObjectManager.AddItem(x);

            string testItemB = "testItemB";
            Item y = new Item(testItemB, 2);
            ObjectManager.AddItem(y);
        }

        public void Save(string filepath)
        {
            
           // FileStream stream = new FileStream(filepath, FileMode.Create);
            
            

            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
            
            StreamWriter writer = new StreamWriter(filepath);

            OM = OM.getObjectManager();
            List<Item> ItemList = OM.getItems();

            serializer.Serialize(writer, ItemList);
            writer.Close();
           

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




