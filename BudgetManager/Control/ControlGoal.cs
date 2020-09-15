using BudgetManager.Data;
using BudgetManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Control
{
    public class ControlGoal
    {
        LocalDatabase database;

        Box Box;

        public ControlGoal()
        {
            database = LocalDatabase.GetInstance();
        }

        public List<Goal> GetGoalList() => database.GetGoals().Where(g => !g.Done).OrderByDescending(g=>g.Balance).ToList();
        public List<Goal> GetCategoryList() => database.GetGoals().Where(g => g.IsCategory).ToList();

        internal void SaveGoal(string name, double goalValue, DateTime deadline) => database.AddGoal(name, goalValue, deadline);

        private void LoadBox() => Box = new Box(database.GetAccounts().Sum(a => a.Amount), database.GetGoals().ToList());

        internal Box GetBox()
        {
            LoadBox();
            return Box;
        }

        internal void GoalDone(object goal)
        {
            if (!((Goal)goal).IsCategory)
            {
                ((Goal)goal).SetDone();
                database.UpdateGoal();
            }
        }

        internal GoalCategory GetCategories()
        {
            var essential = GetCategoryList().Where(g => g.Name.Equals("Essentielle")).Select(g => g).FirstOrDefault();
            var education = GetCategoryList().Where(g => g.Name.Equals("Education")).Select(g => g).FirstOrDefault();
            var investiment = GetCategoryList().Where(g => g.Name.Equals("Investissement")).Select(g => g).FirstOrDefault();
            var other = GetCategoryList().Where(g => g.Name.Equals("Autres")).Select(g => g).FirstOrDefault();

            return new GoalCategory(essential, education, investiment, other);
        }

        internal void Categorize()
        {
            if (Box.AmountAvailable > 0)
            {
                var category = GetCategories();
                category.SetValues(Box.AmountAvailable);
                database.UpdateGoal();
            }
        }
    }
}
