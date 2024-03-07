using RPN.Classes;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

namespace RPN
{
    /*
     *        12+2×(3×4+10/5)
     *        
     *        12 2 3 4 × 10 5 / + × +
     *        
     *        40
     */
    public class RPN
    {
        public static List<string> SplitToListUsingSpaceBar(string tokenString)
        {
            List<string> tokens = new List<string>();

            tokens = tokenString.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            return tokens;
        }
        public static List<string> SplitToListUsingRegex(string tokenString)
        {
            List<string> tokens = new List<string>();
            string pattern = @"([+\-*/()^])|\b\d+(\,\d+)?\b";
            var matches = Regex.Matches(tokenString, pattern);
            tokens = matches.Cast<Match>().Select(match => match.Value).ToList();
            return tokens;
        }
        public static List<string> InfixToPostfix(List<string> listOfTokens)
        {
            List<string> postfix = new List<string>();
            Stack<string> stack = new Stack<string>();
            Queue<string> queueOut = new Queue<string>();

            foreach (var token in listOfTokens)
            {
                if (double.TryParse(token, out double tmpNumber))
                {
                    queueOut.Enqueue(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    string tmpOperator = string.Empty;
                    for (int item = 0; item < stack.Count; item++)
                    {
                        tmpOperator = stack.Pop();
                        if (tmpOperator == "(")
                        {
                            break;
                        }
                        else
                        {
                            queueOut.Enqueue($"{tmpOperator}");
                        }
                    }
                }
                else if (DictionaryOfOperators.dictionary.ContainsKey(token))
                {
                    while (true)
                    {
                        string tmpOperator = "0";
                        if (stack.TryPop(out tmpOperator))
                        {
                            if ((DictionaryOfOperators.dictionary[token].IsLeftHanded && DictionaryOfOperators.dictionary[token].Priority <= DictionaryOfOperators.dictionary[tmpOperator].Priority) || (DictionaryOfOperators.dictionary[token].IsLeftHanded == false && DictionaryOfOperators.dictionary[token].Priority < DictionaryOfOperators.dictionary[tmpOperator].Priority))
                            {
                                queueOut.Enqueue(tmpOperator);
                            }
                            else
                            {
                                stack.Push(tmpOperator);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    stack.Push(token);
                }
            }
            int stackCount = stack.Count;
            for (int i = 0; i < stackCount; i++)
            {
                queueOut.Enqueue(stack.Pop());
            }
            int queueOutCount = queueOut.Count;
            for (int i = 0; i < queueOutCount; i++)
            {
                postfix.Add(queueOut.Dequeue());
            }

            return postfix;
        }
        public static double PostfixToResult(List<string> postfix)
        {
            try
            {
                double result = 0;
                Stack<double> stack = new Stack<double>();

                foreach (var token in postfix)
                {
                    if (double.TryParse(token, out double tmpNumber))
                    {
                        stack.Push(tmpNumber);
                    }
                    else if (DictionaryOfOperators.dictionary.ContainsKey(token))
                    {
                        //double a = stack.Pop();
                        //double b = stack.Count > 1 ? stack.Pop() : 0;
                        double a = stack.Pop();
                        double b = stack.Pop();
                        //  sometimes stack throws exeptiom becouse theres nothing on it
                        //  for example:
                        //  4-(-3)
                        //  it would throw exeption becouse there is nothing behind "-3"
                        //  but if the calculation is"
                        //  4-(0-3)
                        //  it works even if it doesnt meke any sense
                        //double a = stack.TryPop(out double tmpA) ? tmpA : 0;
                        //double b = stack.TryPop(out double tmpB) ? tmpB : 0;

                        switch (token)
                        {
                            case "+":
                                stack.Push(b + a);
                                break;
                            case "-":
                                stack.Push(b - a);
                                break;
                            case "*":
                                stack.Push(b * a);
                                break;
                            case "/":
                                stack.Push(b / a);
                                break;
                            case "^":
                                stack.Push(Math.Pow(b, a));
                                break;
                            default:
                                break;
                        }
                    }
                }
                result = stack.Pop();
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Error, check your calculation");
            }
        }
    }
}