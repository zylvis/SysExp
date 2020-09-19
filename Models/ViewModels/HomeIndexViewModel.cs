using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysExp.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Category> CategoryFilter { get; set; }
        public IEnumerable<Portfolio> Portfolio { get; set; }
        public IEnumerable<Strategy> Strategy { get; set; }
        public IEnumerable<Description> Description { get; set; }

        public IEnumerable<ChartData> ChartDataList { get; set; }
        public ChartData ChartData { get; set; }
        public int Portfolios { get; set; }
        public enum EPortfolios { All = 0, ES_CZHV = 1, ES_Baras = 2, All_CZHV = 3, All_Baras = 4 }
        public SortedList<DateTime, List<int>> SortedListData { get; set; }
    }
}