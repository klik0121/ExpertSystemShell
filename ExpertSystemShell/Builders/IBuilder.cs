using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Интерфейс построителя объектов по AST-дереву.
    /// </summary>
    /// <typeparam name="T">Тип объекта для построения.</typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Возвращает новый экземпляр <see cref="T"/>, построенный по заданному AST-дереву.
        /// </summary>
        /// <param name="node">Корень AST-дерева.</param>
        /// <returns>Возвращает новый экземпляр <see cref="T"/>, построенный по заданному AST-дереву.</returns>
        T Build(Node node);
    }
}
