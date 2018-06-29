using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    /// <summary>
    /// Грамматика для продукционного правила.
    /// </summary>
    /// <seealso cref="Diggins.Jigsaw.SharedGrammar" />
    public class ProductionRuleGrammar: SharedGrammar
    {
        public static Rule If = MatchStringSet("ЕСЛИ Если если");
        public static Rule Then = MatchStringSet("ТО То то");
        public static Rule Name = Node(Pattern(@"(?<=\s*)(\w)(\s*\w)*(?=\s*:)"));
        public static Rule OptName = Opt(Name + WS + MatchChar(':') + WS);

        public static Rule ProductionRule = Node(WS + OptName + If + WS +
            ProductionExprGrammar.Expression + WS + Then + WS +
            ProductionActionGrammar.ProductionActionList);
        public static Rule Delimiter = MatchStringSet(";");
        public static Rule ProductionRuleList = Node(WS + ProductionRule +
            ZeroOrMore(WS + Delimiter + WS + ProductionRule) + WS + Opt(Delimiter));

        static ProductionRuleGrammar()
        {
            InitGrammar(typeof(ProductionRuleGrammar));
        }
    }
}
