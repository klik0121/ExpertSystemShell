using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases
{
    /// <summary>
    /// Представляет элементарные данные в базе данных.
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// Имя переменной.
        /// </summary>
        string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Значение лингвистической переменной.
        /// </summary>
        string Value
        {
            get;
            set;
        }
    }
}
