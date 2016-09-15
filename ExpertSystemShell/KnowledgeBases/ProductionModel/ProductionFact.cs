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

        public static bool operator ==(ProductionFact fact, string value)
        {
            return fact.value == value;
        }
        public static bool operator !=(ProductionFact fact, string value)
        {
            return fact.value != value;
        }
        public static bool operator ==(string value, ProductionFact fact)
        {
            return fact.value == value;
        }
        public static bool operator !=(string value, ProductionFact fact)
        {
            return fact.value != value;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("'{0} - {1}'", name, value != null ? value : "неизвестно");
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return (obj is ProductionFact && this.GetHashCode() == obj.GetHashCode());
        }
    }
}
