using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Interpolation;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Нечёткое множество с гладкой функцией принадлежности, представляемой сплайном Акимы.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.FuzzySet" />
    public class PolynomialFuzzySet: FuzzySet
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PolynomialFuzzySet"/>.
        /// </summary>
        /// <param name="points">Массив точек.</param>
        /// <param name="xmin">Минимальное значение универсального множества.</param>
        /// <param name="xmax">Максимальное значение универсального множества.</param>
        public PolynomialFuzzySet(List<KeyValuePair<double, double>> points, double xmin = 0,
            double xmax = 35): base((x) => 1, xmin, xmax)
        {
            if (points.Count < 5)
                throw new ArgumentException("Список должен содержать не менее 5 значений.", "points");
            InitializeAkima(points);
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PolynomialFuzzySet"/>.
        /// </summary>
        /// <param name="points">Массив точек.</param>
        /// <param name="xmin">Минимальное значение универсального множества.</param>
        /// <param name="xmax">Максимальное значение универсального множества.</param>
        public PolynomialFuzzySet(double[,] points, double xmin = 0, double xmax = 0):
            base((x) => 1, xmin, xmax)
        {
            List<KeyValuePair<double, double>> pointsList = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < points.GetLength(0); i++)
                pointsList.Add(new KeyValuePair<double, double>(points[i, 0], points[i, 1]));
            if(!pointsList.Any((p) => p.Key == xmin))
                pointsList.Add(new KeyValuePair<double, double>(xmin, 0));
            if(!pointsList.Any((p) => p.Key == xmax))
                pointsList.Add(new KeyValuePair<double, double>(xmax, 0));
            pointsList = pointsList.OrderBy((p) => p.Key).ToList();
            if (pointsList.Count < 5) // недостаточно точек для построения сплайна
            {
                var linear = new LinearFuzzySet(points, xmin, xmax);
                if (pointsList.Count == 2 || pointsList.Count == 4)
                {
                    double x = (pointsList[0].Key + pointsList[1].Key)/2;
                    double xv = linear.GetConfidence(x);
                    pointsList.Insert(1, new KeyValuePair<double, double>(x, xv));
                }
                if (pointsList.Count == 3)
                {
                    double x = (pointsList[0].Key + pointsList[1].Key) / 2;
                    double xv = linear.GetConfidence(x);
                    double x1 = (pointsList[1].Key + pointsList[2].Key)/2;
                    double xv1 = linear.GetConfidence(x1);
                    pointsList.Insert(2, new KeyValuePair<double, double>(x1, xv1));
                    pointsList.Insert(1, new KeyValuePair<double, double>(x, xv));
                }
            }
            InitializeAkima(pointsList);
        }

        /// <summary>
        /// Строит функция принадлежности в виде сплайна Акимы.
        /// </summary>
        /// <param name="points">Список точек для построения сплайна.</param>
        protected void InitializeAkima(List<KeyValuePair<double, double>> points)
        {
            var ordered = points.OrderBy((p) => p.Key).ToList();
            var akima = CubicSpline.InterpolateAkima(ordered.Select((p) => p.Key),
                ordered.Select((p) => p.Value));
            function = (x) =>
            {
                double res = akima.Interpolate(x[0]);
                if (res < 0)
                    return 0;
                if (res > 1)
                    return 1;
                return res;
            };
        }
    }
}
