﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Solvers
{
    /// <summary>
    /// Интерфейс для подсистемы решения.
    /// </summary>
    public interface ILogicalSolver
    {
        ILogicalResult GetResult(ILogicalQuery query);
        IEnumerable<ILogicalResult> GetConclusion(ILogicalQuery query);
    }
}
