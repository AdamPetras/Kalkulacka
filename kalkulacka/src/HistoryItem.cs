using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalkulacka.src
{
    class HistoryItem
    {
        private float _value1;
        private float _value2;
        private float _result;
        private EOperation _operation;

        public HistoryItem(float value1, float value2,float result, EOperation oper)
        {
            _value1 = value1;
            _value2 = value2;
            _result = result;
            _operation = oper;
        }
        private string OperationToString()
        {
            if (_operation == EOperation.ADD)
                return "+";
            if (_operation == EOperation.SUB)
                return "-";
            if (_operation == EOperation.MUL)
                return "*";
            if (_operation == EOperation.DIV)
                return "/";
            if (_operation == EOperation.NOT)
                return "NOT";
            return "NO OPERATION";
        }
        public override string ToString()
        {
            return _value1 + " "+ OperationToString()+ " " + _value2+" = "+_result;
        }
    }
}
