using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Expressions;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Builders
{
    public class ProductionRuleBuilder: IStatementBuilder
    {
        protected ExpressionHelper eh;
        protected Dictionary<string, IProductionActionBuilder> builders;

        public ProductionRuleBuilder(ExpressionHelper eh,
            Dictionary<string, IProductionActionBuilder> builders)
        {
            this.eh = eh;
            this.builders = builders;
        }



        #region IStatementBuilder Members

        public KnowledgeBases.ILogicalStatement Build(Diggins.Jigsaw.Node node)
        {
            
        }

        #endregion
    }
}
