using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Charting.Data;
using SysExp.Models.ViewModels;
using SysExp.Models;
using Microsoft.AspNetCore.Authorization;
using SysExp.Utility;
using System.IO;

namespace SysExp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.ManagerUser)]
    public class ChartDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChartDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ChartData
        public async Task<IActionResult> Index(string sortOrder)
        {
            /* var applicationDbContext = _context.ChartData.Include(c => c.Portfolio).Include(c => c.Strategy);
             return View(await applicationDbContext.ToListAsync());*/

            ViewData["PortfolioSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var charts = from s in _context.ChartData.Include(s => s.Portfolio).Include(s => s.Strategy)
                         select s;
            switch (sortOrder)
            {
                case "name_desc":
                    charts = charts.OrderByDescending(s => s.PortfolioId);
                    break;
                case "Date":
                    charts = charts.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    charts = charts.OrderByDescending(s => s.Date);
                    break;
                default:
                    charts = charts.OrderBy(s => s.PortfolioId);
                    break;
            }
            return View(await charts.AsNoTracking().ToListAsync());
        }

        //GET: Chart

        /*public async Task<IActionResult> ToChart()
        {
            ToChartViewModel model = new ToChartViewModel()
            {
                ChartDataList = await _context.ChartData.ToListAsync(),
                ChartData = new ChartData()
            };

            model.ChartDataList = model.ChartDataList.Where(s => s.PortfolioId.Equals(1));
            model.Portfolios = 0;

            return View(model);
        }*/


        // GET
        public async Task<IActionResult> ToChartAll()
        {

            ToChartViewModel model = new ToChartViewModel()
            {
                ChartDataList = await _context.ChartData.ToListAsync(),
                ChartData = new ChartData(),
                SortedListData = new SortedList<DateTime, List<int>>()
            };

            model.ChartDataList = from s in model.ChartDataList
                                  select s;

            List<int> portfolioList1 = new List<int>();
            List<int> portfolioList2 = new List<int>();
            List<int> portfolioList3 = new List<int>();
            List<int> portfolioList4 = new List<int>();
            List<DateTime> dateList = new List<DateTime>();

            foreach (var item in model.ChartDataList.Where(s => s.PortfolioId.Equals(1)).OrderBy(s => s.Date))
            {
                portfolioList1.Add(item.Equity);

            }
            portfolioList1.Count();
            foreach (var item in model.ChartDataList.Where(s => s.PortfolioId.Equals(2)).OrderBy(s => s.Date))
            {
                portfolioList2.Add(item.Equity);

            }
            portfolioList2.Count();
            foreach (var item in model.ChartDataList.Where(s => s.PortfolioId.Equals(1002)).OrderBy(s => s.Date))
            {
                portfolioList3.Add(item.Equity);
            }
            portfolioList3.Count();
            foreach (var item in model.ChartDataList.Where(s => s.PortfolioId.Equals(1004)).OrderBy(s => s.Date))
            {
                portfolioList4.Add(item.Equity);
            }
            portfolioList4.Count();

            foreach (var item in model.ChartDataList.Where(s => s.PortfolioId.Equals(1)).OrderBy(s => s.Date))
            {
                dateList.Add(item.Date);
            }
            dateList.Count();

            SortedList<DateTime, List<int>> sortedList = new SortedList<DateTime, List<int>>();

            for (int i = 0; i < dateList.Count(); i++)
            {
                sortedList.Add(dateList[i], new List<int> { portfolioList1[i], portfolioList2[i], portfolioList3[i], portfolioList4[i] });
            }

            model.SortedListData = sortedList;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToChart(int portfolios)
        {
            ToChartViewModel model = new ToChartViewModel()
            {
                ChartDataList = await _context.ChartData.ToListAsync(),
                ChartData = new ChartData(),
                SortedListData = new SortedList<DateTime, List<int>>()
            };


            model.ChartDataList = from s in model.ChartDataList
                                  select s;

            if (ModelState.IsValid)
            {
                switch (portfolios)
                {

                    case 0:
                        return RedirectToAction(nameof(ToChartAll));

                    case 1:
                        model.ChartDataList = model.ChartDataList.Where(s => s.PortfolioId.Equals(1));
                        break;
                    case 2:
                        model.ChartDataList = model.ChartDataList.Where(s => s.PortfolioId.Equals(2));
                        break;
                    case 3:
                        model.ChartDataList = model.ChartDataList.Where(s => s.PortfolioId.Equals(1002));
                        break;
                    case 4:
                        model.ChartDataList = model.ChartDataList.Where(s => s.PortfolioId.Equals(1004));
                        break;

                    default:
                        break;


                }

                model.Portfolios = portfolios;

                return View(model);

            }

            return View(model);
        }

        // GET: Admin/ChartData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chartData = await _context.ChartData
                .Include(c => c.Portfolio)
                .Include(c => c.Strategy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chartData == null)
            {
                return NotFound();
            }

            return View(chartData);
        }

        // GET: Admin/ChartData/Create
        public IActionResult Create()
        {
            ViewData["PortfolioId"] = new SelectList(_context.Portfolio, "Id", "Name");
            ViewData["StrategyId"] = new SelectList(_context.Strategy, "Id", "Name");
            return View();
        }

        // POST: Admin/ChartData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DateToString,Equity,PortfolioId,StrategyId")] ChartData chartData)
        {
            if (ModelState.IsValid)
            {
                chartData.DateToString = chartData.Date.ToString("yyyy/M/dd");
                _context.Add(chartData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolio, "Id", "Name", chartData.PortfolioId);
            ViewData["StrategyId"] = new SelectList(_context.Strategy, "Id", "Name", chartData.StrategyId);
            return View(chartData);
        }

        //GET: CreateFromFile

        public IActionResult CreateFromFile()
        {
            ViewData["PortfolioId"] = new SelectList(_context.Portfolio, "Id", "Name");
            ViewData["StrategyId"] = new SelectList(_context.Strategy, "Id", "Name");
            return View();
        }

        //Post: CreateFromFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromFile([Bind("Id,Date,DateToString,Equity,PortfolioId,StrategyId")] ChartData chartData)
        {
            if (ModelState.IsValid)
            {
                string[] xyValues;
                DateTime x;
                int y = 0;
                List<DateTime> dateList = new List<DateTime>();
                SortedList<DateTime, int> sortedDictionary = new SortedList<DateTime, int>();
                int portfolioId = chartData.PortfolioId;
                int strategyId = chartData.StrategyId;

                //***atidaro visus csv exportuotus failus pasirinktame kataloge ir istraukia data, profit losss ir treidu skaiciu ***
                //***Naudojama DuplicateKeyComparer klase dublikatams sujungti "data stucte" SortedList tinka ir Dictionary

                var files = HttpContext.Request.Form.Files;
                using (StreamReader file = new StreamReader(files[0].OpenReadStream()))
                {
                    string line = file.ReadLine();

                    while (line != null)

                    {

                        xyValues = line.Split(',');
                        string date = xyValues[0];
                        x = DateTime.ParseExact(date, string.Format("M/d/yyyy"), null);
                        if (!String.IsNullOrEmpty(xyValues[1]))
                        {
                            y = int.Parse(xyValues[1]);
                        }


                        ChartData data = new ChartData();
                        data.PortfolioId = portfolioId;
                        data.StrategyId = strategyId;
                        data.Date = x;
                        data.DateToString = data.Date.ToString("yyyy/M/dd");
                        data.Equity = y;

                        _context.ChartData.Add(data);

                        line = file.ReadLine();
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PortfolioId"] = new SelectList(_context.Portfolio, "Id", "Name", chartData.PortfolioId);
            ViewData["StrategyId"] = new SelectList(_context.Strategy, "Id", "Name", chartData.StrategyId);
            return View(chartData);
        }



        // GET: Admin/ChartData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chartData = await _context.ChartData.FindAsync(id);
            if (chartData == null)
            {
                return NotFound();
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolio, "Id", "Name", chartData.PortfolioId);
            ViewData["StrategyId"] = new SelectList(_context.Strategy, "Id", "Name", chartData.StrategyId);
            return View(chartData);
        }

        // POST: Admin/ChartData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DateToString,Equity,PortfolioId,StrategyId")] ChartData chartData)
        {
            if (id != chartData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    chartData.DateToString = chartData.Date.ToString("yyyy/M/dd");
                    _context.Update(chartData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChartDataExists(chartData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolio, "Id", "Name", chartData.PortfolioId);
            ViewData["StrategyId"] = new SelectList(_context.Strategy, "Id", "Name", chartData.StrategyId);
            return View(chartData);
        }

        // GET: Admin/ChartData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chartData = await _context.ChartData
                    .Include(c => c.Portfolio)
                    .Include(c => c.Strategy)
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (chartData == null)
            {
                return NotFound();
            }

            return View(chartData);
        }

        // POST: Admin/ChartData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chartData = await _context.ChartData.FindAsync(id);
            _context.ChartData.Remove(chartData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //POST DeleteALl

        public async Task<IActionResult> DeleteAll()
        {
            var list = await _context.ChartData.ToListAsync();
            _context.ChartData.RemoveRange(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChartDataExists(int id)
        {
            return _context.ChartData.Any(e => e.Id == id);
        }
    }
}
