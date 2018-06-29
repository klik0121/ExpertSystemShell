using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    public class SimplicityFirstDirectSolver: DirectProductionSolver
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DirectProductionSolver"/>.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        public SimplicityFirstDirectSolver(IKnowledgeBase knBase) : base(knBase)
        {
        }

        /// <summary>
        /// Выбирает одно правило из оставшихся равноправных правил, используя какую-нибудь
        /// эвристику (хоть рандомом).
        /// </summary>
        /// <param name="statements">Список правил в порядке активации</param>
        /// <returns></returns>
        public override ILogicalStatement ChooseOne(ICollection<ILogicalStatement> statements)
        {
            //specificity - глубина правила (чем больше сравнений - тем больше глубина)
            //выбираем правило с наименьшей глубиной
            var mostSimple = statements.OrderBy(s => s.Specificity).First();
            return mostSimple;
        }
    }
}
