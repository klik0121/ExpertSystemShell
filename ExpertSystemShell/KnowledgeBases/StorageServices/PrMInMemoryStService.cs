using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.KnowledgeBases.StorageServices
{
    public class PrMInMemoryStService: IStorageService
    {
        protected List<ILogicalStatement> statements;

        public PrMInMemoryStService()
        {
            this.statements = new List<ILogicalStatement>();
        }

        #region IStorageService Members

        public void AddStatement(ILogicalStatement statement)
        {
            if(statements.Contains(statement))
            {
                throw new ArgumentException("Правило уже существует.");
            }
            statements.Add(statement);
        }
        public void RemoveStatement(ILogicalStatement statement)
        {
            statements.Remove(statement);
        }

        #endregion

        #region IEnumerable<ILogicalStatement> Members

        public IEnumerator<ILogicalStatement> GetEnumerator()
        {
            foreach (var statement in statements)
                yield return statement;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
