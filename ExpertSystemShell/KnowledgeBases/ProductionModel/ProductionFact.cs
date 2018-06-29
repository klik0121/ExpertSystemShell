using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class ProductionFact: IData
    {
        protected string name;
        protected string value;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public ProductionFact(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public virtual double GetConfidence()
        {
            return 1;
        }

        public static bool operator ==(string value, ProductionFact fact)
        {
            return fact.value == value;
        }

        public static bool operator ==(ProductionFact fact, string value)
        {
            return value == fact.value;
        }
        public static bool operator !=(ProductionFact fact, string value)
        {
            return !(value == fact.value);
        }

        public static bool operator !=(string value, ProductionFact fact)
        {
            return fact.value != value;
        }
        public double Intersection(ProductionFact fact, OperationType type = OperationType.MinMax)
        {
            double x = this.GetConfidence();
            double y = fact.GetConfidence();
            int p = 2;
            switch (type)
            {
                case (OperationType.MinMax):
                    return Math.Min(x, y);
                case (OperationType.Algebraic):
                    return x * y;
                case (OperationType.Conditional):
                    if (y == 1) return x;
                    else if (x == 1) return y;
                    else return 0;
                case (OperationType.MinMaxAlternative):
                    return Math.Max(0, x + y - 1);
                case (OperationType.Exponential):
                    return 1 - Math.Min(1, Math.Pow(Math.Pow(1 - x, p) + Math.Pow(1 - y, p), 1.0 / p));
                default:
                    return 0;
            }
        }
        public double Union(ProductionFact fact, OperationType type = OperationType.MinMax)
        {
            double x = this.GetConfidence();
            double y = fact.GetConfidence();
            int p = 2;
            switch (type)
            {
                case (OperationType.MinMax):
                    return Math.Max(x, y);
                case (OperationType.Algebraic):
                    return x + y - x * y;
                case (OperationType.Conditional):
                    if (y == 0) return x;
                    else if (x == 0) return y;
                    else return 1;
                case (OperationType.MinMaxAlternative):
                    return Math.Min(1, x + y);
                case (OperationType.Exponential):
                    return Math.Min(1, Math.Pow(Math.Pow(x, p) + Math.Pow(y, p), 1.0 / p));
                default:
                    return 0;
            }
        }
        public static double operator !(ProductionFact fact)
        {
            return 1 - fact.GetConfidence();
        }
        public static double operator |(ProductionFact fact1, ProductionFact fact2)
        {
            return Math.Max(fact1.GetConfidence(), fact2.GetConfidence());
        }
        public static double operator &(ProductionFact fact1, ProductionFact fact2)
        {
            return Math.Min(fact1.GetConfidence(), fact2.GetConfidence());
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("'{0} - {1}'", name, value ?? "неизвестно");
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            ProductionFact fact = obj as ProductionFact;
            if(fact != null)
            {
                return fact.name == this.name && fact.value == this.value;
            }
            return false;
        }
    }
}
