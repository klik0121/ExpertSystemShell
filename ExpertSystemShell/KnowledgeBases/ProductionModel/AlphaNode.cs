using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Альфа-нод сети RETE.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ReteNode" />
    public class AlphaNode: ReteNode
    {
        protected HashSet<IData> alphaMemory; //память для хранения фактов, удовлетворяющих условию
        protected Expression predicate; //условие для проверки

        /// <summary>
        /// Получает или задёт альфа-память, хранящую факты, удовлетворяющие 
        /// текущему предикату.
        /// </summary>
        public HashSet<IData> AlphaMemory
        {
            get { return alphaMemory; }
        }
        /// <summary>
        /// Получает или задаёт предикат.
        /// </summary>
        public Expression Predicate
        {
            get { return predicate; }
            set { predicate = value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AlphaNode"/>.
        /// </summary>
        public AlphaNode(): base(1, 0)
        {
            this.predicate = null;
            this.alphaMemory = new HashSet<IData>();
        }

        /// <summary>
        /// Удаляет факт из сети с корнем к текущем ноде.
        /// </summary>
        /// <param name="fact">Удаляемый факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public override void RemoveFact(IData fact, ReteNode parent)
        {
            alphaMemory.Remove(fact);
            if(alphaMemory.Count == 0) //выполнимых фактов не осталось
                base.RemoveFact(fact, this);
        }
        /// <summary>
        /// Пропускает факт через сеть с корнем в текущем ноде.
        /// </summary>
        /// <param name="fact">Факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public override void AddFact(IData fact, ReteNode parent)
        {
            var clonedPred = predicate.Copy();
            clonedPred.SetVariable(fact.Name, fact);
            if(clonedPred.Calculate().Equals(true))
            {
                alphaMemory.Add(fact);
                base.AddFact(fact, this);
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
        /// Добавляет новое правило в сеть.
        /// </summary>
        /// <param name="statement">Новое правило.</param>
        /// <param name="parent">Текущий родитель.</param>
        /// <param name="end">Конечный нод сети.</param>
        public override void AddStatement(ProductionRule statement, ReteNode parent, AgendaNode end)
        {
            if(this.outputs.Count == 0)
            {
                this.outputs.Add(new BetaMemoryNode());
                this.outputs[0].Inputs.Add(this);
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
            return predicate.ToString();
        }
    }
}
