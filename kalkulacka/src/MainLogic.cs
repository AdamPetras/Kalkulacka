using System.Windows.Controls;

namespace kalkulacka.src
{
    class MainLogic
    {
        private TextBlock _textBlock;
        private ListBox _history;
        private float memValue1;
        private float memValue2;
        private EOperation _oper;
                public bool IsResultOnDisplay { get; set; }

        public MainLogic(TextBlock textBlock, ListBox historyList)
        {
            memValue1 = 0;
            memValue2 = 0;
            _oper = EOperation.NONE;
            _textBlock = textBlock;
            _history = historyList;
        }

        public void ResultOperation()
        {
            if (_textBlock.Text == "" || _oper == EOperation.NONE)
            {
                return;
            }
            Calculate(memValue1, float.Parse(_textBlock.Text), _oper);
            memValue1 = 0;
            memValue2 = 0;
            IsResultOnDisplay = true;
            
        }

        public void ClearOperation()
        {
            _textBlock.Text = "";
            memValue1 = 0; 
            memValue2 = 0;
            _history.Items.Clear();
            _oper = EOperation.NONE;
        }

        public void Negation()
        {
            if (float.TryParse(_textBlock.Text, out memValue1))
            {
                memValue1 *= -1;
                _textBlock.Text = memValue1.ToString();
                AddItemToHistory(memValue1*-1,-1,memValue1,EOperation.MUL);
                memValue1 = 0;
                IsResultOnDisplay = true;
            }
        }

        private void AddItemToHistory(float value1, float value2, float result, EOperation oper)
        {
            ListBoxItem itm = new ListBoxItem();
            itm.Content = new HistoryItem(value1, value2, result, oper).ToString();
            _history.Items.Add(itm);
            _history.SelectedIndex = _history.Items.Count - 1;  //nastavení scrollbaru na poslední výpočet
            _history.ScrollIntoView(_history.SelectedItem);
        }

        private void Calculate(float value1, float value2, EOperation oper)
        {
            if (oper == EOperation.ADD)
            {
                memValue1 = value1 + value2;
                _textBlock.Text = memValue1.ToString();
            }
            if (oper == EOperation.SUB)
            {
                memValue1 = value1 - value2;
                _textBlock.Text = memValue1.ToString();
            }
            if (oper == EOperation.MUL)
            {
                memValue1 = value1 * value2;
                _textBlock.Text = memValue1.ToString();
            }
            if (oper == EOperation.DIV)
            {
                if (value2 != 0)
                {
                    memValue1 = value1 / value2;
                    _textBlock.Text = memValue1.ToString();
                }
                else _textBlock.Text = "DIV Err";
            }
            AddItemToHistory(value1, value2, memValue1, oper);
        }

        public void MathOperation(EOperation oper)
        {
            if (_textBlock.Text == "")
            {
                return;            
            }
            float value = float.Parse(_textBlock.Text);
            if (memValue1 == 0)
            {
                memValue1 = value;
                _textBlock.Text = "";
                this._oper = oper;
            }
            else
            {
                Calculate(memValue1, float.Parse(_textBlock.Text), oper);
                IsResultOnDisplay = true;
                this._oper = oper;
            }       
        }
    }
}
