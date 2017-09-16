using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ParkingConsole
{
    public class ParkingTask
    {
        protected int xmin = -150;
        protected int xmax = 150;
        protected List<FuzzySet> xList;
        protected List<FuzzySet> fiList;
        protected List<FuzzySet> tetaList;
        protected double[,] conf;
        protected FuzzySet[,] rules;
        protected List<Tuple<FuzzySet, FuzzySet, SimpleRule>> buildedRules;

        public ParkingTask(double[,] trainingData)
        {
            InitializeLinear();
            Train(trainingData);
        }
        public ParkingTask(bool linear = true)
        {
            if(linear)
                InitializeLinear();
            else 
                InitializeNonLinear();
            conf = new double[xList.Count, fiList.Count];
            rules = new FuzzySet[xList.Count, fiList.Count];
            buildedRules = new List<Tuple<FuzzySet, FuzzySet, SimpleRule>>();
        }

        protected void InitializeNonLinear()
        {
            xList = new List<FuzzySet>();
            xList.Add(new PolynomialFuzzySet(new double[2, 2] { { -150, 1 }, { -45, 0 } }, -150, 150));
            xList.Add(new PolynomialFuzzySet(new double[3, 2] { { -90, 0 }, { -30, 1 }, { 0, 0 } }, -150, 150));
            xList.Add(new PolynomialFuzzySet(new double[3, 2] { { -30, 0 }, { 0, 1 }, { 30, 0 } }, -150, 150));
            xList.Add(new PolynomialFuzzySet(new double[3, 2] { { 0, 0 }, { 30, 1 }, { 90, 0 } }, -150, 150));
            xList.Add(new PolynomialFuzzySet(new double[2, 2] { { 45, 0 }, { 150, 1 } }, -150, 150));
            fiList = new List<FuzzySet>();
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { -135, 0 }, { -135, 1 }, { -90, 0 } }, -180, 180));
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { -135, 0 }, { -90, 1 }, { -30, 0 } }, -180, 180));
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { -90, 0 }, { -30, 1 }, { 0, 0 } }, -180, 180));
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { -15, 0 }, { 0, 1 }, { -15, 0 } }, -180, 180));
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { 0, 0 }, { 30, 1 }, { 90, 0 } }, -180, 180));
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { 30, 0 }, { 90, 1 }, { 135, 0 } }, -180, 180));
            fiList.Add(new PolynomialFuzzySet(new double[3, 2] { { 90, 0 }, { 135, 1 }, { 205, 0 } }, -180, 180));
            tetaList = new List<FuzzySet>();
            tetaList.Add(new PolynomialFuzzySet(new double[2, 2] { { -45, 1 }, { -15, 0 } }, -45, 45));
            tetaList.Add(new PolynomialFuzzySet(new double[3, 2] { { -45, 0 }, { -15, 1 }, { -7.25, 0 } }, -45, 45));
            tetaList.Add(new PolynomialFuzzySet(new double[3, 2] { { -15, 0 }, { -7.5, 1 }, { 15, 0 } }, -45, 45));
            tetaList.Add(new PolynomialFuzzySet(new double[3, 2] { { -7.5, 0 }, { 0, 1 }, { 7.5, 0 } }, -45, 45));
            tetaList.Add(new PolynomialFuzzySet(new double[3, 2] { { 0, 0 }, { 7.5, 1 }, { 15, 1 } }, -45, 45));
            tetaList.Add(new PolynomialFuzzySet(new double[3, 2] { { 7.5, 0 }, { 15, 1 }, { 45, 1 } }, -45, 45));
            tetaList.Add(new PolynomialFuzzySet(new double[2, 2] { { 15, 0 }, { 45, 1 } }, -45, 45));
        }
        protected void InitializeLinear()
        {
            xList = new List<FuzzySet>();
            xList.Add(new LinearFuzzySet(new double[2, 2] { { -150, 1 }, { -45, 0 } }, -150, 150));
            xList.Add(new LinearFuzzySet(new double[3, 2] { { -90, 0 }, { -30, 1 }, { 0, 0 } }, -150, 150));
            xList.Add(new LinearFuzzySet(new double[3, 2] { { -30, 0 }, { 0, 1 }, { 30, 0 } }, -150, 150));
            xList.Add(new LinearFuzzySet(new double[3, 2] { { 0, 0 }, { 30, 1 }, { 90, 0 } }, -150, 150));
            xList.Add(new LinearFuzzySet(new double[2, 2] { { 45, 0 }, { 150, 1 } }, -150, 150));
            fiList = new List<FuzzySet>();
            fiList.Add(new LinearFuzzySet(new double[3, 2] {{-135, 0}, { -135, 1 }, { -90, 0 } }, -180, 180));
            fiList.Add(new LinearFuzzySet(new double[3, 2] { { -135, 0 }, { -90, 1 }, { -30, 0 } }, -180, 180));
            fiList.Add(new LinearFuzzySet(new double[3, 2] { { -90, 0 }, { -30, 1 }, { 0, 0 } }, -180, 180));
            fiList.Add(new LinearFuzzySet(new double[3, 2] { { -15, 0 }, { 0, 1 }, { -15, 0 } }, -180, 180));
            fiList.Add(new LinearFuzzySet(new double[3, 2] { { 0, 0 }, { 30, 1 }, { 90, 0 } }, -180, 180));
            fiList.Add(new LinearFuzzySet(new double[3, 2] { { 30, 0 }, { 90, 1 }, { 135, 0 } }, -180, 180));
            fiList.Add(new LinearFuzzySet(new double[3, 2] { { 90, 0 }, { 135, 1 }, { 205, 0 } }, -180, 180));
            tetaList = new List<FuzzySet>();
            tetaList.Add(new LinearFuzzySet(new double[2, 2] { { -45, 1 }, { -15, 0 } }, -45, 45));
            tetaList.Add(new LinearFuzzySet(new double[3, 2] { { -45, 0 }, { -15, 1 }, { -7.25, 0 } }, -45, 45));
            tetaList.Add(new LinearFuzzySet(new double[3, 2] { { -15, 0 }, { -7.5, 1 }, { 15, 0 } }, -45, 45));
            tetaList.Add(new LinearFuzzySet(new double[3, 2] { { -7.5, 0 }, { 0, 1 }, { 7.5, 0 } }, -45, 45));
            tetaList.Add(new LinearFuzzySet(new double[3, 2] { { 0, 0 }, { 7.5, 1 }, { 15, 1 } }, -45, 45));
            tetaList.Add(new LinearFuzzySet(new double[3, 2] { { 7.5, 0 }, { 15, 1 }, { 45, 1 } }, -45, 45));
            tetaList.Add(new LinearFuzzySet(new double[2, 2] { { 15, 0 }, { 45, 1 } }, -45, 45));
        }
        protected void Train(double[,] trainingData)
        {
            conf = new double[xList.Count, fiList.Count];
            rules = new FuzzySet[xList.Count, fiList.Count];
            for (int i = 0; i < trainingData.GetLength(0); i++) //для каждой тройки тренировочных данных
            {
                double currX = trainingData[i, 0];
                double currFi = trainingData[i, 1];
                double currTeta = trainingData[i, 2];
                for(int a1 = 0; a1 < xList.Count; a1++)
                {
                    for(int a2 = 0; a2 < fiList.Count; a2++) //для всех пар нечётких множеств
                    {
                        //найдём такое множество из teta, для которого значение функции принадлежности максимально
                        double maxConfidence = 0;
                        FuzzySet maxConfSet = null;
                        foreach(FuzzySet set in tetaList) //для каждого выходного нечёткого множества
                        {
                            double currConf = set.GetConfidence(currTeta);
                            if(currConf > maxConfidence)
                            {
                                maxConfidence = currConf;
                                maxConfSet = set;
                            }
                        }
                        //степень истинности правила
                        maxConfidence *= xList[a1].GetConfidence(currX) * fiList[a2].GetConfidence(currFi);
                        if(conf[a1, a2] < maxConfidence) //если степень истинности больше текущей
                        {
                            conf[a1, a2] = maxConfidence; //обновим степень принадлежности
                            rules[a1, a2] = maxConfSet; //обновим правило
                        }
                    }
                }
            }
        }
        public double GetAngle(double x, double fi, DefuzzificationMethod method = 
            DefuzzificationMethod.GravityCentre)
        {
            //дефаззификация по методу среднего центра            
            double num = 0;
            double div = 0;
            foreach(var rule in buildedRules)
            {
                double actLevel = rule.Item1.GetConfidence(x)*rule.Item2.GetConfidence(fi);
                if (actLevel > 0)
                {
                    double deff = rule.Item3.Defuzzificate(x, fi, method);
                    num += actLevel*deff;
                    div += actLevel;
                }
            }
            return num / div;
        }
        public void AddRule(int x, int fi, int teta,  double sp)
        {
            buildedRules.Add(new Tuple<FuzzySet, FuzzySet, SimpleRule>(xList[x], fiList[fi],
                new SimpleRule(xList[x], fiList[fi], tetaList[teta])));
        }
    }
}
