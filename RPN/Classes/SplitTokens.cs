using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Classes
{
    internal class SplitTokens
    {
        public static List<string> SplitToList(string tokenString, Dictionary<char,Operator> dictionaryOfOperators)
        {
            string temoraryNumber = "";
            List<string> tokens = new List<string>();
            foreach (var token in tokenString)
            {
                if (dictionaryOfOperators.TryGetValue(token, out var tmp))
                {
                    tokens.Add(token.ToString());
                }
                else if (int.TryParse(token.ToString(), out int result))
                {
                    temoraryNumber += result;
                }
                else if( token == '.')
                {
                    temoraryNumber += result;
                }
            }
            return tokens;
        }
    }
}
