using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;
using ExpertSystemShell.Parsers.Grammars;
using System.Text.RegularExpressions;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    public class ProductionQueryGrammar: SharedGrammar
    {
        public static Rule IfKeyword = MatchRegex(new Regex("если", RegexOptions.IgnoreCase));
        public static Rule If = Node(WS + IfKeyword + WS + ProductionActionGrammar.ProductionAction + 
            ZeroOrMore(WS + Comma + WS + ProductionActionGrammar.ProductionAction));
        public static Rule ThenKeyword = MatchRegex(new Regex("то", RegexOptions.IgnoreCase));
        public static Rule FactName = Node(Pattern(@"(\w)(\s*\w)*(?=\s*=\?)"));
        public static Rule GetOp = MatchString("=?");
        public static Rule AtomicFactQuery = FactName + GetOp + ZeroOrMore(WS + FactName + WS + GetOp);
        public static Rule Then = Node(ThenKeyword + WS + AtomicFactQuery + WS);
        public static Rule Query = Node(If + WS + Then);

        static ProductionQueryGrammar()
        {
            InitGrammar(typeof(ProductionQueryGrammar));
        }
        
    }
}
