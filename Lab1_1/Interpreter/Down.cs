using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Interpreter
{
    class Down : Expression
    {
        protected Expression _topExp, _botExp = null;

        public Down(Expression topExp, Expression botExp)
        {
            _topExp = topExp;
            _botExp = botExp;
        }

        public override Tuple<int, int> Interpreter()
        {
            return Tuple.Create(_botExp.Interpreter().Item1,
                _botExp.Interpreter().Item2 + _topExp.Interpreter().Item1);
        }
    }
}
