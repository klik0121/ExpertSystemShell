using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases
{
    public interface IComplexData<T>
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
        T Value
        {
            get;
            set;
        }
    }
}
