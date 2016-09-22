using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.KnowledgeBases
{
    /// <summary>
    /// Интерфейс для логического правила (будь то продукционное правило, фрейм или отношение).
    /// </summary>
    public interface ILogicalStatement
    {
        //пока не понятно, что включать в интерфейс
        void Execute(IKnowledgeBase knBase);
    }
}
