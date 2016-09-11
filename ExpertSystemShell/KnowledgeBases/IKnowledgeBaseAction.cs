using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Solvers;

namespace ExpertSystemShell.KnowledgeBases
{
    /// <summary>
    /// Интерфейс для действия с базой знаний.
    /// </summary>
    public interface IKnowledgeBaseAction
    {
        /// <summary>
        /// Выполняет текущее действие над заданной базой знаний.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        void Execute(IKnowledgeBase knBase);
    }
}
