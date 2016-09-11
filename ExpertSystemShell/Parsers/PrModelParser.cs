using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Builders;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using Diggins.Jigsaw;
using ExpertSystemShell.Expressions;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers;

namespace ExpertSystemShell.Parsers
{
    /// <summary>
    /// Парсер для грамматики продукционных правил.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Parsers.IParser" />
    public class PrModelParser: IParser
    {
        protected Dictionary<string, IBuilder<ILogicalStatement>> stBuilders;
        protected Dictionary<string, IBuilder<ILogicalQuery>> qBuilders;
        protected ExpressionHelper eh;

        /// <summary>
        /// Инициализирует новый экземпляр  <see cref="PrModelParser"/>.
        /// </summary>
        /// <param name="eh">Построитель выражений.</param>
        public PrModelParser(ExpressionHelper eh)
        {
            this.eh = eh;
            stBuilders = new Dictionary<string, IBuilder<ILogicalStatement>>();
            stBuilders.Add(ProductionRuleGrammar.ProductionRule.Name,
                new ProductionRuleBuilder(eh));
            qBuilders = new Dictionary<string, IBuilder<ILogicalQuery>>();
            qBuilders.Add(ProductionQueryGrammar.Query.Name,
                new ProductionQueryBuilder());
        }

        #region IParser Members

        /// <summary>
        /// Разбирает запрос пользователя и формирует множество логических параметров запроса.
        /// </summary>
        /// <param name="query">Строка-запрос.</param>
        /// <returns>
        /// Возвращает перечисление параметров запроса.
        /// </returns>
        public Solvers.ILogicalQuery ParseQuery(string query)
        {
            try
            {
                Node q = ProductionQueryGrammar.Query.Parse(query)[0];
                return qBuilders[q.Label].Build(q);
            }
            catch
            {
                throw new ParsingException("Заданная строка не является запросом.");
            }

        }
        /// <summary>
        /// Разбирает логическое правило.
        /// </summary>
        /// <param name="rule">Строка-правило.</param>
        /// <returns>
        /// Возвращает созданный экземпляр правила.
        /// </returns>
        /// <exception cref="ParsingException">Заданная строка не является правилом.</exception>
        public KnowledgeBases.ILogicalStatement ParseRule(string rule)
        {
            try
            {
                Node n = ProductionRuleGrammar.ProductionRule.Parse(rule)[0];
                return stBuilders[n.Label].Build(n);
            }
            catch
            {
                throw new ParsingException("Заданная строка не является правилом.");
            }
        }
        /// <summary>
        /// Разбирает список правил.
        /// </summary>
        /// <param name="rules">Строка, содержащая список правил.</param>
        /// <returns>Возвращает перечисление, содержащее разобранные правила.</returns>
        public IEnumerable<KnowledgeBases.ILogicalStatement> ParseRules(string rules)
        {
            List<ILogicalStatement> statements = new List<ILogicalStatement>();
            try
            {
                Node n = ProductionRuleGrammar.ProductionRuleList.Parse(rules)[0];
                foreach(var child in n.Nodes)
                {
                    statements.Add(stBuilders[child.Label].Build(child));
                }
            }
            catch
            {
                throw new ParsingException("Заданная строка содержит некорректный список правил.");
            }
            return statements;
        }

        #endregion
    }
}
