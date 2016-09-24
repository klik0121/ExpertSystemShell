using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Представляет вызов функции.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Expressions.Expression" />
    public class FunctionCall: Expression
    {
        protected string name;
        protected Func<dynamic[], dynamic> action;
        protected Expression[] args;
        protected int argsCount;

        /// <summary>
        /// Аргументы функции.
        /// </summary>
        public Expression[] Arguments
        {
            get { return args; }
            set { args = value; }
        }
        /// <summary>
        /// Имя функции.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Количество аргументов.
        /// </summary>
        public int ArgumentsCount
        {
            get { return argsCount; }
            set
            {
                Expression[] argsNew = new Expression[value];
                for(int i = 0; i < args.Length; i++)
                {
                    argsNew[i] = args[i];
                }
                args = argsNew;
                argsCount = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionCall"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="name">The name.</param>
        /// <param name="argsCount">The arguments count.</param>
        public FunctionCall(Func<dynamic[], dynamic> action, string name = "", int argsCount = 0)
        {
            this.name = name;
            this.action = action;
            this.args = new Expression[argsCount];
            this.argsCount = argsCount;
        }

        /// <summary>
        /// Возвращает результат вычисления выражения.
        /// </summary>
        /// <returns>
        /// Возвращает результат вычисления выражения.
        /// </returns>
        public override dynamic Calculate()
        {
            if (this.args == null) return action(null); 
            dynamic[] computedArgs = new object[args.Length];
            for(int i = 0; i < args.Length; i++)
            {
                computedArgs[i] = args[i].Calculate();
            }
            return action(computedArgs);
        }

        /// <summary>
        /// Упрощает данное выражение.
        /// </summary>
        /// <returns>
        /// Возвращает упрощённое выражение.
        /// </returns>
        public override Expression Simplify()
        {
            //if(this.args == null || this.args.Length == 0) return new Constant(this.Calculate());
            //bool toConstant = true;
            //for (int i = 0; i < args.Length; i++)
            //{
            //    args[i] = args[i].Simplify();
            //    toConstant &= args[i] is Constant;
            //}
            //if (toConstant) return new Constant(this.Calculate());
            //else return this;
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
                foreach(Expression exp in args)
                {
                    names.UnionWith(exp.VariableNames);
                }
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
            foreach(Expression exp in args)
            {
                exp.SetVariable<T>(name, value);
            }
        }
        /// <summary>
        /// Устанавливает значение переменной с заданным иеменем.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">Значение переменной..</param>
        public override void SetVariable(string name, dynamic value)
        {
            foreach (Expression exp in args)
            {
                exp.SetVariable(name, value);
            }
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

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            return new FunctionCall(action, name, argsCount);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = name + "(";
            for(int i = 0; i < args.Length; i++)
            {
                if (i == args.Length - 1)
                {
                    result += args[i].ToString() + ")";
                }
                else result += args[i].ToString() + ", ";
            }
            return result;
        }

        public override List<Expression> Descendants
        {
            get 
            {
                return args.ToList();
            }
        }

        /// <summary>
        /// Создаёт полную копию текущего выражения.
        /// </summary>
        /// <returns>
        /// Возвращает полную копию текущего выражения.
        /// </returns>
        public override Expression Copy()
        {
            FunctionCall call = new FunctionCall(action, name, argsCount);
            call.args = (Expression[])this.args.Clone();
            return call;
        }
    }
}
