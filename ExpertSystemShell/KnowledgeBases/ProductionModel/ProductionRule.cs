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
        protected IEnumerable<IProductionAction> actions;

        #region ILogicalStatement Members

        public void Execute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
