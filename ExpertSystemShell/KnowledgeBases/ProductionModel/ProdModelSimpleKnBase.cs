using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.StorageServices;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.AbstractKnowledgeBase" />
    public class ProdModelSimpleKnBase: AbstractKnowledgeBase
    {
        protected List<IData> workMemory;

        /// <summary>
        /// Gets the current data.
        /// </summary>
        /// <value>
        /// The current data.
        /// </value>
        public override IEnumerable<IData> CurrentData
        {
            get 
            {
                stateChanged = false;
                return workMemory;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProdModelSimpleKnBase"/> class.
        /// </summary>
        /// <param name="stService">Сервис хранения утверждений.</param>
        public ProdModelSimpleKnBase(IStorageService stService): base(stService)
        {
            this.workMemory = new List<IData>();
        }

        /// <summary>
        /// Проверяет на конфликк два правила. Возвращает true, если два правила конфликуют.
        /// </summary>
        /// <param name="st1">The ST1.</param>
        /// <param name="st2">The ST2.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool CheckConflict(ILogicalStatement st1, ILogicalStatement st2)
        {
            return false;
        }
        /// <summary>
        /// Разрешает логические конфликты в базе.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveConflicts()
        {
            throw new NotImplementedException();
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
            ProductionRule rule = (ProductionRule)statement;
            foreach(string variableName in rule.Condition.VariableNames)
            {
                foreach (var fact in workMemory)
                    rule.Condition.SetVariable(fact.Name, fact);
            }
            return rule.Condition.Calculate() == true;
        }

        /// <summary>
        /// Добавляет элементарные знания в рабочую память.
        /// </summary>
        /// <param name="data"></param>
        public override void AddData(IData data)
        {
            for (int i = 0; i < workMemory.Count; i++)
            {
                //если факт найден в рабочей памяти
                if (workMemory[i].Name == data.Name)
                {
                    //если значения различны
                    if (workMemory[i].Value != data.Value)
                    {
                        //меняем состояние рабочей памяти
                        stateChanged = true;
                        workMemory[i].Value = data.Value;
                    }
                    //если 
                    return;
                }
            }
            //факт не найден в рабчоей памяти
            workMemory.Add(data);
            stateChanged = true;
        }

        /// <summary>
        /// Очищает рябочую память.
        /// </summary>
        public override void ClearWorkMemory()
        {
            stateChanged = true;
            workMemory.Clear();
        }
        /// <summary>
        /// Заменяет значение данных на новое значение.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">новое значение.</param>
        public override void ChangeData(IData oldValue, IData newValue)
        {
            foreach(var item in CurrentData)
            {
                if(item.Name == oldValue.Name)
                {
                    item.Value = oldValue.Value;
                }
            }
        }
        /// <summary>
        /// Удаляет данные из рабочей памяти.
        /// </summary>
        /// <param name="data">данные.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteData(IData data)
        {
            workMemory.Remove(data);
        }

        public override IEnumerable<ILogicalStatement> ActiveSet
        {
            get
            {
                return stService.Where((a) => { return CheckStatement(a); });
            }
        }
    }
}
