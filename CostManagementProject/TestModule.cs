﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CostManagementProject.Models;

namespace CostManagementProject
{
    class TestModule
    {

        class Criterium
        {
            public int Id { get; set; }

            public double Value { get; set; }

            public double Rate { get; set; }

            public int StandardRank { get; set; }

            public int Rank { get; set; }

            public double GetRankDeviation()//квадрат рангових відхилень
            {
                return Math.Pow(Rank - StandardRank, 2);
            }
        }

      

        class YearGrowsCriteries
        {
            public YearGrowsCriteries()
            {
                Criteriums = new List<Criterium>();
            }

            public double Year { get; set; }

            public List<Criterium> Criteriums { get; set; }

            public double GetCriteriumsDeviationsSum()//сума квадратів рангових відхилень
            {
                double sum = 0;

                foreach (var criterium in Criteriums)
                {
                    sum += criterium.GetRankDeviation();
                }

                return sum;
            }

            public double SpirmanKoef { get; set; }
            
             
        }

        public void Run()
        {
            List<YearGrowth> yearGrowths = GetYearGrowths();//table 1-3

            List<YearGrowsCriteries> yearsCriterieses = GetYearsGrowsCriterieses(yearGrowths);//table 3 formatted

            yearsCriterieses = GetYearsGrowthRateCriteries(yearsCriterieses);//table 4

            yearsCriterieses = getYearsRanks(yearsCriterieses);//table 5

            
            CalculateSpirman(yearsCriterieses);//table 6

        }

        private static void CalculateSpirman(List<YearGrowsCriteries> yearsCriterieses)
        {
            for (var i = 1; i < yearsCriterieses.Count; ++i)
            {
                var yearCriterieses = yearsCriterieses[i];
                var n = yearCriterieses.Criteriums.Count;
                

                yearCriterieses.SpirmanKoef = 1 - yearCriterieses.GetCriteriumsDeviationsSum()* (6.0/(n*(n*n - 1)));
            }
        }

        private static List<YearGrowsCriteries> getYearsRanks(List<YearGrowsCriteries> yearsCriterieses)
        {
            for (int i = 1; i < yearsCriterieses.Count; ++i)
            {
                var yearCriteries = yearsCriterieses[i];
                var sortedRanks = yearCriteries.Criteriums.GroupBy(x=>x.Rate).Select(x=> x.Key).OrderByDescending(x => x).ToList();
                foreach (var criterium in yearCriteries.Criteriums)
                {
                    criterium.Rank = sortedRanks.FindIndex(x => x.Equals(criterium.Rate)) + 1;
                }
            }

            return yearsCriterieses;
        }


        private static List<YearGrowsCriteries> GetYearsGrowthRateCriteries(List<YearGrowsCriteries> yearsGrowsCriterieses)
        {
          
            for (int i = 1; i < yearsGrowsCriterieses.Count; ++i)
            {

                var yearGrowsCriterieses = yearsGrowsCriterieses[i];
                var yearGrowsCriteriesesPrev = yearsGrowsCriterieses[i - 1];

              
                for (int j = 0; j < yearGrowsCriterieses.Criteriums.Count; ++j)
                {
                    yearGrowsCriterieses.Criteriums[j].Rate =
                        (yearGrowsCriterieses.Criteriums[j].Value - yearGrowsCriteriesesPrev.Criteriums[j].Value)/
                        yearGrowsCriteriesesPrev.Criteriums[j].Value;
                }

                
            }

            return yearsGrowsCriterieses;
        }

        private static List<YearGrowsCriteries> GetYearsGrowsCriterieses(List<YearGrowth> yearGrowths)
        {
            List<YearGrowsCriteries> yearsGrowsCriterieses = new List<YearGrowsCriteries>();
            foreach (var yearGrowth in yearGrowths)
            {
                yearsGrowsCriterieses.Add(new YearGrowsCriteries()
                {
                    Year = yearGrowth.Year,
                    Criteriums = new List<Criterium>()
                    {
                        new Criterium() {Id = 1, Value = yearGrowth.NetProfit, StandardRank = 1},
                        new Criterium() {Id = 2, Value = yearGrowth.SalesNetIncome, StandardRank = 3},
                        new Criterium() {Id = 3, Value = yearGrowth.Cost, StandardRank = 6},
                        new Criterium() {Id = 4, Value = yearGrowth.AverageAssets, StandardRank = 4},
                        new Criterium() {Id = 5, Value = yearGrowth.AverageFixedAssets, StandardRank = 5},
                        new Criterium() {Id = 6, Value = yearGrowth.AverageCurrentAssets, StandardRank = 2},
                        new Criterium() {Id = 7, Value = yearGrowth.EmployeeCount, StandardRank = 7},
                    }
                });
            }
            return yearsGrowsCriterieses;
        }

        private static List<YearGrowth> GetYearGrowths()
        {
            return new List<YearGrowth>()
            {
                new YearGrowth()
                {
                    Year = 2012,
                    NetProfit = 3792,//"Чистий прибуток\збиток"
                    SalesNetIncome = 671554,//"Чистий дохід від реалізації"
                    Cost = 667762,//собівартість
                    AverageAssets = 318453.5,//"Середньорічна вартість активів"
                    AverageFixedAssets = 329855,//"Середньорічна вартість основних засобів"
                    AverageCurrentAssets = 201850.5,//"Середньорічна вартість оборотних активів"
                    EmployeeCount = 2093,//"Середньоспискова чисельність працівників"
                    
                },
                new YearGrowth()
                {
                    Year = 2013,
                    NetProfit = 3925,//"Чистий прибуток\збиток"
                    SalesNetIncome = 497620,//"Чистий дохід від реалізації"
                    Cost = 493695,//собівартість
                    AverageAssets = 830832.5,//"Середньорічна вартість активів"
                    AverageFixedAssets = 738413,//"Середньорічна вартість основних засобів"
                    AverageCurrentAssets = 92458.5,//"Середньорічна вартість оборотних активів"
                    EmployeeCount = 1457,//"Середньоспискова чисельність працівників"
                    
                },

                new YearGrowth()
                {
                    Year = 2014,
                    NetProfit = 34816,//"Чистий прибуток\збиток"
                    SalesNetIncome = 294354,//"Чистий дохід від реалізації"
                    Cost = 259538,//собівартість
                    AverageAssets = 800849,//"Середньорічна вартість активів"
                    AverageFixedAssets = 730892,//"Середньорічна вартість основних засобів"
                    AverageCurrentAssets = 69936,//"Середньорічна вартість оборотних активів"
                    EmployeeCount = 1383,//"Середньоспискова чисельність працівників"
                    
                },

                new YearGrowth()
                {
                    Year = 2015,
                    NetProfit = 11021,//"Чистий прибуток\збиток"
                    SalesNetIncome = 250516,//"Чистий дохід від реалізації"
                    Cost = 239495,//собівартість
                    AverageAssets = 744672,//"Середньорічна вартість активів"
                    AverageFixedAssets = 654011,//"Середньорічна вартість основних засобів"
                    AverageCurrentAssets = 90640,//"Середньорічна вартість оборотних активів"
                    EmployeeCount = 757,//"Середньоспискова чисельність працівників"
                    
                },

                new YearGrowth()
                {
                    Year = 2016,
                    NetProfit = 12905.59,//"Чистий прибуток\збиток"
                    SalesNetIncome = 298865.6,//"Чистий дохід від реалізації"
                    Cost = 220095.9,//собівартість
                    AverageAssets = 836266.7,//"Середньорічна вартість активів"
                    AverageFixedAssets = 654011,//"Середньорічна вартість основних засобів"
                    AverageCurrentAssets = 90640,//"Середньорічна вартість оборотних активів"
                    EmployeeCount = 757,//"Середньоспискова чисельність працівників"
                    
                },
            };
        }
    }
}