using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Нод сети RETE.
    /// </summary>
    public abstract class ReteNode
    {
        protected List<ReteNode> inputs;
        protected List<ReteNode> outputs;

        /// <summary>
        /// Множество входов нода.
        /// </summary>
        public List<ReteNode> Inputs
        {
            get { return inputs; }
            set { inputs = value; }
        }
        /// <summary>
        /// Множество выходов нода.
        /// </summary>
        public List<ReteNode> Outputs
        {
            get { return outputs; }
            set { outputs = value; }
        }

        /// <summary>
        /// Инцииализирует новый экземпляр класса <see cref="ReteNode"/>.
        /// </summary>
        /// <param name="input">Количество входов.</param>
        /// <param name="output">Количество выходов.</param>
        public ReteNode(int input, int output)
        {
            this.inputs = new List<ReteNode>();
            for (int i = 0; i < input; i++)
            {
                inputs.Add(null);
            }
            this.outputs = new List<ReteNode>();
            for (int i = 0; i < output; i++)
            {
                outputs.Add(null);
            }
        }

        /// <summary>
        /// Добавляет новое правило в сеть.
        /// </summary>
        /// <param name="statement">Новое правило.</param>
        /// <param name="parent">Текущий родитель.</param>
        /// <param name="end">Конечный нод сети.</param>
        public virtual void AddStatement(ProductionRule statement, ReteNode parent, AgendaNode end)
        {
            foreach (var output in outputs)
                output.AddStatement(statement, this, end);
        }
        /// <summary>
        /// Пропускает факт через сеть с корнем в текущем ноде.
        /// </summary>
        /// <param name="fact">Факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public virtual void AddFact(IData fact, ReteNode parent)
        {
            foreach (var output in outputs)
                output.AddFact(fact, this);
        }
        /// <summary>
        /// Удаляет факт из сети с корнем к текущем ноде.
        /// </summary>
        /// <param name="fact">Удаляемый факт.</param>
        /// <param name="parent">Текущий родитель.</param>
        public virtual void RemoveFact(IData fact, ReteNode parent)
        {
            foreach (var output in outputs)
                output.RemoveFact(fact, this);
        }
        /// <summary>
        /// Изменяет значение заданного факта в текущей сети.
        /// </summary>
        /// <param name="oldValue">Старое значение факта.</param>
        /// <param name="newValue">Новое значение факта.</param>
        /// <param name="parent">Текущий родитель.</param>
        public virtual void ChangeFact(IData oldValue, IData newValue, ReteNode parent)
        {
            foreach (var output in outputs)
                output.RemoveFact(oldValue, this);
            foreach (var output in outputs)
                output.AddFact(newValue, this);
        }
        /// <summary>
        /// Сливает текущий нод с заданным.
        /// </summary>
        /// <param name="node">Второй нод.</param>
        /// <returns>Возвращает новый экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ReteNode"/>,
        /// который является наследником двух заданных нодов.</returns>
        public virtual ReteNode Merge(ReteNode node)
        {
            BetaNode result = new BetaNode();
            result.inputs[0] = this;
            result.inputs[1] = node;
            return result;
        }
        /// <summary>
        /// Определяет, есть ли у текущего и заданного нода общий потомок.
        /// </summary>
        /// <param name="node">Второй нод.</param>
        /// <param name="ouput">Наследник.</param>
        /// <returns>Возвращает <c>true</c>, если найден наследник, иначе - <c>false</c>.</returns>
        public bool HasCommonOutputWith(ReteNode node, out ReteNode ouput)
        {
            foreach(var n1 in this.outputs)
            {
                foreach(var n2 in node.outputs)
                {
                    if(n1 == n2)
                    {
                        ouput = n1;
                        return true;
                    }
                }
            }
            ouput = null;
            return false;
        }
    }
}
