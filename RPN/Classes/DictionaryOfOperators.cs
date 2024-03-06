using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Classes
{
    public class DictionaryOfOperators
    {
        public static Dictionary<char, Operator> dictionary = new Dictionary<char, Operator>()
        {
            { '(', new Operator('(',OperatorType.OpeningBracket, true, 0) },
            { ')', new Operator(')',OperatorType.ClosingBracket, true, 1) },
            {'+', new Operator('+',OperatorType.Addition,true,1) },
            {'-', new Operator('-',OperatorType.Subtraction,true,1) },
            {'*', new Operator('*',OperatorType.Multiplication,true,2) },
            {'/', new Operator('/',OperatorType.Division,true,2) },
            {'%', new Operator('%',OperatorType.Modulo,true,2) },
            {'^', new Operator('^',OperatorType.Exponentiation,false,3) },
        };
    }
}
