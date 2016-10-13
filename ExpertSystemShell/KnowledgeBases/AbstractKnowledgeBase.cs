using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.StorageServices;

namespace ExpertSystemShell.KnowledgeBases
{
    /// <summary>
    /// Абстрактный класс для баз знаний. Предполагает замену хранилища базы "на лету". 
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.IKnowledgeBase" />
    public abstract class AbstractKnowledgeBase: IKnowledgeBase
    {
        protected IStorageService stService;
        protected KnowledgeBaseType type; //???
        protected bool stateChanged;

        public abstract IEnumerable<IData> CurrentData
        {
            get;
        }
        public bool StateChanged
        {
            get { return stateChanged; }
        }
        public abstract IEnumerable<ILogicalStatement> ActiveSet
        {
            get;
        }

        public AbstractKnowledgeBase(IStorageService stService)
        {
            this.stService = stService;
        }

        #region IKnowledgeBase Members

        /// <summary>
        /// Добавляет заданное логическое утвреждение к существующей базе знаний.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public virtual void AddStatement(ILogicalStatement statement)
        {
            stateChanged = true;
            stService.AddStatement(statement);
        }
        /// <summary>
        /// Удаляет заданной логическое утверждение из существующе базы.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public virtual void RemoveStatement(ILogicalStatement statement)
        {
            stateChanged = true;
            stService.RemoveStatement(statement);
        }
        /// <summary>
        /// Проверяет на конфликк два правила. Возвращает true, если два правила конфликуют.
        /// </summary>
        /// <param name="st1">The ST1.</param>
        /// <param name="st2">The ST2.</param>
        /// <returns></returns>
        public abstract bool CheckConflict(ILogicalStatement st1, ILogicalStatement st2);
        /// <summary>
        /// Разрешает логические конфликты в базе.
        /// </summary>
        public abstract void RemoveConflicts();

        /// <summary>
        /// Добавляет элементарные знания в рабочую память.
        /// </summary>
        /// <param name="data"></param>
        public abstract void AddData(IData data);
        /// <summary>
        /// Очищает рябочую память.
        /// </summary>
        public abstract void ClearWorkMemory();
        /// <summary>
        /// Проверяет истинность логического высказывания.
        /// </summary>
        /// <param name="statement">Логическое утверждение..</param>
        /// <returns>
        /// Возвращает <c>true</c>, если правило можно выполнить.
        /// </returns>
        public abstract bool CheckStatement(ILogicalStatement statement);
        /// <summary>
        /// Заменяет значение данных на новое значение.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">новое значение.</param>
        public abstract void ChangeData(IData oldValue, IData newValue);
        /// <summary>
        /// Удаляет данные из рабочей памяти.
        /// </summary>
        /// <param name="data">данные.</param>
        public abstract void DeleteData(IData data);

        #endregion

        #region IEnumerable<ILogicalStatement> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ILogicalStatement> GetEnumerator()
        {
            return stService.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
