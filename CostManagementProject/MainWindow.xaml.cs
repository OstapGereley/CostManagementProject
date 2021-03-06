﻿using CostManagementProject.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using System.Windows;
using System.Linq;
using System;

namespace CostManagementProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public ObservableCollection<YearGrowth> YearStats = new ObservableCollection<YearGrowth>();
        public ObservableCollection<YearGrowth> YearGrowthCriteriaStats = new ObservableCollection<YearGrowth>();
        public ObservableCollection<YearGrowth> YearGrowthRateStats = new ObservableCollection<YearGrowth>();
        public ObservableCollection<YearGrowth> YearSpiermanStats = new ObservableCollection<YearGrowth>();
        public ObservableCollection<FehnerCompare> FehnerStats = new ObservableCollection<FehnerCompare>();
        public ObservableCollection<CompanyScale> ScaleStats = new ObservableCollection<CompanyScale>();
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

            YearStats.Add(new YearGrowth()
            {
                Year = 2012,
                NetProfit = 3792,//"Чистий прибуток\збиток"
                SalesNetIncome = 671554,//"Чистий дохід від реалізації"
                Cost = 667762,//собівартість
                AverageAssets = 318453.5,//"Середньорічна вартість активів"
                AverageFixedAssets = 329855,//"Середньорічна вартість основних засобів"
                AverageCurrentAssets = 201850.5,//"Середньорічна вартість оборотних активів"
                EmployeeCount = 2093,//"Середньоспискова чисельність працівників"

            });

            YearStats.Add(new YearGrowth()
            {
                Year = 2013,
                NetProfit = 3925,//"Чистий прибуток\збиток"
                SalesNetIncome = 497620,//"Чистий дохід від реалізації"
                Cost = 493695,//собівартість
                AverageAssets = 830832.5,//"Середньорічна вартість активів"
                AverageFixedAssets = 738413,//"Середньорічна вартість основних засобів"
                AverageCurrentAssets = 92458.5,//"Середньорічна вартість оборотних активів"
                EmployeeCount = 1457,//"Середньоспискова чисельність працівників"

            });

            YearStats.Add(new YearGrowth()
            {
                Year = 2014,
                NetProfit = 34816,//"Чистий прибуток\збиток"
                SalesNetIncome = 294354,//"Чистий дохід від реалізації"
                Cost = 259538,//собівартість
                AverageAssets = 800849,//"Середньорічна вартість активів"
                AverageFixedAssets = 730892,//"Середньорічна вартість основних засобів"
                AverageCurrentAssets = 69936,//"Середньорічна вартість оборотних активів"
                EmployeeCount = 1383,//"Середньоспискова чисельність працівників"

            });

            YearStats.Add(new YearGrowth()
            {
                Year = 2015,
                NetProfit = 11021,//"Чистий прибуток\збиток"
                SalesNetIncome = 250516,//"Чистий дохід від реалізації"
                Cost = 239495,//собівартість
                AverageAssets = 744672,//"Середньорічна вартість активів"
                AverageFixedAssets = 654011,//"Середньорічна вартість основних засобів"
                AverageCurrentAssets = 90640,//"Середньорічна вартість оборотних активів"
                EmployeeCount = 757,//"Середньоспискова чисельність працівників"

            });

            YearStats.Add(new YearGrowth()
            {
                Year = 2016,
                NetProfit = 12905.59,//"Чистий прибуток\збиток"
                SalesNetIncome = 298865.6,//"Чистий дохід від реалізації"
                Cost = 220095.9,//собівартість
                AverageAssets = 836266.7,//"Середньорічна вартість активів"
                AverageFixedAssets = 654011,//"Середньорічна вартість основних засобів"
                AverageCurrentAssets = 90640,//"Середньорічна вартість оборотних активів"
                EmployeeCount = 757,//"Середньоспискова чисельність працівників"

            });

            YearsCount = YearStats.Count;
            YearGrowthGrid.ItemsSource = YearStats;
            YearGrowthCriteriaGrid.ItemsSource = YearGrowthCriteriaStats;
            YearGrowthRateGrid.ItemsSource = YearGrowthRateStats;
            YearSpirmanGrid.ItemsSource = YearSpiermanStats;
            FehnerGrid.ItemsSource = FehnerStats;
            ScaleRateGrid.ItemsSource = ScaleStats;
        }



        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            YearsCount++;
            YearStats.Add(new YearGrowth());
        }

        private void SubButtonClick(object sender, RoutedEventArgs e)
        {
            if (YearsCount == 1)
                return;

            YearsCount--;
            YearStats.RemoveAt(YearStats.Count - 1);
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            ScaleStats.Clear();
            FehnerStats.Clear();
            YearSpiermanStats.Clear();
            YearGrowthRateStats.Clear();
            YearGrowthCriteriaStats.Clear();
            YearStats.Clear();
            YearsCount = YearStats.Count;
        }

        private void CalculateButtonClick(object sender, RoutedEventArgs e)
        {
            if (YearStats.ToList().Count == 0)
                return;

            var calcsList = new TestModule().Run(YearStats.ToList());
            calcsList.RemoveAt(0);

            foreach (YearGrowthCriteries year in calcsList)
            {
                var tempRate = new YearGrowth(year.Year, 
                    Math.Round(year.Criteriums[0].Rate,3), 
                    Math.Round(year.Criteriums[1].Rate,3), 
                    Math.Round(year.Criteriums[2].Rate,3), 
                    Math.Round(year.Criteriums[3].Rate,3), 
                    Math.Round(year.Criteriums[4].Rate,3), 
                    Math.Round(year.Criteriums[5].Rate,3), 
                    Math.Round(year.Criteriums[6].Rate,3));
                YearGrowthCriteriaStats.Add(tempRate);

                var tempArr = year.Criteriums.OrderBy(x => x.StandardRank).ToArray();
                var tempRank = new YearGrowth(year.Year,
                    tempArr[0].Rank,
                    tempArr[1].Rank,
                    tempArr[2].Rank,
                    tempArr[3].Rank,
                    tempArr[4].Rank,
                    tempArr[5].Rank,
                    tempArr[6].Rank);
                YearGrowthRateStats.Add(tempRank);

                var tempDev = new YearGrowth(year.Year,
                    Math.Pow(1 - tempArr[0].Rank, 2),
                    Math.Pow(2 - tempArr[1].Rank, 2),
                    Math.Pow(3 - tempArr[2].Rank, 2),
                    Math.Pow(4 - tempArr[3].Rank, 2),
                    Math.Pow(5 - tempArr[4].Rank, 2),
                    Math.Pow(6 - tempArr[5].Rank, 2),
                    Math.Pow(7 - tempArr[6].Rank, 2),
                    Math.Round(year.SpirmanKoef,3));
                YearSpiermanStats.Add(tempDev);

                var tempFeh = new FehnerCompare(year.Year,
                    year.FahnerPairCriteriums[0].Value,
                    year.FahnerPairCriteriums[1].Value,
                    year.FahnerPairCriteriums[2].Value,
                    year.FahnerPairCriteriums[3].Value,
                    year.FahnerPairCriteriums[4].Value,
                    year.FahnerPairCriteriums[5].Value,
                    year.FahnerPairCriteriums[6].Value,
                    year.FahnerPairCriteriums[7].Value,
                    year.FahnerPairCriteriums[8].Value,
                    year.FahnerPairCriteriums[9].Value,
                    year.FahnerPairCriteriums[10].Value,
                    year.FahnerPairCriteriums[11].Value,
                    year.FahnerPairCriteriums[12].Value,
                    year.FahnerPairCriteriums[13].Value,
                    year.FahnerPairCriteriums[14].Value,
                    year.FahnerPairCriteriums[15].Value,
                    year.FahnerPairCriteriums[16].Value,
                    year.FahnerPairCriteriums[17].Value,
                    year.FahnerPairCriteriums[18].Value,
                    year.FahnerPairCriteriums[19].Value,
                    year.FahnerPairCriteriums[20].Value,
                    year.FehnerSum,
                    Math.Round(year.FehnerKoef,3));
                FehnerStats.Add(tempFeh);

                var tempScale = new CompanyScale(year.Year, 
                    Math.Round(year.SpirmanKoef, 3), 
                    Math.Round(year.FehnerKoef, 3), 
                    Math.Round(year.ScaleLevel, 3));
                ScaleStats.Add(tempScale);
            }

        }
    }
}
