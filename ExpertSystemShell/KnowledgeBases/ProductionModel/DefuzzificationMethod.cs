using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public enum DefuzzificationMethod
    {
        /// <summary>
        /// Метод центра тяжести.
        /// </summary>
        GravityCentre,
        /// <summary>
        /// Метод средних максимумов.
        /// </summary>
        MeanOfMaximus,
        /// <summary>
        /// Метод бисекции???
        /// </summary>
        Median
    }
}
