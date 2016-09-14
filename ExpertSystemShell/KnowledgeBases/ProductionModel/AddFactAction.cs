using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Действие по добавлению элементарного факта в базу знаний.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.IKnowledgeBaseAction" />
    public class AddFactAction: IKnowledgeBaseAction
    {
        protected ProductionFact fact;

        /// <summary>
        /// Получает или задаёт факт, который должен быть добавлен в базу знаний.
        /// </summary>
        public ProductionFact Fact
        {
            get { return fact; }
            set { fact = value; }
        }

        /// <summary>
        /// инициализирует новый экземпляр <see cref="AddFactAction"/>.
        /// </summary>
        /// <param name="fact">Добавляемый факт.</param>
        public AddFactAction(ProductionFact fact)
        {
            this.fact = fact;
        }

        #region IProductionAction Members

        /// <summary>
        /// Выполняет текущее действие над заданной базой знаний.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        public void Execute(IKnowledgeBase knBase)
        {
            knBase.AddData(fact);
        }

        #endregion
    }
}
