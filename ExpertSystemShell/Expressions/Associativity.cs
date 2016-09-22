using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Ассфоциативность оператора.
    /// </summary>
    public enum Associativity
    {
        /// <summary>
        /// Значение для униарных операторов.
        /// </summary>
        None, //value for single operators        
        /// <summary>
        /// Левоассоциативный.
        /// </summary>
        Left,
        /// <summary>
        /// Правоассоциативный.
        /// </summary>
        Right
    }
}
