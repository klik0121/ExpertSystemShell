using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    public class ProductionFactQuery: ILogicalQuery
    {
        protected IEnumerable<IKnowledgeBaseAction> inputFacts;
        protected IEnumerable<ProductionFact> queriedFacts;

        public ProductionFactQuery(IEnumerable<IKnowledgeBaseAction> inputFacts,
            IEnumerable<ProductionFact> queriedFacts)
        {
            this.inputFacts = inputFacts;
            this.queriedFacts = queriedFacts;
        }

        #region ILogicalQuery Members

        public IEnumerable<IData> GetQueriedItems()
        {
            return queriedFacts;
        }
        public IEnumerable<IKnowledgeBaseAction> GetPreQueryActions()
        {
            return inputFacts;
        }

        #endregion
    }
}
