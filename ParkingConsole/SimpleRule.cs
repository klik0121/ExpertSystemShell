using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ParkingConsole
{
    public class SimpleRule
    {
        protected NFuzzySet premise; 
        protected FuzzySet teta;

        public SimpleRule(FuzzySet x, FuzzySet fi, FuzzySet teta)
        {
            this.teta = teta;
            premise = x.CartesianProduct(fi, OperationType.MinMax);
        }

        public double Defuzzificate(double xv, double fiv, DefuzzificationMethod method = 
            DefuzzificationMethod.GravityCentre)
        {
            double[] args = {xv, fiv};
            double min = premise.GetConfidence(args);
            FuzzySet result = new FuzzySet((x) => Math.Min(teta.GetConfidence(x), min),
                teta.XMin[0], teta.XMax[0]);
            return result.Defuzzification(method);
        }
    }
}
