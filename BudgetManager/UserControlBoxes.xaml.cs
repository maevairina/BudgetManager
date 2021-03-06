﻿using BudgetManager.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logique pour UserControlBoxes.xaml
    /// </summary>
    public partial class UserControlBoxes : UserControl
    {
        ControlGoal control;

        public UserControlBoxes()
        {
            InitializeComponent();
            control = new ControlGoal();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var messageQueue = SnackbarThree.MessageQueue;
            if (TextBoxName.Text == string.Empty)
            {
                TextBoxName.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Saisissez le nom"));
                return;
            }

            if (TextBoxValue.Text == string.Empty)
            {
                TextBoxValue.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Entrez la valeur"));
                return;
            }

            string name = TextBoxName.Text;
            Double value = Double.Parse(TextBoxValue.Text, NumberStyles.Currency);
            DateTime deadline = DatePickerDeadline.SelectedDate ?? DateTime.Now;

            control.SaveGoal(name, value, deadline);
            LoadGoals();

            TextBoxName.Text = string.Empty;
            TextBoxValue.Text = string.Empty;
            DatePickerDeadline.Text = string.Empty;

            LoadBox();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGoals();
            LoadBox();
        }

        private void LoadBox()
        {
            var box = control.GetBox();

            TextBlockAmountAvailable.DataContext = box;
        }

        private void TextBoxValue_LostFocus(object sender, RoutedEventArgs e)
        {
            Double.TryParse(TextBoxValue.Text, out double result);

            if (!(new Regex(@"\d+(,\d{1,2})?")).Match(result.ToString()).Success)
            {
                TextBoxValue.Text = string.Empty;
            }
            else
            {
                TextBoxValue.Text = result.ToString("C");
            }
        }

        private void TextBoxValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxValue.Text = string.Empty;
        }

        private void LoadGoals()
        {
            var goals = control.GetGoalList();
            ListViewGoals.ItemsSource = null;
            GridCategory.DataContext = control.GetCategories();
            if (goals.Count > 0)
            {
                ListViewGoals.ItemsSource = goals;
                TextBlockGoalsEmpty.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockGoalsEmpty.Visibility = Visibility.Visible;
            }
        }

        private void ButtonAddCredit_Click(object sender, RoutedEventArgs e)
        {
            WindowAddCredit credit = new WindowAddCredit(((Button)sender).DataContext);
            credit.ShowDialog();

            LoadGoals();
            LoadBox();
        }

        private void ButtonFinishGoal_Click(object sender, RoutedEventArgs e)
        {
            control.GoalDone(((Button)sender).DataContext);

            LoadGoals();
            LoadBox();
        }

        private void ButtonCategorize_Click(object sender, RoutedEventArgs e)
        {
            control.Categorize();
            LoadGoals();
            LoadBox();
        }
    }
}
