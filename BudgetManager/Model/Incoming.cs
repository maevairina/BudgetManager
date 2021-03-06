﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model
{
    public class Incoming : Transaction
    {
        public Account Account { get; private set; }

        public Incoming(DateTime date, Account account, double value, ItemCategory category)
        {
            Description = account.Name;
            Date = date;
            Account = account;
            Value = value;
            Category = category;
        }

        public override void Move(double value)
        {
            Account.Credit(value);
        }
    }
}
