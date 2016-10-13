using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ReteNode" />
    public class AgendaNode: ReteNode
    {
        protected List<string> ready; //содержит имена правил, готовых к выполнению

        /// <summary>
        /// Список имён, готовых к выполнению правил.
        /// </summary>
        public List<string> Ready
        {
            get { return ready; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AgendaNode"/>.
        /// </summary>
        public AgendaNode(): base(0, 0)
        {
            ready = new List<string>();
        }

        /// <summary>
        /// Пропускает факт через сеть с корнем в текущем ноде.
        /// </summary>
        /// <param name="fact">Факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public override void AddFact(IData fact, ReteNode parent)
        {
            BetaMemoryNode bmn = (BetaMemoryNode)parent;
            foreach(var item in bmn.BetaMemory)
            {
                if (!ready.Contains(item))
                    ready.Add(item);
            }
        }
        /// <summary>
        /// Удаляет факт из сети с корнем к текущем ноде.
        /// </summary>
        /// <param name="fact">Удаляемый факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public override void RemoveFact(IData fact, ReteNode parent)
        {
            BetaMemoryNode bmn = (BetaMemoryNode)parent;
            foreach (var item in bmn.BetaMemory)
            {
                if (!ready.Contains(item))
                    ready.Remove(item);
            }
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "AGENDA";
        }
    }
}
