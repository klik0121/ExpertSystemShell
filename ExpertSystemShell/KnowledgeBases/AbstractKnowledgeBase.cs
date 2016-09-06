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
            stService.AddStatement(statement);
        }

        /// <summary>
        /// Удаляет заданной логическое утверждение из существующе базы.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public virtual void RemoveStatement(ILogicalStatement statement)
        {
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

        #endregion
    }
}
