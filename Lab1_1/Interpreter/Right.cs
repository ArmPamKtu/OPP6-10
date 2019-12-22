using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Interpreter
{
    class Right : Expression
    {
        protected Expression _topExp, _botExp = null;

        public Right(Expression topExp, Expression botExp)
        {
            _topExp = topExp;
            _botExp = botExp;
        }

        public override Tuple<int, int> Interpreter()
        {
            return Tuple.Create(_botExp.Interpreter().Item1 + _topExp.Interpreter().Item1,
                _botExp.Interpreter().Item2);
        }
    }
}
