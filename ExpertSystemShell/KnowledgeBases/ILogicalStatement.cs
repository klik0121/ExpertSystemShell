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
        /// <summary>
        /// Получает или задёт имя текущего утверждения.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Выполняет текущее утверждение в заданной базе знаний.
        /// </summary>
        /// <param name="knBase"База знаний.</param>
        void Execute(IKnowledgeBase knBase);
    }
}
