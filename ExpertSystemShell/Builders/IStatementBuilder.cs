using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Интерфейс для "построителей знаний". Парсер создаёт AST-дерево. Построитель создаёт 
    /// логическое правило из нода такого дерева.
    /// </summary>
    protected interface IStatementBuilder
    {
        ILogicalStatement Build(Node node);
    }
}
