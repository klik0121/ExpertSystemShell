using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Solvers;

namespace ExpertSystemShell.ExplanationSystems
{
    /// <summary>
    /// Интерфейс для систем объяснения вывода.
    /// </summary>
    public interface IExplanationSystem
    {
        string GetExplanation(IEnumerable<ILogicalResult> conclusion);
    }
}
