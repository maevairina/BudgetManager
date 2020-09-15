using BudgetManager.Data;
using BudgetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Control
{
    public class ControlMain
    {
        LocalDatabase database;
        Endpoints endpoints;

        public ControlMain()
        {
            database = LocalDatabase.GetInstance();
            endpoints = new Endpoints();
        }

        public List<MenuItem> GetMenuList()
        {
            List<MenuItem> menu = new List<MenuItem>
            {
                new MenuItem("DASHBOARD", MaterialDesignThemes.Wpf.PackIconKind.ViewDashboard),
                new MenuItem("COMPTES", MaterialDesignThemes.Wpf.PackIconKind.Account),
                new MenuItem("ENTRÉES", MaterialDesignThemes.Wpf.PackIconKind.PlusCircleOutline),
                new MenuItem("SORTIE", MaterialDesignThemes.Wpf.PackIconKind.MinusCircleOutline),
                new MenuItem("TRANSFERTS", MaterialDesignThemes.Wpf.PackIconKind.SwapHorizontal),
                new MenuItem("OBJECTIFS", MaterialDesignThemes.Wpf.PackIconKind.Dropbox)
            };
            return menu;
        }

        public void GetAriaryAPI()
        {
            DateTime StartDate = DateTime.Now.AddDays(-10);
            DateTime EndDate = DateTime.Now;

            var dollares = database.GetAriary();

            if (dollares.Count > 0)
            {
                StartDate = dollares.OrderByDescending(d=>d.DateTimeQuota).FirstOrDefault().DateTimeQuota.AddDays(1);
                EndDate = DateTime.Today;
            }

            var result = endpoints.GetDollars(StartDate, EndDate);

            foreach (var item in result)
            {
                if (dollares
                    .Where(d => 
                        d.DateTimeQuota.ToShortDateString()
                        .Equals(item.DateTimeQuota.ToShortDateString()))
                    .Count()
                    .Equals(0))
                {
                    database.AddDollar(item.PurchaseQuota, item.QuotationSale, item.DateTimeQuota);
                }
            }
        }
    }
}
