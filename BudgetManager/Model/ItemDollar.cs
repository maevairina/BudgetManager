using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BudgetManager.Model
{
    public class ItemDollar
    {
        public string Value { get; private set; }
        public string Day { get; private set; }

        public ItemDollar(double value, DateTime day)
        {
            Value = value.ToString("c");

            if (day.Day.Equals(DateTime.Now.Day))
                Day = "aujourd'hui";
            else if (day.Day.Equals(DateTime.Now.AddDays(-1).Day))
                Day = "hier";
            else
                Day = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(day.DayOfWeek);
        }
    }
}
