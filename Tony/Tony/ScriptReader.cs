using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tony
{
    static class ScriptReader
    {
        private static StreamReader reader;
        public static string basic { get; private set; }
        public static string complex { get; private set; }


        public static void ReadScript(string ObjectName)
        {
            reader = new StreamReader(@"Content\Scripts\"+ ObjectManager.Instance.CurrentLevel.level + "\\" + ObjectName + ".txt");
            basic = reader.ReadLine();
            complex = reader.ReadLine();

            reader.Close();
        }
    }
}
