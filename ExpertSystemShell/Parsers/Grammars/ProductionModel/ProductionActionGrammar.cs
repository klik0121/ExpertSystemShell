using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    /// <summary>
    /// Грамматика для правой части правила продукционной модели.
    /// </summary>
    /// <seealso cref="Diggins.Jigsaw.SharedGrammar" />
    public class ProductionActionGrammar: SharedGrammar
    {
        //пока есть только одно действие - добавление одного или нескольких фактов в базу

        public static Rule AddFact = OneOrMore(ProductionFactGrammar.ProductionFact);
        public static Rule ProductionAction = AddFact;
        public static Rule Delimiter = MatchStringSet(",");
        public static Rule ProductionActionList = Node(ProductionAction +
            ZeroOrMore(WS + Delimiter + WS + ProductionAction + WS));


        static ProductionActionGrammar()
        {
            InitGrammar(typeof(ProductionActionGrammar));
        }
    }
}
