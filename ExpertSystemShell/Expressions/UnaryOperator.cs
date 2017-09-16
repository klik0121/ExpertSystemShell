using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Представляет унарный оператор.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Expressions.Operator" />
    public class UnaryOperator: Operator
    {
        protected Func<dynamic, dynamic> action;
        protected Expression left;

        /// <summary>
        /// Аргумент оператора.
        /// </summary>
        public Expression Left
        {
            get { return left; }
            set { left = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryOperator"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="sign">The sign.</param>
        /// <param name="associativity">The associativity.</param>
        /// <param name="precendence">The precendence.</param>
        public UnaryOperator(Func<dynamic, dynamic> action, string sign = "",
            Associativity associativity = Expressions.Associativity.None, int precendence = 0)
            :base(sign, associativity, 1, precendence)
        {
            this.action = action;
        }

        /// <summary>
        /// Возвращает результат вычисления выражения.
        /// </summary>
        /// <returns>
        /// Возвращает результат вычисления выражения.
        /// </returns>
        public override dynamic Calculate()
        {
            return action(left.Calculate());
        }
        /// <summary>
        /// Упрощает данное выражение.
        /// </summary>
        /// <returns>
        /// Возвращает упрощённое выражение.
        /// </returns>
        public override Expression Simplify()
        {
            this.left = left.Simplify();
            if(this.left is Constant)
            {
                return new Constant(this.Calculate());
            }
            else
            {
                return this;
            }
        }
        /// <summary>
        /// Получает имена переменных в данном выражении.
        /// </summary>
        public override HashSet<string> VariableNames
        {
            get 
            {
                return left.VariableNames;
            }
        }
        /// <summary>
        /// Установить значене переменной с заданным именем.
        /// </summary>
        /// <typeparam name="T">Тип переменной.</typeparam>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">значение переменной.</param>
        public override void SetVariable<T>(string name, T value)
        {
            left.SetVariable<T>(name, value);
        }
        /// <summary>
        /// Устанавливает значение переменной с заданным иеменем.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">Значение переменной..</param>
        public override void SetVariable(string name, dynamic value)
        {
            left.SetVariable(name, value);
        }
        /// <summary>
        /// Получает количество различных переменных в данном выражении.
        /// </summary>
        /// <value>
        /// The variable count.
        /// </value>
        public override int VariableCount
        {
            get 
            {
                return this.VariableNames.Count;
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            UnaryOperator uo = new UnaryOperator(action, sign, associativity, precendence);
            return uo;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return (sign + string.Format("({0})", left.ToString()));
        }

        public override List<Expression> Descendants
        {
            get { return new List<Expression> { left }; }
        }

        /// <summary>
        /// Создаёт полную копию текущего выражения.
        /// </summary>
        /// <returns>
        /// Возвращает полную копию текущего выражения.
        /// </returns>
        public override Expression Copy()
        {
            UnaryOperator uo = new UnaryOperator(action, sign, associativity, precendence);
            uo.left = left.Copy();
            return uo;
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            UnaryOperator uo = obj as UnaryOperator;
            if(uo != null)
            {
                return uo.sign == this.sign && uo.left.Equals(this.left);
            }
            return false;
        }
    }
}
