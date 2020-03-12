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

        public SaveItem()
        {

            /*
            string testItem = "testItem";
            Item x = new Item(testItem, 1);
            ObjectManager.Instance.AddItem(x);

            string testItemB = "testItemB";
            Item y = new Item(testItemB, 2);
            ObjectManager.Instance.AddItem(y);
            */
        }

        public void save()
        {

            using (StreamWriter writer = new StreamWriter(@"D:\VS CM\LifeOfTony\save.txt"))
            {

               // writer.WriteLine(ObjectManager.Instance.Items);
                foreach (Item i in ObjectManager.Instance.Items)
                {

                    if (i.IsCollected() == true){
                        writer.WriteLine("I"+  i.GetName());
                    }

                }
               writer.WriteLine("L"+  ObjectManager .Instance .CurrentLevel.getLevel  );

                writer.WriteLine("M"+  ObjectManager.Instance.MentalState.ToString());

                writer.WriteLine("P" +  ObjectManager.Instance.CurrentLevel.Player.getPosition());
            }
        }
        public void read()
        {
            char prefix  ;
            string line = "";
            using (StreamReader sr = new StreamReader (@"D:\VS CM\LifeOfTony\save.txt"))
            {

                while (sr.Peek() >= 0)
                {
                    prefix = ((char)sr.Read());
                   // Console.Write(prefix);
                    if (prefix.Equals('I'))
                    {
                        line = sr.ReadLine();

                        Console.WriteLine(line);
                    }
                    else if (prefix.Equals('L'))
                    {
                        line = sr.ReadLine();
                      
                        Console.WriteLine(line);
                    }
                    else if (prefix.Equals('M'))
                    {
                        line = sr.ReadLine();

                        Console.WriteLine(line);
                    }
                    else if (prefix.Equals('P'))
                    {
                        line = sr.ReadLine();

                        Console.WriteLine(line);
                    }


                }


                Console.WriteLine ("end of the file");
            }
        }

    }
}




