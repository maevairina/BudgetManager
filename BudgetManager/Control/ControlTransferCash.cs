﻿using BudgetManager.Data;
using BudgetManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Control
{
    public class ControlTransferCash
    {
        LocalDatabase database;

        public DateTime ActualDate { get; private set; }

        public ControlTransferCash()
        {
            ActualDate = DateTime.Now;

            database = LocalDatabase.GetInstance();
        }

        public List<TransferCash> GetTransferList()
        {
            var transferCashList = database.GetTransfers();
            return (from t in transferCashList
                    where t.Date.Year.Equals(ActualDate.Year) && t.Date.Month.Equals(ActualDate.Month)
                    select t).OrderByDescending(t=>t.Date).ToList();
        }

        internal void NextMonth() => ActualDate = ActualDate.AddMonths(1);

        internal List<Account> GetAccountList() => database.GetAccounts().Where(a => a.Enabled).ToList();

        internal void PreviousMonth()
        {
            ActualDate = ActualDate.AddMonths(-1);
        }

        internal void SaveTransferCash(double value, DateTime date, object accountOut, object accountIn)
        {
            var accOut = (from a in GetAccountList()
                       where a.Name.Equals(((Account)accountOut).Name)
                       select a).FirstOrDefault();

            var accIn = (from a in GetAccountList()
                       where a.Name.Equals(((Account)accountIn).Name)
                       select a).FirstOrDefault();

            database.AddTransferCash(value, date, accOut, accIn);
        }

        internal void Delete(object transfer)
        {
            var t = (TransferCash)transfer;
            var accountIn = (from a in database.GetAccounts()
                           where a.Name.Equals(t.AccountIn.Name)
                           select a).FirstOrDefault();
            var accountOut = (from a in database.GetAccounts()
                             where a.Name.Equals(t.AccountOut.Name)
                             select a).FirstOrDefault();

            accountIn.Debit(t.Value);
            accountOut.Credit(t.Value);
            database.DeleteTransfer(t);
        }
    }
}
