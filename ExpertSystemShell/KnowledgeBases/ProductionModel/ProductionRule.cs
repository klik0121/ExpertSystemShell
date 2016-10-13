using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Базовый класс для продукционных правил.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ILogicalStatement" />
    public class ProductionRule: ILogicalStatement
    {
        protected string name;
        protected Expression condition;
        protected IEnumerable<IKnowledgeBaseAction> actions;

        /// <summary>
        /// Получает или задёт имя текущего утверждения.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Получает или задаёт условие в левой части правила.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public Expression Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        /// <summary>
        /// Получает или задаёт список действий в правой чсти правила.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        public IEnumerable<IKnowledgeBaseAction> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductionRule"/>.
        /// </summary>
        /// <param name="name">TИмя правила.</param>
        /// <param name="condition">Условие правила.</param>
        /// <param name="actions">Действия в правой части правила.</param>
        public ProductionRule(string name, Expression condition,
            IEnumerable<IKnowledgeBaseAction> actions)
        {
            this.name = name;
            this.condition = condition;
            this.actions = actions;
            if(name == null) //если имя не дали, сделаем его сами (оно будет длинным)
            {
                this.name = string.Format("если {0} то {1}", condition.ToString(),
                    ActionsToString()).GetHashCode().ToString();
            }
        }

        #region ILogicalStatement Members

        /// <summary>
        /// Выполняет текущее утверждение в заданной базе знаний.
        /// </summary>
        /// <param name="knBase"База знаний.</param>
        public void Execute(IKnowledgeBase knBase)
        {
            foreach (var action in actions)
                action.Execute(knBase);
        }

        #endregion

        /// <summary>
        /// Возвращает строку, содержащую все действия данного правила.
        /// </summary>
        /// <returns>Возвращает строку, содержащую все действия данного правила.</returns>
        protected string ActionsToString()
        {
            string result = string.Empty;
            foreach(var action in actions)
            {
                result += action.ToString() + ", ";
            }
            return result.Substring(0, result.Length - 2);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Если {0} то {1}", condition.ToString(),
                ActionsToString());
        }
    }
}
