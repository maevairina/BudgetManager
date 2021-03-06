﻿using BudgetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logique pour UserControlItemMonthlyIncome.xaml
    /// </summary>
    public partial class UserControlItemMonthlyIncome : UserControl
    {
        public UserControlItemMonthlyIncome(MonthlyIncome item, double maximum)
        {
            InitializeComponent();

            DataContext = item;

            GridIncoming.Height = (item.Incomings * 130) / maximum;
        }
    }
}
