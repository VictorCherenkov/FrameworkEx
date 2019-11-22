using System.Collections.Generic;
using System.Linq;

namespace FrameworkEx
{
    public static class ArgsParser
    {
        /// <summary>
        /// Each arg format: name=value. Value can be in “” to contain spaces.
        /// </summary>
        public static IDictionary<string, string> Parse(string[] args, bool keyToLowerCase = false)
        {
            return args.Select(x => x.Split('=')).ToDictionary
            (
                x => keyToLowerCase ? x.First().Trim(' ', '"').ToLower() : x.First().Trim(' ', '"'),
                x => x.Skip(1).First().Trim(' ', '"')
            );
        }
    }
}