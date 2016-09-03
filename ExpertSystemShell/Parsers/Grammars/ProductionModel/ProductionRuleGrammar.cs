using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    public class ProductionRuleGrammar: SharedGrammar
    {
        public static Rule If = MatchStringSet("ЕСЛИ Если если");
        public static Rule Then = MatchStringSet("ТО То то");

        public static Rule ProductionRule = Node(If + WS +
            ProductionExpressionGrammar.Expression + WS + Then + WS +
            ProductionActionGrammar.ProductionActionList);
        public static Rule Delimiter = MatchStringSet(";");
        public static Rule ProductionRuleList = Node(WS + ProductionRule +
            ZeroOrMore(WS + Delimiter + WS + ProductionRule) + WS);

        static ProductionRuleGrammar()
        {
            InitGrammar(typeof(ProductionRuleGrammar));
        }
    }
}
