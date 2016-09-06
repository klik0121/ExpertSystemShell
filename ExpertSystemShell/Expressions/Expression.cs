using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Представляет собой выражение.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public abstract class Expression: ICloneable
    {
        /// <summary>
        /// Возвращает результат вычисления выражения.
        /// </summary>
        /// <returns>Возвращает результат вычисления выражения.</returns>
        public abstract dynamic Calculate();
        /// <summary>
        /// Упрощает данное выражение.
        /// </summary>
        /// <returns>Возвращает упрощённое выражение.</returns>
        public abstract Expression Simplify();
        /// <summary>
        /// Получает имена переменных в данном выражении.
        /// </summary>
        public abstract HashSet<string> VariableNames
        {
            get;
        }
        /// <summary>
        /// Установить значене переменной с заданным именем.
        /// </summary>
        /// <typeparam name="T">Тип переменной.</typeparam>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">значение переменной.</param>
        public abstract void SetVariable<T>(string name, T value);
        /// <summary>
        /// Устанавливает значение переменной с заданным иеменем.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">Значение переменной..</param>
        public abstract void SetVariable(string name, dynamic value);
        /// <summary>
        /// Получает количество различных переменных в данном выражении.
        /// </summary>
        /// <value>
        /// The variable count.
        /// </value>
        public abstract int VariableCount
        {
            get;
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public abstract object Clone();

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override abstract string ToString();
    }
}
