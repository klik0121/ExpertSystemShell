using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class ResolutionMethod
    {
        protected LogicalExpressionHelper eh = new LogicalExpressionHelper();

        public ResolutionMethod()
        {

        }

        public bool CheckConflict(ProductionRule rule1, ProductionRule rule2)
        {
            return false;
        }
    }
}
