using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ParkingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingTask task = new ParkingTask();
            task.AddRule(0, 0, 6, 0.80);
            task.AddRule(0, 1, 6, 0.80);
            task.AddRule(0, 3, 0, 0.80);
            task.AddRule(0, 4, 0, 0.80);
            task.AddRule(0, 5, 4, 0.80);
            task.AddRule(0, 6, 4, 0.80);
            task.AddRule(1, 0, 6, 0.80);
            task.AddRule(1, 1, 3, 0.80);
            task.AddRule(1, 3, 0, 0.80);
            task.AddRule(1, 4, 4, 0.80);
            task.AddRule(1, 5, 6, 0.80);
            task.AddRule(1, 6, 6, 0.80);
            task.AddRule(2, 2, 1, 0.80);
            task.AddRule(2, 3, 3, 0.80);
            task.AddRule(2, 4, 5, 0.80);
            task.AddRule(3, 0, 0, 0.80);
            task.AddRule(3, 1, 0, 0.80);
            task.AddRule(3, 2, 2, 0.80);
            task.AddRule(3, 3, 6, 0.80);
            task.AddRule(3, 5, 4, 0.80);
            task.AddRule(4, 0, 2, 0.80);
            task.AddRule(4, 1, 2, 0.80);
            task.AddRule(4, 2, 5, 0.80);
            task.AddRule(4, 3, 5, 0.80);
            task.AddRule(4, 5, 0, 0.80);
            task.AddRule(4, 6, 0, 0.80);
            string input = Console.ReadLine();
            double teta = 0;
            while(input != "q")
            {
                string[] splitted = input.Split(new char[] { ' ' });
                double x = int.Parse(splitted[0]);
                double fi = int.Parse(splitted[1]);
                teta = task.GetAngle(x, fi, DefuzzificationMethod.MeanOfMaximus);
                Console.WriteLine(teta);
                input = Console.ReadLine();
            }
            
            Console.ReadKey();
        }
    }
}
