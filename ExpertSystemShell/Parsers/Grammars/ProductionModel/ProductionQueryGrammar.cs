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
    /// <summary>
    /// Грамматика для запросов к продукционной базе данных.
    /// <para />Синтаксис: если #список фактов# то #запросы фактов#.
    /// <para />#список фактов# задаётся следующим образом: один или несколько фактов через запятую.
    /// <para />#факт# задаётся следующим образом: 'имя факта - значение факта'.
    /// <para />#запросы фактов# задаются следющим образом: один или несколько элементарных запросов фактов.
    /// <para />#элементарный запрос факта# задаётся следующим образом: имя факта =?.
    /// </summary>
    /// <seealso cref="Diggins.Jigsaw.SharedGrammar" />
    public class ProductionQueryGrammar: SharedGrammar
    {
        public static Rule IfKeyword = MatchRegex(new Regex("если", RegexOptions.IgnoreCase));
        public static Rule If = (WS + IfKeyword + WS + ProductionActionGrammar.ProductionActionList);
        public static Rule ThenKeyword = MatchRegex(new Regex("то", RegexOptions.IgnoreCase));
        public static Rule FactName = Node(Pattern(@"(\w)(\s*\w)*(?=\s*=\?)"));
        public static Rule GetOp = MatchString("=?");
        public static Rule AtomicFactQuery = FactName + GetOp + ZeroOrMore(WS + FactName + WS + GetOp);
        public static Rule Then = (ThenKeyword + WS + AtomicFactQuery + WS);
        public static Rule Query = Node(If + WS + Then);

        static ProductionQueryGrammar()
        {
            InitGrammar(typeof(ProductionQueryGrammar));
        }
        
    }
}
