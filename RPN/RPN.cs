using RPN.Classes;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

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
        public static List<string> SplitToList(string tokenString, Dictionary<char, Operator> dictionaryOfOperators)
        {
            List<string> tokens = new List<string>();
            string pattern = @"([+\-*/()^])|\b\d+(\.\d+)?\b";
            var matches = Regex.Matches(tokenString, pattern);
            tokens = matches.Cast<Match>().Select(match => match.Value).ToList();
            return tokens;
        }
        public static string InfixToPostfix(List<string> listOfTokens)
        {
            string postfix = string.Empty;
            Stack<string> stack = new Stack<string>();
            Queue<string> queueOut = new Queue<string>();

            foreach (var token in listOfTokens)
            {
                if (double.TryParse(token, out var temporatuNumber))
                {
                    queueOut.Enqueue(token);
                }
                else if (token == '('.ToString())
                {
                    stack.Push(token);
                }
                else if (token == ')'.ToString())
                {
                    string tmpOperator = string.Empty;
                    for (int item = 0; item < stack.Count; item++)
                    {
                        tmpOperator = stack.Pop();
                        if (tmpOperator == '('.ToString())
                        {
                            break;
                        }
                        else
                        {
                            queueOut.Enqueue($"{tmpOperator}");
                        }
                    }
                }
                else if (char.TryParse(token, out char tmpToken))
                {
                    string tmpOperator = string.Empty;
                    if (DictionaryOfOperators.dictionary.ContainsKey(tmpToken))
                    {
                        while (true)
                        {
                            if (stack.TryPop(out tmpOperator))
                            {
                                if (char.TryParse(tmpOperator, out char tmpCharOperator) && ((DictionaryOfOperators.dictionary[tmpToken].IsLeftHanded && DictionaryOfOperators.dictionary[tmpToken].Priority <= DictionaryOfOperators.dictionary[tmpCharOperator].Priority) || (DictionaryOfOperators.dictionary[tmpToken].IsLeftHanded == false && DictionaryOfOperators.dictionary[tmpToken].Priority < DictionaryOfOperators.dictionary[tmpCharOperator].Priority)))
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
                        stack.Push(tmpToken.ToString());
                    }
                }
            }
            foreach (var token in stack)
            {
                queueOut.Enqueue($"{token}");
            }
            for (int i = 0; i < queueOut.Count; i++)
            {
                postfix += queueOut.Dequeue() + " ";
            }
            postfix.Remove(postfix.Length - 1);

            return postfix;
        }
        public static string PostfixToResult(List<string> postfix)
        {
            string result = string.Empty;
            Stack<double> stack = new Stack<double>();

            foreach (var token in postfix)
            {
                if (double.TryParse(token, out double tmpNumber))
                {
                    stack.Push(tmpNumber);
                }
                else if (char.TryParse(token, out char tmpToken))
                {
                    if (DictionaryOfOperators.dictionary.ContainsKey(tmpToken))
                    {
                        double a = stack.Pop();
                        double b = stack.Pop();
                        switch (tmpToken)
                        {
                            case '+':
                                stack.Push(a + b);
                                break;
                            case '-':
                                stack.Push(a - b);
                                break;
                            case '*':
                                stack.Push(a * b);
                                break;
                            case '/':
                                stack.Push(a / b);
                                break;
                            case '^':
                                stack.Push(Math.Pow(a, b));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            result = stack.Pop().ToString();
            return result;

        }
    }
}