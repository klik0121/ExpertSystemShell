using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class ProductionRule: ILogicalStatement
    {
        protected string name;

        #region ILogicalStatement Members

        public void Execute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
