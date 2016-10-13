using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.StorageServices;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Сеть RETE для модели продукционных правил.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.AbstractKnowledgeBase" />
    public class ProductionModelReteNetwork: AbstractKnowledgeBase
    {
        protected StartNode start;
        protected AgendaNode agenda;
        protected List<IData> currentData;

        /// <summary>
        /// Инициализирует новый экземлпяр класса <see cref="ProductionModelReteNetwork"/>.
        /// </summary>
        /// <param name="stService">Сервис хранения правил.</param>
        public ProductionModelReteNetwork(IStorageService stService): base(stService)
        {
            this.start = new StartNode();
            this.agenda = new AgendaNode();
            currentData = new List<IData>();
        }

        /// <summary>
        /// Добавляет заданное логическое утвреждение к существующей базе знаний.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public override void AddStatement(ILogicalStatement statement)
        {
            start.AddStatement((ProductionRule)statement, null, agenda);
            base.AddStatement(statement);
        }

        public override IEnumerable<IData> CurrentData
        {
            get { return currentData; }
        }
        public override IEnumerable<ILogicalStatement> ActiveSet
        {
            get 
            {
                stateChanged = false;
                return stService.Where((a) => { return agenda.Ready.Contains(a.Name); });
            }
        }

        /// <summary>
        /// Проверяет на конфликк два правила. Возвращает true, если два правила конфликуют.
        /// </summary>
        /// <param name="st1">The ST1.</param>
        /// <param name="st2">The ST2.</param>
        /// <returns></returns>
        public override bool CheckConflict(ILogicalStatement st1, ILogicalStatement st2)
        {
            return false;
        }
        /// <summary>
        /// Разрешает логические конфликты в базе.
        /// </summary>
        public override void RemoveConflicts()
        {
            return;
        }
        /// <summary>
        /// Добавляет элементарные знания в рабочую память.
        /// </summary>
        /// <param name="data"></param>
        public override void AddData(IData data)
        {
            IData d = currentData.FirstOrDefault((a) => { return a.Name == data.Name; });
            if (d != null)
            {
                if (d.Value != data.Value)
                {
                    start.ChangeFact(d, data, null);
                    d.Value = data.Value;
                    stateChanged = true;
                }
            }
            else
            {
                currentData.Add(data);
                start.AddFact(data, null);
                stateChanged = true;
            }           
        }
        /// <summary>
        /// Очищает рябочую память.
        /// </summary>
        public override void ClearWorkMemory()
        {
            foreach(IData fact in currentData)
            {
                start.RemoveFact(fact, null);
            }
            currentData.Clear();
            stateChanged = true;
        }
        /// <summary>
        /// Проверяет истинность логического высказывания.
        /// </summary>
        /// <param name="statement">Логическое утверждение..</param>
        /// <returns>
        /// Возвращает <c>true</c>, если правило можно выполнить.
        /// </returns>
        public override bool CheckStatement(ILogicalStatement statement)
        {
            return agenda.Ready.Contains(statement.Name);
        }
        /// <summary>
        /// Заменяет значение данных на новое значение.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">новое значение.</param>
        public override void ChangeData(IData oldValue, IData newValue)
        {
            IData d = currentData.FirstOrDefault((a) => { return a.Name == oldValue.Name; });
            if (d != null)
            {
                if (d.Value != oldValue.Value)
                {
                    start.ChangeFact(d, newValue, null);
                    d.Value = newValue.Value;
                    stateChanged = true;
                }
            }
            else
            {
                start.AddFact(newValue, null);
                stateChanged = true;
            }  
        }
        /// <summary>
        /// Удаляет данные из рабочей памяти.
        /// </summary>
        /// <param name="data">данные.</param>
        public override void DeleteData(IData data)
        {
            if(currentData.Contains(data))
            {
                currentData.Remove(data);
                start.RemoveFact(data, null);
                stateChanged = true;
            }
        }
    }
}
