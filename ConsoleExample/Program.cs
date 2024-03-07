using System.Runtime.CompilerServices;

namespace ConsoleExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("set 1 if u want to calculate, otherwise set 0 to close app:");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Calculate();
                }
                else if (option == "0") 
                {
                    break;
                }
            }                
        }
        static void Calculate()
        {
            string calculation = string.Empty;
            Console.WriteLine("Add calculation (example: 3*(3-5,4)/(3+0,2)/3^3)-(-3):");
            calculation = Console.ReadLine();
            List<string> tokenList = RPN.RPN.SplitToListUsingSpaceBar(calculation);
            foreach (var token in tokenList)
            {
                Console.WriteLine(token);
            }
            Console.WriteLine();
            List<string> postfix = RPN.RPN.InfixToPostfix(tokenList);
            foreach (var token in postfix)
            {
                Console.WriteLine(token);
            }
            Console.WriteLine();
            double result = RPN.RPN.PostfixToResult(postfix);
            Console.WriteLine(result);

        }
    }
}
