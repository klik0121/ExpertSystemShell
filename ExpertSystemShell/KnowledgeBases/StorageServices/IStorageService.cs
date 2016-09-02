using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.StorageServices
{
    /// <summary>
    /// Интерфейс для хранилищ базы знаний. Изначально предполагается хранить базу в памяти с
    /// возможностью сохранения на диск. Позже можно реализовать отражение в базу данных с
    /// использованием Entity Framework.
    /// </summary>
    public interface IStorageService: IEnumerable<ILogicalStatement>
    {
        /// <summary>
        /// Добавляет заданное логическое утвреждение к существующей базе знаний.
        /// </summary>
        /// <param name="statement">The statement.</param>
        void AddStatement(ILogicalStatement statement);
        /// <summary>
        /// Удаляет заданной логическое утверждение из существующе базы.
        /// </summary>
        /// <param name="statement">The statement.</param>
        void RemoveStatement(ILogicalStatement statement);
    }
}
