using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Microsoft.Xna.Framework;

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
                        writer.WriteLine(i.GetModifier());
                    }

                }
               writer.WriteLine("L"+  ObjectManager .Instance .CurrentLevel.level  );

                writer.WriteLine("M"+  ObjectManager.Instance.MentalState.ToString());

                writer.WriteLine("P" +  ObjectManager.Instance.CurrentLevel.Player.getPosition().X);
                writer.WriteLine(ObjectManager.Instance.CurrentLevel.Player.getPosition().Y);
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
                        string name = line;
                        line = sr.ReadLine();
                        int modifier = Convert.ToInt32 (line);
                        Item i = new Item(name, modifier);
                        ObjectManager.Instance.setCorrectedItem(i);

                        //Console.WriteLine(line);
                    }
                    else if (prefix.Equals('L'))
                    {
                        line = sr.ReadLine();
                        int levelNumber = Convert.ToInt32(line);
                        ObjectManager .Instance .setCurrentLevel (ObjectManager.Instance.getLevelFromList (levelNumber));
                        
                        //Console.WriteLine(line);
                    }
                    else if (prefix.Equals('M'))
                    {
                        line = sr.ReadLine();
                        int mentalState = Convert.ToInt32(line);
                        ObjectManager.Instance.setMentalState(mentalState);

                        //Console.WriteLine(line);
                    }
                    else if (prefix.Equals('P'))
                    {
                        line = sr.ReadLine();
                        int x = Convert.ToInt32(line);
                        line = sr.ReadLine();
                        int y = Convert.ToInt32(line);
                        Vector2 Position = new Vector2(x,y);
                        ObjectManager.Instance.setPosition(Position);

                       // Console.WriteLine(line);
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




