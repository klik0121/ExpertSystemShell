using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Функция принадлежности n-арного нечёткого множества.
    /// </summary>
    /// <param name="arg">Значения нечётких переменных.</param>
    /// <returns>Возвращает значение функции принадлежности.</returns>
    public delegate double NFunction(double[] arg);

    /// <summary>
    /// n-арное нечёткое множество. n задаётся посредством задания универсального множества.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public class NFuzzySet: ICloneable
    {
        protected double[] xmin;
        protected double[] xmax;
        protected NFunction function;

        /// <summary>
        /// Возвращает количество измерений заданной функции.
        /// </summary>
        public int Dimensions
        {
            get { return xmin.Length; }
        }
        /// <summary>
        /// Получает массив, содержащий минимумы универсальных множеств для каждой переменной.
        /// </summary>
        public double[] XMin
        {
            get { return xmin; }
        }
        /// <summary>
        /// Получает массив, содержащий максимумы универсальных множеств для каждой переменной.
        /// </summary>
        public double[] XMax
        {
            get { return xmax; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NFuzzySet"/>.
        /// </summary>
        /// <param name="function">Функция принадлежности.</param>
        /// <param name="xmin">Массив, содержащий минимумы универсальных множеств для каждой переменной. Размерности 
        /// <see cref="xmin"/> и <see cref="xmax"/> должны совпадать.</param>
        /// <param name="xmax">>Массив, содержащий максимумы универсальных множеств для каждой переменной. Размерности 
        /// <see cref="xmin"/> и <see cref="xmax"/> должны совпадать.</param>
        public NFuzzySet(NFunction function, double[] xmin, double[] xmax)
        {
            if (xmin.Length == 0)
                throw new ArgumentException("Размерность массива должна быть больше 0.", "xmin");
            if(xmax.Length  == 0)
                throw new ArgumentException("Размерность массива должна быть больше 0.", "xmax");
            if (xmin.Length != xmax.Length)
                throw new ArgumentException("Размерности массивов должны совпадать.");
            this.function = function;
            this.xmax = xmax;
            this.xmin = xmin;
        }
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NFuzzySet"/> значениями по умолчанию.
        /// Значения по умолчанию: фукнция принадлежности всегда возвращает 0, количество измерений = 0, 
        /// минимум для первой переменной = 0, максимум для второй переменной = 35.
        /// </summary>
        public NFuzzySet(): this((double[] x) => 0, new double[]{ 0 }, new double[]{ 35 })
        {

        }

        /// <summary>
        /// Возвращает значение функции принадлежности.
        /// </summary>
        /// <param name="x">Массив входных значений нечётких переменных.</param>
        /// <returns>Возвращает значение функции принадлежности.</returns>
        public virtual double GetConfidence(double[] x)
        {
            double[] xv = new double[x.Length];
            for(int i = 0; i < x.Length; i++)
            {
                if (x[i] < xmin[i])
                    xv[i] = xmin[i];
                else if (x[i] > xmax[i])
                    xv[i] = xmax[i];
                else xv[i] = x[i];
            }
            return function(xv);
        }
        /// <summary>
        /// Возвращает пересечение нечётких множеств.
        /// </summary>
        /// <param name="set">Второе множество.</param>
        /// <param name="type">Тип операции пересечения.</param>
        /// <returns>Возвращает пересечение нечётких множеств.</returns>
        public NFuzzySet Intersection(NFuzzySet set, OperationType type = OperationType.MinMax)
        {
            double[] newXMin = Minimum(set);
            double[] newXMax = Maximum(set);
            NFunction func;
            switch (type)
            {
                case (OperationType.MinMax):
                    func = (x) => Math.Min(this.GetConfidence(x), set.GetConfidence(x));
                    break;
                case (OperationType.Algebraic):
                    func = (x) => this.GetConfidence(x) * set.GetConfidence(x);
                    break;
                case (OperationType.Conditional):
                    func = (x) =>
                    {
                        double y1 = this.GetConfidence(x);
                        double y2 = set.GetConfidence(x);
                        if (y2 == 1) return y1;
                        else if (y1 == 1) return y2;
                        else return 0;
                    };
                    break;
                case (OperationType.MinMaxAlternative):
                    func = (x) => Math.Max(0, this.GetConfidence(x) + set.GetConfidence(x) - 1);
                    break;
                case (OperationType.Exponential):
                    func = (x) => 1 - Math.Min(1, Math.Pow(Math.Pow(1 - this.GetConfidence(x), 2) +
                        Math.Pow(1 - set.GetConfidence(x), 2), 1.0 / 2));
                    break;
                default:
                    func = (x) => 0;
                    break;
            }
            return new NFuzzySet(func, newXMin, newXMax);
        }
        /// <summary>
        /// Возвращает объединение нечётких множеств.
        /// </summary>
        /// <param name="set">Второе множество.</param>
        /// <param name="type">Тип операции объединения.</param>
        /// <returns>Возвращает объединение нечётких множеств.</returns>
        public NFuzzySet Union(NFuzzySet set, OperationType type = OperationType.MinMax)
        {
            double[] newXMin = Minimum(set);
            double[] newXMax = Maximum(set);
            NFunction func;
            switch (type)
            {
                case (OperationType.MinMax):
                    func = (x) => Math.Max(this.GetConfidence(x), set.GetConfidence(x));
                    break;
                case (OperationType.Algebraic):
                    func = (x) => this.GetConfidence(x) + set.GetConfidence(x) -
                        this.GetConfidence(x) * set.GetConfidence(x);
                    break;
                case (OperationType.Conditional):
                    func = (x) =>
                    {
                        double y1 = this.GetConfidence(x);
                        double y2 = set.GetConfidence(x);
                        if (y2 == 0) return y1;
                        else if (y1 == 0) return y2;
                        else return 1;
                    };
                    break;
                case (OperationType.MinMaxAlternative):
                    func = (x) => Math.Min(1, this.GetConfidence(x) + set.GetConfidence(x));
                    break;
                case (OperationType.Exponential):
                    func = (x) => Math.Min(1, Math.Pow(Math.Pow(this.GetConfidence(x), 2) +
                        Math.Pow(set.GetConfidence(x), 2), 1.0 / 2));
                    break;
                default:
                    func = (x) => 0;
                    break;
            }
            return new NFuzzySet(func, newXMin, newXMax);
        }
        /// <summary>
        /// Возвращает дополнение текущего множества.
        /// </summary>
        /// <returns>Возвращает дополнение текущего множества.</returns>
        public NFuzzySet Complement()
        {
            return new NFuzzySet((x) => 1 - this.GetConfidence(x), (double[])xmin.Clone(), (double[])xmax.Clone());
        }
        /// <summary>
        /// Возвращает концентрацию текущего множества.
        /// </summary>
        /// <returns>Возвращает концентрацию текущего множества.</returns>
        public NFuzzySet Concentration()
        {
            return new NFuzzySet((x) => Math.Pow(this.GetConfidence(x), 2), (double[])xmin.Clone(), (double[])xmax.Clone());
        }
        /// <summary>
        /// Возвращает разбавление текущего множества.
        /// </summary>
        /// <returns>Возвращает разбавление текущего множества.</returns>
        public NFuzzySet Dilution()
        {
            return new NFuzzySet((x) => Math.Pow(this.GetConfidence(x), 0.5), (double[])xmin.Clone(), (double[])xmax.Clone());
        }
        /// <summary>
        /// Возвращает декартово произведение заданных множеств.
        /// </summary>
        /// <param name="set">Второе множество.</param>
        /// <param name="type">Тип операции.</param>
        /// <returns>Возвращает декартово произведение заданных множеств.</returns>
        public NFuzzySet CartesianProduct(NFuzzySet set, OperationType type = OperationType.MinMax)
        {
            return new FuzzyCartesianProduct(this, set, type);
        }

        protected double[] Minimum(NFuzzySet set)
        {
            double[] min = new double[xmin.Length];
            for(int i = 0; i < xmin.Length; i++)
            {
                min[i] = Math.Min(xmin[i], set.xmin[i]);
            }
            return min;
        }
        protected double[] Maximum(NFuzzySet set)
        {
            double[] max = new double[xmin.Length];
            for (int i = 0; i < xmax.Length; i++)
            {
                max[i] = Math.Min(xmax[i], set.xmax[i]);
            }
            return max;
        }

        public static implicit operator NFunction(NFuzzySet set)
        {
            return set.GetConfidence;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new NFuzzySet((NFunction)function.Clone(), (double[])xmin.Clone(), (double[])xmax.Clone());
        }

        #endregion
    }
}
