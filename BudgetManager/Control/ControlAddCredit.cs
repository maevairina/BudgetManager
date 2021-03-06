﻿using BudgetManager.Data;
using BudgetManager.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Control
{
    public class ControlAddCredit
    {
        LocalDatabase database;

        private Goal Goal { get; set; }

        public ControlAddCredit(object goal)
        {
            database = LocalDatabase.GetInstance();
            Goal = (Goal)goal;
        }

        public object GetGoal() => Goal;

        internal void AddCredit(double value)
        {
            Goal.Credit(value);
            database.UpdateGoal();
        }
        internal List<Goal> GetGoalCategories() => database.GetGoals().Where(g => g.IsCategory == true).ToList();

        internal void DebitGoal(object goal, double value)
        {
            var Goal = (Goal)goal;
            Goal.Debit(value);
            database.UpdateGoal();
        }
    }
}
