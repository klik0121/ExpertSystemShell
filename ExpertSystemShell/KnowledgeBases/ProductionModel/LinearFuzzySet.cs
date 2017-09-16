using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Нечёткое множество с линейной функцией принадлежности.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.FuzzySet" />
    public class LinearFuzzySet: FuzzySet
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LinearFuzzySet"/>.
        /// </summary>
        /// <param name="points">Массив точек.</param>
        /// <param name="xmin">Минимальное значение универсального множества.</param>
        /// <param name="xmax">Максимальное значение универсального множества.</param>
        public LinearFuzzySet(List<KeyValuePair<double, double>> points,
            double xmin = 0, double xmax = 35): base((x) => 1, xmin, xmax)
        {
            if (!points.Any((a) => { return a.Key == xmin; }))
                points.Add(new KeyValuePair<double, double>(xmin, 0));
            if (!points.Any((a) => { return a.Key == xmax; }))
                points.Add(new KeyValuePair<double, double>(xmax, 0));
            InitializeFunction(points);
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LinearFuzzySet"/>.
        /// </summary>
        /// <param name="points">Массив точек.</param>
        /// <param name="xmin">Минимальное значение универсального множества.</param>
        /// <param name="xmax">Максимальное значение универсального множества.</param>
        public LinearFuzzySet(double[,]points, double xmin = 0, double xmax = 35): base((x) => 1, xmin, xmax)
        {
            List<KeyValuePair<double, double>> p = new List<KeyValuePair<double, double>>();
            for(int i = 0; i < points.GetLength(0); i++)
                p.Add(new KeyValuePair<double, double>(points[i, 0], points[i, 1]));
            if (!p.Any((a) => { return a.Key == xmin; }))
                p.Add(new KeyValuePair<double, double>(xmin, 0));
            if (!p.Any((a) => { return a.Key == xmax; }))
                p.Add(new KeyValuePair<double, double>(xmax, 0));
            InitializeFunction(p);
        }

        private void InitializeFunction(List<KeyValuePair<double, double>> points)
        {
            var ordered = points.OrderBy((p) => p.Key).ToList();
            function = (x) =>
            {
                if (x[0] < ordered[0].Key)
                    return ordered[0].Value;
                for (int i = 0; i < (ordered.Count - 1); i++)
                {
                    if (x[0] >= ordered[i].Key && x[0] <= ordered[i + 1].Key)
                    {
                        double x1 = ordered[i].Key;
                        double x2 = ordered[i + 1].Key;
                        double y1 = ordered[i].Value;
                        double y2 = ordered[i + 1].Value;
                        return y2 + (x[0] - x2) * (y2 - y1) / (x2 - x1);
                    }
                }
                return ordered.Last().Value;
            };
        }
    }
}
