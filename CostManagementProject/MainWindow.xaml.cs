using CostManagementProject.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CostManagementProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<YearGrowth> YearStats = new ObservableCollection<YearGrowth>();
        private int yearsCount;
        public int YearsCount
        {
            get
            {
                return yearsCount;
            }
            set
            {
                yearsCount = value;
                ElementsCountLabel.Content = value;
            }
        }
        public MainWindow()
        {            
            InitializeComponent();

            new TestModule().Run();

            YearStats.Add(new YearGrowth(1, 1, 1, 1, 1, 1, 1, 1));
            YearsCount = YearStats.Count;
            YearGrowthGrid.ItemsSource = YearStats;


        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            YearsCount++;
            YearStats.Add(new YearGrowth());
        }

        private void SubButtonClick(object sender, RoutedEventArgs e)
        {
            YearsCount--;
            YearStats.RemoveAt(YearStats.Count - 1);
        }
    }
}
