using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Представляет переменную.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Expressions.Expression" />
    public class Variable: Expression
    {
        protected string name;
        protected dynamic value;

        public override int Specificity
        {
            get { return 0; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Variable(string name):this(name, null)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public Variable(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Возвращает результат вычисления выражения.
        /// </summary>
        /// <returns>
        /// Возвращает результат вычисления выражения.
        /// </returns>
        public override dynamic Calculate()
        {
            return value;
        }
        /// <summary>
        /// Упрощает данное выражение.
        /// </summary>
        /// <returns>
        /// Возвращает упрощённое выражение.
        /// </returns>
        public override Expression Simplify()
        {
            return this;
        }
        /// <summary>
        /// Получает имена переменных в данном выражении.
        /// </summary>
        public override HashSet<string> VariableNames
        {
            get 
            {
                HashSet<string> names = new HashSet<string>();
                names.Add(this.name);
                return names;
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
            if (this.name.Equals(name))
                this.value = value;
        }
        /// <summary>
        /// Устанавливает значение переменной с заданным иеменем.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">Значение переменной..</param>
        public override void SetVariable(string name, dynamic value)
        {
            if (this.name == name)
                this.value = value;
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
                return 1;
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
            return new Variable(name, value);
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return name;
        }

        public override List<Expression> Descendants
        {
            get { return new List<Expression>(); }
        }

        /// <summary>
        /// Создаёт полную копию текущего выражения.
        /// </summary>
        /// <returns>
        /// Возвращает полную копию текущего выражения.
        /// </returns>
        public override Expression Copy()
        {
            return new Variable(name, value);
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
            Variable v = obj as Variable;
            if(v != null)
            {
                return v.name == this.name;
            }
            return false;
        }
    }
}
