using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Нод бета-памяти бета-сети RETE.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ReteNode" />
    public class BetaMemoryNode: ReteNode
    {
        protected HashSet<string> betaMemory; //хранит только имена правил, которые подходят

        /// <summary>
        /// Получает или задаёт имна правил, хранящиеся в бета-памяти.
        /// </summary>
        public HashSet<string> BetaMemory
        {
            get { return betaMemory; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BetaMemoryNode"/>.
        /// </summary>
        public BetaMemoryNode(): base(0, 1)
        {
            betaMemory = new HashSet<string>();
        }
        /// <summary>
        /// Добавляет новое правило в сеть.
        /// </summary>
        /// <param name="statement">Новое правило.</param>
        /// <param name="parent">Текущий родитель.</param>
        /// <param name="end">Конечный нод сети.</param>
        public override void AddStatement(ProductionRule statement, ReteNode parent, AgendaNode end)
        {
            if(outputs[0] == null)
            {
                outputs[0] = end;
                end.Inputs.Add(this);
            }
            if(!betaMemory.Contains(statement.Name))
            {
                betaMemory.Add(statement.Name);
            }
            base.AddStatement(statement, parent, end);
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = "BetaMemory: ";
            foreach(var item in betaMemory)
            {
                result += item + "   ";
            }
            return result;
        }
    }
}
