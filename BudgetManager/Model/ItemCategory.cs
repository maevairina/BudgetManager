using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model
{
    public class ItemCategory
    {
        public string Name { get; set; }
        public string Group { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
