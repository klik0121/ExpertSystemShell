using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Реализация операций над нечёткими множествами.
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Максиминная.
        /// </summary>
        MinMax,
        /// <summary>
        /// Алгебраическая.
        /// </summary>
        Algebraic,
        /// <summary>
        /// Альтернативная максиминная.
        /// </summary>
        MinMaxAlternative,
        /// <summary>
        /// Условная.
        /// </summary>
        Conditional,
        /// <summary>
        /// Степенная.
        /// </summary>
        Exponential
    }
}
