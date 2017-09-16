using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyParallel;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Одномерное нечёткое множество.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.KnowledgeBases.ProductionModel.NFuzzySet" />
    public class FuzzySet: NFuzzySet
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FuzzySet"/>.
        /// </summary>
        /// <param name="function">Функция принадлежности.</param>
        /// <param name="xmin">Минимум универсального множества.</param>
        /// <param name="xmax">Максимум универсального множества.</param>
        public FuzzySet(Func<double, double> function, double xmin = 0, double xmax = 35)
            : base((x) => { return function(x[0]); }, new double[] {xmin}, new double[] {xmax})
        {

        }
        /// <summary>
        /// Возвращает значение функции принадлежности.
        /// </summary>
        /// <param name="x">Аргумент функции.</param>
        /// <returns>Возвращает значение функции принадлежности.</returns>
        public double GetConfidence(double x)
        {
            return base.GetConfidence(new double[] { x });
        }
        public static implicit operator Func<double, double>(FuzzySet set)
        {
            return set.GetConfidence;
        }
        public double Defuzzification(DefuzzificationMethod type = DefuzzificationMethod.GravityCentre)
        {
            switch (type)
            {
                case (DefuzzificationMethod.GravityCentre):
                {
                    Func<double, double> func = this.GetConfidence;
                    Func<double, double> func1 = (x) => x * func(x);
                    double fIntegral = func.PIntegral1(xmin[0], xmax[0], 1E-1);
                    if (fIntegral == 0) return 0;
                    return func1.PIntegral1(xmin[0], xmax[0], 1E-1) / fIntegral;
                }
                case(DefuzzificationMethod.MeanOfMaximus):
                {
                    double eps = (xmax[0] - xmin[0]) / 5000;
                    double max = Max(eps);
                    double result = 0;
                    int count = 0;
                    for (double x = xmin[0]; x <= xmax[0]; x += eps)
                    {
                        double v = GetConfidence(x);
                        if (v == max)
                        {
                            result += x;
                            count++;
                        }
                    }
                    return result/count;
                }
                case(DefuzzificationMethod.Median):
                {
                    Func<double, double> f = GetConfidence;
                    Func<double, double, double> integral = (a, b) => f.PIntegral1(a, b, 1E-1);
                    return Solve((x) => integral(xmin[0], x) - integral(x, xmax[0]));
                }
            }
            return 0;
        }
        private double Max(double step)
        {
            double max = 0;
            for (double x = xmin[0]; x <= xmax[0]; x += step)
            {
                double v = GetConfidence(x);
                if (v > max)
                    max = v;
            }
            return max;
        }
        private double Solve(Func<double, double> f, double eps = 1E-1)
        {
            double a = xmin[0];
            double b = xmax[0];
            double fa = f(a);
            double fb = f(b);
            double fx = double.MaxValue;
            double x = 0;
            while (Math.Abs(fx) >= eps && Math.Abs(b - a) >= eps)
            {
                x = (a + b)/2;
                fx = f(x);
                if (fx < 0)
                {
                    a = x;
                    fa = fx;
                }
                else
                {
                    b = x;
                    fb = fx;
                }
            }
            return x;
        }
    }
}
