using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Декартово произведение нечётких множеств.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.NFuzzySet" />
    public class FuzzyCartesianProduct: NFuzzySet
    {
        protected NFuzzySet left;
        protected NFuzzySet right;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FuzzyCartesianProduct"/>.
        /// </summary>
        /// <param name="left">Первое множество.</param>
        /// <param name="right">Второе множество.</param>
        /// <param name="type">Тип операции.</param>
        public FuzzyCartesianProduct(NFuzzySet left, NFuzzySet right, OperationType type = OperationType.MinMax)
        {
            this.left = left;
            this.right = right;
            this.xmax = UnionMaximum();
            this.xmin = UnionMinimum();
            switch(type)
            {
                case(OperationType.MinMax):
                    function = (x) =>
                    {
                        var args = SplitArgs(x);
                        return Math.Min(left.GetConfidence(args.Item1), right.GetConfidence(args.Item2));
                    };
                    break;
                case(OperationType.MinMaxAlternative):
                    function = (x) =>
                    {
                        var args = SplitArgs(x);
                        return Math.Max(left.GetConfidence(args.Item1), right.GetConfidence(args.Item2));
                    };
                    break;
                default:
                    function = (x) =>
                    {
                        var args = SplitArgs(x);
                        return Math.Max(left.GetConfidence(args.Item1), right.GetConfidence(args.Item2));
                    };
                    break;
            }
        }

        /// <summary>
        /// Разделяет едиый массив аргументов на два, в соответствии с размерами операндов декартового произведения.
        /// </summary>
        /// <param name="x">Массив значений входных переменных.</param>
        /// <returns><Возвращает <see cref="System.Tuple<double[], double[]>"/>, содержащий два массива аргументов для 
        /// операндов декартова произведения.</returns>
        protected Tuple<double[], double[]> SplitArgs(double[] x)
        {
            double[] first = x.Take(left.Dimensions).ToArray();
            double[] second = x.Skip(left.Dimensions).ToArray();
            return new Tuple<double[], double[]>(first, second);
        }
        /// <summary>
        /// Возвращает объединённый массив минимумов декартова произведения.
        /// </summary>
        /// <returns>Возвращает объединённый массив минимумов декартова произведения.</returns>
        protected double[] UnionMinimum()
        {
            double[] union = new double[left.XMin.Length + right.XMin.Length];
            for (int i = 0; i < left.XMin.Length; i++)
            {
                union[i] = left.XMin[i];
            }
            for (int i = 0; i < right.XMin.Length; i++)
            {
                union[i + xmin.Length] = right.XMin[i];
            }
            return union;
        }
        /// <summary>
        /// Возвращает объединённый массив максимумов декартова произведения.
        /// </summary>
        /// <returns>Возвращает объединённый массив максимумов декартова произведения.</returns>
        protected double[] UnionMaximum()
        {
            double[] union = new double[left.XMax.Length + right.XMax.Length];
            for (int i = 0; i < left.XMax.Length; i++)
            {
                union[i] = left.XMax[i];
            }
            for (int i = 0; i < right.XMin.Length; i++)
            {
                union[i + xmin.Length] = right.XMax[i];
            }
            return union;
        }
    }
}
