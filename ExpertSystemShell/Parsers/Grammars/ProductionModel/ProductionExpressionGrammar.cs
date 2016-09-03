using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Parsers.Grammars.ProductionModel
{
    public class ProductionExpressionGrammar: SharedGrammar
    {
        public static Rule AndOp = MatchStringSet("И и");
        public static Rule OrOp = MatchStringSet("ИЛИ или Или");
        public static Rule BinaryOp = Node( OrOp | AndOp);
        public static Rule LeftParan = Node(MatchChar('('));
        public static Rule RightParan = Node(MatchChar(')'));
        public static Rule FactCheck = Node(ProductionFactGrammar.ProductionFact);

        public static Rule RecExp = Recursive(() => Expression);
        public static Rule ParanExp = LeftParan + WS + RecExp + WS + RightParan;
        public static Rule SimpleExp = ParanExp | FactCheck;
        public static Rule Expression = Node(SimpleExp + ZeroOrMore(WS + BinaryOp + WS + SimpleExp));

        static ProductionExpressionGrammar()
        {
            InitGrammar(typeof(ProductionExpressionGrammar));
        }
    }
}
