﻿using resurec.ViewModels;
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

namespace resurec.Views
{
    /// <summary>
    /// Interaction logic for RecordingHistoryView.xaml
    /// </summary>
    public partial class RecordingHistoryView : UserControl
    {
        public RecordingHistoryView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem && listViewItem.DataContext is RecordingViewModel recording)
            {
                if (recording.StartEditingCommand.CanExecute(null))
                {
                    recording.StartEditingCommand.Execute(null);
                }
            }
        }
    }
}
