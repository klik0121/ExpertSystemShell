using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class AddFactAction: IProductionAction
    {
        protected ProductionFact fact;

        public ProductionFact Fact
        {
            get { return fact; }
            set { fact = value; }
        }

        public AddFactAction(ProductionFact fact)
        {
            this.fact = fact;
        }

        #region IProductionAction Members

        public void Execute(IKnowledgeBase knBase)
        {
            ((ProdRuleKnBase)knBase).AddFact(Fact);
        }

        #endregion
    }
}
