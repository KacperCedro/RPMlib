using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Classes
{
    public class Operator
    {
        public string Sign { get; set; }
        public OperatorType TypeOfOperation { get; set; }
        public bool IsLeftHanded { get; set; }
        public int Priority { get; set; }
        public Operator(string sign, OperatorType typeOfOperation, bool isLeftHanded, int priority)
        {
            Sign = sign;
            TypeOfOperation = typeOfOperation;
            IsLeftHanded = isLeftHanded;
            Priority = priority;
        }
    }
}
