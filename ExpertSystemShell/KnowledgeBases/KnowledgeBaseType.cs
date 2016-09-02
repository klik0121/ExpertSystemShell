using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases
{
    /// <summary>
    /// Перечесление, содержащее возможные типы баз знаний.
    /// Возможно, пригодится при реализации паттерна "фабрика".
    /// </summary>
    [Flags]
    public enum KnowledgeBaseType
    {
        /// <summary>
        /// Модель, основанная на правилах, позволяющая представить знания в виде
        /// преложений типа: ЕСЛИ (условие) ТО (действие).
        /// </summary>
        ProductionRules = 1,
        /// <summary>
        /// Модель основанная на фреймах, т.е. структурах, представляющих минимально возможное
        /// описание сущности какого-либо явления, события, ситуации, процесса или объекта.
        /// </summary>
        FrameModel = 2,
        /// <summary>
        /// Модель, сонованная на представлении знаний в виде совокупностей оюъектов (понятий) и
        /// связей между ними (отношений).
        /// </summary>
        SemanticNetwok = 4
    }
}
