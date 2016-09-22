using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases
{
    /// <summary>
    /// Интерфейс для базы знаний (буть то фреймоваия, продукционная или семантическая модель).
    /// </summary>
    public interface IKnowledgeBase: IEnumerable<ILogicalStatement>
    {
        IEnumerable<IData> CurrentData
        {
            get;
        }
        bool StateChanged
        {
            get;
        }

        /// <summary>
        /// Добавляет заданное логическое утвреждение к существующей базе знаний.
        /// </summary>
        /// <param name="statement">The statement.</param>
        void AddStatement(ILogicalStatement statement);
        /// <summary>
        /// Удаляет заданной логическое утверждение из существующе базы.
        /// </summary>
        /// <param name="statement">The statement.</param>
        void RemoveStatement(ILogicalStatement statement);
        /// <summary>
        /// Проверяет истинность логического высказывания.
        /// </summary>
        /// <param name="statement">Логическое утверждение..</param>
        /// <returns>Возвращает <c>true</c>, если правило можно выполнить.</returns>
        bool CheckStatement(ILogicalStatement statement);
        /// <summary>
        /// Проверяет на конфликк два правила. Возвращает true, если два правила конфликуют.
        /// </summary>
        /// <param name="st1">The ST1.</param>
        /// <param name="st2">The ST2.</param>
        /// <returns></returns>
        bool CheckConflict(ILogicalStatement st1, ILogicalStatement st2);
        /// <summary>
        /// Добавляет элементарные знания в рабочую память.
        /// </summary>
        /// <param name="IData">Элементарные знания.</param>
        void AddData(IData data);
        /// <summary>
        /// Очищает рябочую память.
        /// </summary>
        void ClearWorkMemory();
        /// <summary>
        /// Разрешает логические конфликты в базе.
        /// </summary>
        void RemoveConflicts();
    }
}
