using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ExpertSystemShell.Builders
{
    public interface IProductionActionBuilder
    {
        IProductionAction Build();
    }
}
