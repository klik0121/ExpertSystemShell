using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;
using System.Text.RegularExpressions;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    /// <summary>
    /// Грамматика для распознавания фактов продукционной модели.
    /// </summary>
    /// <seealso cref="Diggins.Jigsaw.SharedGrammar" />
    public class ProductionFactGrammar: SharedGrammar
    {
        public static Rule Property = Node(OneOrMore(ExceptCharSet("-")));
        public static Rule Value = Node(OneOrMore(SharedGrammar.ExceptCharSet("\' -")));
        public static Rule EqualityOp = Node(MatchString("-"));

        //Фактом считается запись '<свойство> - <значение>'
        public static Rule ProductionFact = Node(MatchChar('\'') + WS + Property + WS +
            EqualityOp + WS + Value + WS + MatchChar('\''));

        static ProductionFactGrammar()
        {
            InitGrammar(typeof(ProductionFactGrammar));
        }
    }
}
