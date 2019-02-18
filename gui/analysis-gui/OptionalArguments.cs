using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace analysis_gui
{
    static class OptionalArguments
    {
        public struct Argument
        {
            public string name;
            public string argument;
            public Type type;
        }

        public static List<Argument> getArgumentsForScript(string script)
        {
            var returnList = new List<Argument>();

            returnList.Add(new Argument { name = "stopwords", argument = "--stopwords", type = typeof(bool) });
            returnList.Add(new Argument { name = "lowest-frequency", argument = "--lowest-frequency", type = typeof(int) });
            returnList.Add(new Argument { name = "thread-count", argument = "--thread-count", type = typeof(int) });

            //n-grams has an n
            if (script == "ngrams.py")
                returnList.Add(new Argument { name = "n", argument = "--n", type = typeof(int) });

            return returnList;
        }
    }
}
