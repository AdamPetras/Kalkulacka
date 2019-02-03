using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace kalkulacka.src
{
    class MainLogic
    {
        private TextBlock _textBlock;
        private float memValue1;
        private float memValue2;
        private EOperation _oper;
        public bool IsResultOnDisplay { get; set; }

        public MainLogic(TextBlock textBlock)
        {
            memValue1 = 0;
            memValue2 = 0;
            _oper = EOperation.NONE;
            _textBlock = textBlock;
        }

        public void ResultOperation()
        {
            if (_textBlock.Text == "")
            {
                return;
            }
            Calculate(memValue1, float.Parse(_textBlock.Text, CultureInfo.InvariantCulture.NumberFormat), _oper);
            memValue1 = 0;
            memValue2 = 0;
            IsResultOnDisplay = true;
            
        }

        public void ClearOperation()
        {
            _textBlock.Text = "";
            memValue1 = 0;
            memValue2 = 0;
            _oper = EOperation.NONE;
        }

        public void Negation()
        {
            if (float.TryParse(_textBlock.Text, out memValue1))
            {
                memValue1 *= -1;
                _textBlock.Text = memValue1.ToString();
            }
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
        }

        public void MathOperation(EOperation oper)
        {
            if (_textBlock.Text == "")
            {
                return;            
            }
            float value = float.Parse(_textBlock.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (memValue1 == 0)
            {
                memValue1 = value;
                _textBlock.Text = "";
                this._oper = oper;
            }
            else
            {
                Calculate(memValue1, float.Parse(_textBlock.Text, CultureInfo.InvariantCulture.NumberFormat), oper);
                IsResultOnDisplay = true;
                this._oper = oper;
            }       
        }
    }
}
