using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class ProductionRule: ILogicalStatement
    {
        protected string name;
        protected Expression condition;
        protected IEnumerable<IKnowledgeBaseAction> actions;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Expression Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public IEnumerable<IKnowledgeBaseAction> Actions
        {
            get { return actions; }
            set { actions = value; }
        }
        public bool HasName
        {
            get { return name != null; }
        }

        public ProductionRule(string name, Expression condition,
            IEnumerable<IKnowledgeBaseAction> actions)
        {
            this.name = name;
            this.condition = condition;
            this.actions = actions;
        }

        #region ILogicalStatement Members

        public void Execute(IKnowledgeBase knBase)
        {
            foreach (var action in actions)
                action.Execute(knBase);
        }

        #endregion
    }
}
