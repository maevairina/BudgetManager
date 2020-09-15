using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BudgetManager.Model
{
    public class Dollar
    {
        public Dollar(DateTime dateTimeQuota, double purchaseQuota, double quotationSale)
        {
            DateTimeQuota = dateTimeQuota;
            PurchaseQuota = purchaseQuota;
            QuotationSale = quotationSale;
        }

        public DateTime DateTimeQuota { get; private set; }
        public double PurchaseQuota { get; private set; }
        public double QuotationSale { get; private set; }
    }
}
