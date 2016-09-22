using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Представляет оператор.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Expressions.Expression" />
    public abstract class Operator: Expression
    {
        protected int paramCount;
        protected string sign;
        protected Associativity associativity;
        protected int precendence;

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <param name="associativity">The associativity.</param>
        /// <param name="paramCount">The parameter count.</param>
        /// <param name="precendence">The precendence.</param>
        public Operator(string sign = "", Associativity associativity = Expressions.Associativity.None,
            int paramCount = 1, int precendence = 0)
        {
            this.associativity = associativity;
            this.sign = sign;
            this.paramCount = paramCount;
            this.precendence = precendence;
        }

        /// <summary>
        /// Ассоциативность оператора.
        /// </summary>
        public Associativity Associativity
        {
            get { return this.associativity; }
        }
        /// <summary>
        /// Знак оператора.
        /// </summary>
        public string Sign
        {
            get { return sign; }
        }
        /// <summary>
        /// Количество параметров.
        /// </summary>
        public int ParametersCount
        {
            get { return paramCount; }
        }
        /// <summary>
        /// Получает преоритет.
        /// </summary>
        public int Precendence
        {
            get { return precendence; }
        }
    }
}
