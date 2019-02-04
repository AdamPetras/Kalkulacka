using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace kalkulacka.src
{
    class MainLogic
    {
        private TextBlock _textBlock;
        private ListBox _history;
        private Stack<char> _stackOperator;

        public MainLogic(TextBlock textBlock, ListBox historyList)
        {
            _stackOperator = new Stack<char>();
            _textBlock = textBlock;
            _history = historyList;
        }

        public static bool IsOperator(char c)
        {
            if (c == '+')
                return true;
            if (c == '-')
                return true;
            if (c == '*')
                return true;
            if (c == '/')
                return true;
            return false;
        }

        private List<string> InfixToPrefix(string infix)
        {
            string str = "";
            bool ifTheFistIsSub = false;
            if (infix.Length > 0)
            {
                if (infix[0] == '-')
                ifTheFistIsSub = true;
            }
            List<string> output = new List<string>();
            for (int i = 0; i < infix.Length; i++)
            {
                if (!IsOperator(infix[i]) || ifTheFistIsSub)
                {
                    ifTheFistIsSub = false;
                    str += infix[i];
                    continue;
                }
                else
                {
                    output.Add(str);
                    str = "";
                    while (_stackOperator.Count > 0 && Priority(_stackOperator.Peek())   //lazy evaluation
                        >= Priority(infix[i]))
                    {
                        output.Add(_stackOperator.Pop().ToString());
                    }
                    _stackOperator.Push(infix[i]);
                }
            }
            output.Add(str);
            while (_stackOperator.Count > 0)
            {
                output.Add(_stackOperator.Pop().ToString());
            }
            return output;
        }

        private double evaluatePrefix(List<string> prefix)
        {
            Stack<double> result = new Stack<double>();
            for (int j = 0; j < prefix.Count; j++)
            {
                if (prefix[j] != "")
                    if (!IsOperator(prefix[j][0]) || prefix[j].Length > 1)  //druhá podmínka se splní pokud máme záporné číslo na začátku
                        result.Push(Convert.ToDouble(prefix[j])); //odečtení char hodnoty '0'
                    else
                    {
                        if (result.Count > 1)
                        {
                            double o2 = result.Pop();    //výběr operandů
                            double o1 = result.Pop();
                            switch (prefix[j])  //switch operací a push na stack
                            {
                                case "+":
                                    result.Push(o1 + o2);
                                    break;
                                case "-":
                                    result.Push(o1 - o2);
                                    break;
                                case "*":
                                    result.Push(o1 * o2);
                                    break;
                                case "/":
                                    result.Push(o1 / o2);
                                    break;
                            }
                        }
                    }
            }           
            return result.Pop();
        }

        private int Priority(char c)
        {
            if (c == '+' || c == '-')
                return 2;
            if (c == '*' || c == '/')
                return 3;
            return 0;
        }

        public void ResultOperation()
        {
            string text = _textBlock.Text;
            List<string> prefix = InfixToPrefix(_textBlock.Text);
            _textBlock.Text = evaluatePrefix(prefix).ToString();
            ListBoxItem itm = new ListBoxItem();
            itm.Content = text+" = "+_textBlock.Text;
            _history.Items.Add(itm);
            _history.SelectedIndex = _history.Items.Count - 1;
            _history.ScrollIntoView(_history.SelectedItem);
            _stackOperator.Clear();
        }

        public void ClearOperation()
        {
            _textBlock.Text = "";
            _stackOperator.Clear();
            _history.Items.Clear();
        }

        public void Negation()
        {
            ResultOperation();
            double val = Convert.ToDouble(_textBlock.Text);
            val *= -1;
            ListBoxItem itm = new ListBoxItem();
            itm.Content = val*-1 + "*(-1) = " + val;
            _history.Items.Add(itm);
            _history.SelectedIndex = _history.Items.Count - 1;
            _history.ScrollIntoView(_history.SelectedItem);
            _textBlock.Text = val.ToString();
        }
    }
}
