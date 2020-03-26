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
        public static string giver { get; private set; }
        public static string taker { get; private set; }

        public static void ReadScript(string ObjectName)
        {
            reader = new StreamReader(@"Content\Scripts\"+ObjectName + ".txt");
            basic = reader.ReadLine();
            giver = reader.ReadLine();
            taker = reader.ReadLine();
            reader.Close();
        }
    }
}
