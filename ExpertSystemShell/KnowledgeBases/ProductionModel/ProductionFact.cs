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
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public ProductionFact(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
