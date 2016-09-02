using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers;

namespace ExpertSystemShell.Parsers
{
    public interface IParser
    {
        IEnumerable<ILogicalQueryParameter> ParseQuery(string query);
        ILogicalStatement ParseRule(string rule);
        IEnumerable<ILogicalStatement> ParseRules(string rules);
    }
}
