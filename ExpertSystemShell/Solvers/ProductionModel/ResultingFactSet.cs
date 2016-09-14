using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    public class ResultingFactSet: ILogicalResult
    {
        protected IEnumerable<IData> result;

        public ResultingFactSet(IEnumerable<IData> result)
        {
            this.result = result;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string res = string.Empty;
            foreach(IData data in result)
            {
                res += data.ToString() + ", ";
            }
            return res.Substring(0, res.Length - 2);
        }
    }
}
