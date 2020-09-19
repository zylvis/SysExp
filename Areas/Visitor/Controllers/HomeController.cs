using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Charting.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SysExp.Areas.Admin.Controllers;
using SysExp.Models;
using SysExp.Models.ViewModels;

namespace SysExp.Controllers
{
    [Area("Visitor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }

        public async Task<IActionResult> Index(string filterString)
        {
            HomeIndexViewModel modelVM = new HomeIndexViewModel()
            {

                Category = await _db.Category.ToListAsync(),
                CategoryFilter = await _db.Category.ToListAsync(),
                Portfolio = await _db.Portfolio.ToListAsync(),
                Strategy = await _db.Strategy.ToListAsync(),
                Description = await _db.Description.ToListAsync(),

                ChartDataList = await _db.ChartData.ToListAsync(),
                ChartData = new ChartData(),
                SortedListData = new SortedList<DateTime, List<int>>()
            };

            //chart manipulation

            modelVM.ChartDataList = from s in modelVM.ChartDataList
                                  select s;

            List<int> portfolioList1 = new List<int>();
            List<int> portfolioList2 = new List<int>();
            List<int> portfolioList3 = new List<int>();
            List<int> portfolioList4 = new List<int>();
            List<DateTime> dateList = new List<DateTime>();

            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1)).OrderBy(s => s.Date))
            {
                portfolioList1.Add(item.Equity);

            }
            portfolioList1.Count();
            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(2)).OrderBy(s => s.Date))
            {
                portfolioList2.Add(item.Equity);

            }
            portfolioList2.Count();
            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1002)).OrderBy(s => s.Date))
            {
                portfolioList3.Add(item.Equity);
            }
            portfolioList3.Count();
            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1004)).OrderBy(s => s.Date))
            {
                portfolioList4.Add(item.Equity);
            }
            portfolioList4.Count();

            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1)).OrderBy(s => s.Date))
            {
                dateList.Add(item.Date);
            }
            dateList.Count();

            SortedList<DateTime, List<int>> sortedList = new SortedList<DateTime, List<int>>();

            for (int i = 0; i < dateList.Count(); i++)
            {
                sortedList.Add(dateList[i], new List<int> { portfolioList1[i], portfolioList2[i], portfolioList3[i], portfolioList4[i] });
            }

            modelVM.SortedListData = sortedList;

            //tabs
            var tab = from s in modelVM.Category
                      select s;

            if (!String.IsNullOrEmpty(filterString))
            {
                tab = tab.Where(s => s.Name.ToString().Contains(filterString));
            }

            modelVM.CategoryFilter = tab;
         
            return View(modelVM);
        }

        public async Task<IActionResult> OneSChart(string filterString, int portfolios)
        {
            HomeIndexViewModel modelVM = new HomeIndexViewModel()
            {

                Category = await _db.Category.ToListAsync(),
                CategoryFilter = await _db.Category.ToListAsync(),
                Portfolio = await _db.Portfolio.ToListAsync(),
                Strategy = await _db.Strategy.ToListAsync(),
                Description = await _db.Description.ToListAsync(),

                ChartDataList = await _db.ChartData.ToListAsync(),
                ChartData = new ChartData(),
                SortedListData = new SortedList<DateTime, List<int>>()
            };

            //chart manipulation

            modelVM.ChartDataList = from s in modelVM.ChartDataList
                                    select s;

            if (ModelState.IsValid)
            {
                switch (portfolios)
                {

                    case 0:
                        return RedirectToAction(nameof(AllSChart));

                    case 1:
                        modelVM.ChartDataList = modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1));
                        break;
                    case 2:
                        modelVM.ChartDataList = modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(2));
                        break;
                    case 3:
                        modelVM.ChartDataList = modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1002));
                        break;
                    case 4:
                        modelVM.ChartDataList = modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1004));
                        break;

                    default:
                        break;


                }

                modelVM.Portfolios = portfolios;
             
                

                //tabs
                var tab = from s in modelVM.Category
                          select s;

                if (!String.IsNullOrEmpty(filterString="Charts"))
                {
                    tab = tab.Where(s => s.Name.ToString().Contains(filterString));
                }

                modelVM.CategoryFilter = tab;

                
            }
            return View(modelVM);
        }

        public async Task<IActionResult> AllSChart(string filterString = "Charts")
        {
            HomeIndexViewModel modelVM = new HomeIndexViewModel()
            {

                Category = await _db.Category.ToListAsync(),
                CategoryFilter = await _db.Category.ToListAsync(),
                Portfolio = await _db.Portfolio.ToListAsync(),
                Strategy = await _db.Strategy.ToListAsync(),
                Description = await _db.Description.ToListAsync(),

                ChartDataList = await _db.ChartData.ToListAsync(),
                ChartData = new ChartData(),
                SortedListData = new SortedList<DateTime, List<int>>()
            };

            List<int> portfolioList1 = new List<int>();
            List<int> portfolioList2 = new List<int>();
            List<int> portfolioList3 = new List<int>(); 
            List<int> portfolioList4 = new List<int>();
            List<DateTime> dateList = new List<DateTime>();

            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1)).OrderBy(s => s.Date))
            {
                portfolioList1.Add(item.Equity);

            }
            portfolioList1.Count();
            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(2)).OrderBy(s => s.Date))
            {
                portfolioList2.Add(item.Equity);

            }
            portfolioList2.Count();
            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1002)).OrderBy(s => s.Date))
            {
                portfolioList3.Add(item.Equity);
            }
            portfolioList3.Count();
            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1004)).OrderBy(s => s.Date))
            {
                portfolioList4.Add(item.Equity);
            }
            portfolioList4.Count();

            foreach (var item in modelVM.ChartDataList.Where(s => s.PortfolioId.Equals(1)).OrderBy(s => s.Date))
            {
                dateList.Add(item.Date);
            }
            dateList.Count();

            SortedList<DateTime, List<int>> sortedList = new SortedList<DateTime, List<int>>();

            for (int i = 0; i < dateList.Count(); i++)
            {
                sortedList.Add(dateList[i], new List<int> { portfolioList1[i], portfolioList2[i], portfolioList3[i], portfolioList4[i] });
            }

            modelVM.SortedListData = sortedList;

            //tabs
            var tab = from s in modelVM.Category
                      select s;

            if (!String.IsNullOrEmpty(filterString))
            {
                tab = tab.Where(s => s.Name.ToString().Contains(filterString));
            }

            modelVM.CategoryFilter = tab;

            return View(modelVM);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
