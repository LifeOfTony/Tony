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
        public static int currentLevel;

        public static void ReadScript(string ObjectName)
        {
            reader = new StreamReader(@"Content\Scripts\"+ currentLevel + "\\" + ObjectName + ".txt");
            basic = reader.ReadLine();
            complex = reader.ReadLine();

            reader.Close();
        }
    }
}
