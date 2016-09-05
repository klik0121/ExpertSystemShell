using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    /// <summary>
    /// Грамматика для выражений в левой части продукционного правила.
    /// </summary>
    /// <seealso cref="Diggins.Jigsaw.SharedGrammar" />
    public class ProductionExprGrammar: SharedGrammar
    {
        public static Rule AndOp = MatchStringSet("И и & &&");
        public static Rule OrOp = MatchStringSet("ИЛИ или Или | ||");
        public static Rule NotOp = Node(MatchStringSet("Не НЕ не !"));
        public static Rule EqualityOp = ProductionFactGrammar.EqualityOp;
        public static Rule BinaryOp = Node(OrOp | AndOp);
        public static Rule LeftParan = Node(MatchChar('('));
        public static Rule RightParan = Node(MatchChar(')'));
        public static Rule Property = ProductionFactGrammar.Property;
        public static Rule Value = ProductionFactGrammar.Value;
        public static Rule ProdFact = (MatchChar('\'') + WS + Property + WS +
            EqualityOp + WS + Value + WS + MatchChar('\''));

        public static Rule RecExp = Recursive(() => Expression);
        public static Rule PrefixExpr = NotOp + RecExp;
        public static Rule ParanExp = LeftParan + WS + RecExp + WS + RightParan;
        public static Rule SimpleExp = (PrefixExpr | ParanExp | ProdFact);
        public static Rule Expression = Node(SimpleExp + ZeroOrMore(WS + BinaryOp + WS + SimpleExp));

        static ProductionExprGrammar()
        {
            InitGrammar(typeof(ProductionExprGrammar));
        }
    }
}
