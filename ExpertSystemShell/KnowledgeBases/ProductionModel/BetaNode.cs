using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Бета-нод сети RETE.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ReteNode" />
    public class BetaNode: ReteNode
    {
        protected byte waitingInputs;
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BetaNode"/>.
        /// </summary>
        public BetaNode(): base(2, 0)
        {
            waitingInputs = 2;
            IsActive = false;
        }

        /// <summary>
        /// Добавляет новое правило в сеть.
        /// </summary>
        /// <param name="statement">Новое правило.</param>
        /// <param name="parent">Текущий родитель.</param>
        /// <param name="end">Конечный нод сети.</param>
        public override void AddStatement(ProductionRule statement, ReteNode parent, AgendaNode end)
        {
            if (!this.outputs.Any((a) => { return a is BetaMemoryNode; }))
            {
                this.outputs.Add(new BetaMemoryNode());
                this.outputs.Last().Inputs.Add(this);
            }
            base.AddStatement(statement, this, end);
        }


        /// <summary>
        /// Пропускает факт через сеть с корнем в текущем ноде.
        /// </summary>
        /// <param name="fact">Факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public override void AddFact(IData fact, ReteNode parent)
        {
            var other = inputs[0] == parent ? inputs[1] : inputs[0];
            var otherAlpha = other as AlphaNode;
            if (otherAlpha != null && otherAlpha.AlphaMemory.Any())
            {
                IsActive = true;
                base.AddFact(fact, parent);
            }
            var otherBeta = other as BetaNode;
            if (otherBeta != null && otherBeta.IsActive)
            {
                IsActive = true;
                base.AddFact(fact, parent);
            }
        }
        /// <summary>
        /// Сливает текущий нод с заданным.
        /// </summary>
        /// <param name="node">Второй нод.</param>
        /// <returns>
        /// Возвращает новый экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ReteNode" />,
        /// который является наследником двух заданных нодов.
        /// </returns>
        public override ReteNode Merge(ReteNode node)
        {
            ReteNode result = base.Merge(node);
            this.outputs.Add(result);
            if (node is AlphaNode)
            {
                node.Outputs.Add(result);
            }
            else
                node.Outputs.Add(result);
            return result;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return inputs[0].ToString() + " & " + inputs[1].ToString();
        }
    }
}
