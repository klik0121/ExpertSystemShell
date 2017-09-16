using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class FuzzyVariable: ProductionFact, ICloneable
    {
        protected Dictionary<string, FuzzySet> terms;
        protected int xmin;
        protected int xmax;
        protected double numericValue;
        protected static Regex regex = new Regex("(не|очень)");

        public Dictionary<string, FuzzySet> Functions
        {
            get { return terms; }
        }
        public double NumericValue
        {
            get { return numericValue; }
            set { this.numericValue = value; }
        }
        public int Min
        {
            get { return xmin; }
        }
        public int Max
        {
            get { return xmax; }
        }
        public IList<string> Labels
        {
            get { return terms.Keys.ToList(); }
        }

        public FuzzyVariable(string name, string value, int xmin = 0,
            int xmax = 100): base(name, value)
        {
            this.terms = new Dictionary<string, FuzzySet>();
            this.xmax = xmax;
            this.xmin = xmin;
            this.numericValue = xmin;
        }

        protected double GetConfidence(IEnumerable<string> value)
        {
            if(value.Count() == 1)
            {
                FuzzySet set;
                string v = value.First();
                if (terms.TryGetValue(v, out set))
                    return set.GetConfidence(numericValue);
            }
            else
            {
                string v = value.First();
                switch(v)
                {
                    case("не"): return 1 - GetConfidence(value.Skip(1));
                    case("очень"): return Math.Pow(GetConfidence(value.Skip(1)), 2);
                    default: return 0;
                }
            }
            return 0;
        }
        public void AddSet(string name, Func<double, double> function)
        {
            if(!this.terms.ContainsKey(name))
                terms.Add(name, new FuzzySet(function, xmin, xmax));
            else
                terms[name] = new FuzzySet(function, xmin, xmax);
        }
        public override double GetConfidence()
        {
            IEnumerable<string> key = regex.Split(this.value.ToLower());
            return GetConfidence(key);
        }
        public double Complement()
        {
            return 1 - this.GetConfidence();
        }
        public override string ToString()
        {
            return base.ToString() + string.Format(" ({0}%)", Math.Round(this.GetConfidence(), 2) * 100);
        }

        #region ICloneable Members

        public object Clone()
        {
            FuzzyVariable fact = new FuzzyVariable(name, value, xmin, xmax);
            fact.terms = this.terms;
            return fact;
        }

        #endregion
    }
}