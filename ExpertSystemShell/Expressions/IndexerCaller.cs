using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ExpertSystemShell;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Класс для вызова методов индексаторов.
    /// </summary>
    public static class IndexerCaller
    {
        /// <summary>
        /// Получает значение индекстора у заданного объекта.
        /// </summary>
        /// <param name="target">Цель вызова.</param>
        /// <param name="args">Аргументы вызова.</param>
        /// <returns>Возвращает значение вызываемого метода-индексатора.</returns>
        public static dynamic GetIndexerValue(dynamic target, dynamic[] args)
        {
            Type type = target.GetType();
            if(target is Array)
            {
                MethodInfo method = type.GetMethod("GetValue", new Type[] { typeof(int[]) });
                return method.Invoke(target, new object[] { args.Convert<int>() });
            }

            IEnumerable<PropertyInfo> properties =
                type.GetProperties().Where<PropertyInfo>(
                (a) => {return a.GetIndexParameters().Length == args.Length; });
            return properties.First().GetValue(target, args);
        }
        /// <summary>
        /// Устаналивает значение индексатора у заданного объекта.
        /// </summary>
        /// <param name="target">Цель вызова.</param>
        /// <param name="args">Аргументы.</param>
        /// <param name="value">Устанавливаемое значение.</param>
        public static void SetIndexerValue(dynamic target, dynamic[] args, dynamic value)
        {
            Type type = target.GetType();
            if (target is Array)
            {
                Type t = ((Array)target).GetType().GetElementType();
                MethodInfo method = type.GetMethod("SetValue", new Type[] { typeof(int[]),  t});
                method.Invoke(target, new object[] { value, args.Convert<int>() });
            }

            IEnumerable<PropertyInfo> properties =
                type.GetProperties().Where<PropertyInfo>(
                (a) => { return a.GetIndexParameters().Length == args.Length; });
            PropertyInfo indexer = properties.First();
            indexer.SetValue(target, value, args);
        }
    }
}
