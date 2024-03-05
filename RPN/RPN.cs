using RPN.Classes;
using System.Collections.ObjectModel;

namespace RPN
{
    public class RPN
    {
        public static string InfixToPostfix(string calculation)
        {
            List<string> listOfTokens = SplitTokens.SplitToList(calculation, DictionaryOfOperators.dictionary);
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
        public static string PostfixToResult(string postfix)
        {
            string result = string.Empty;
            Stack<double> stack = new Stack<double>();

            List<string> list = postfix.Split(' ').ToList();
            foreach (var token in list)
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
                                stack.Push(Math.Pow(a , b));
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