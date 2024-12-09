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
using resurec.ViewModels;
using resurec.ViewModels.CustomLvc;

namespace resurec.Views
{
    /// <summary>
    /// Interaction logic for ResurecView.xaml
    /// </summary>
    public partial class ResurecView : UserControl
    {
        public ResurecView()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is ResurecViewModel viewModel)
            {
                viewModel.RamUsage.RescaleLabels(ActualWidth, ActualHeight);
                viewModel.CpuTemperature.RescaleLabels(ActualWidth, ActualHeight);
                viewModel.GpuTemperature.RescaleLabels(ActualWidth, ActualHeight);
            }
        }
    }
}
