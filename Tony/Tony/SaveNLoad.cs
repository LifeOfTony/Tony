using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Tony
{
    public class SaveNLoad
    {

        public SaveNLoad()
        {

        }

        public void save()
        {
            //Use the StreamWriter to save file in a txt fromate.

            using (StreamWriter writer = new StreamWriter(@"Content\Save\save.txt"))
            {

                foreach (Item i in ObjectManager.Instance.Items)
                {

                    if (i.IsCollected() == true){
                        writer.WriteLine("I"+  i.GetName());
                    }

                }
               writer.WriteLine("L"+  ObjectManager .Instance .CurrentLevel.level  );

                writer.WriteLine("M"+  ObjectManager.Instance.MentalState.ToString());

                writer.WriteLine("P" +  ObjectManager.Instance.CurrentLevel.Player.getPosition());
            }
        }
        public void read()
        {

            char prefix  ;
            string line = "";

            //Use a StreamReader to read savefile.

            using (StreamReader sr = new StreamReader (@"Content\Save\save.txt"))
            {

                while (sr.Peek() >= 0)
                {
                    prefix = ((char)sr.Read());

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

        public void resetSave()
        {
            using (StreamWriter writer = new StreamWriter(@"Content\Save\save.txt"))
            {
                writer.WriteLine("");
            }
        }

    }
}




