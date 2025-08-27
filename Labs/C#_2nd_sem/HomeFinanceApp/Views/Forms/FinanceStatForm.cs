using HomeFinanceApp.Models;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Views.Forms
{
    internal class FinanceStatsForm : Form
    {
        private LiveCharts.WinForms.PieChart pieChart;
        private readonly Stat _statistics;
        private readonly string _title;

        public FinanceStatsForm(Stat statistics, string title)
        {
            _statistics = statistics;
            _title = title;
            this.Text = _title;
            this.Width = 800;
            this.Height = 600;

            pieChart = new LiveCharts.WinForms.PieChart
            {
                Dock = DockStyle.Fill,
                LegendLocation = LegendLocation.Right
            };

            this.Controls.Add(pieChart);
            UpdateChart();
        }

        public void UpdateChart()
        {
            SeriesCollection series = new SeriesCollection();

            // Группируем расходы по типам
            var groupedExpenses = _statistics.wasteMoneyOnExpenses
                .GroupBy(e => e.Item1.ExpenseSubTypes)
                .Select(g => new
                {
                    ExpenseType = g.Key,
                    TotalAmount = g.Sum(x => x.Item2)
                })
                .OrderByDescending(x => x.TotalAmount);

            foreach (var expense in groupedExpenses)
            {
                series.Add(new PieSeries
                {
                    Title = expense.ExpenseType.ToString(),
                    Values = new ChartValues<decimal> { expense.TotalAmount },
                    DataLabels = true
                });
            }

            // Добавляем кредиты, если они есть
            if (_statistics.wasteMoneyOnCredits.Any())
            {
                decimal totalCredits = _statistics.wasteMoneyOnCredits.Sum(x => x.Item2);
                series.Add(new PieSeries
                {
                    Title = "Кредиты",
                    Values = new ChartValues<decimal> { totalCredits },
                    DataLabels = true
                });
            }

            // Добавляем доходы, если они есть
            if (_statistics.incomeMoneyToFamily.Any())
            {
                decimal totalIncome = _statistics.incomeMoneyToFamily.Sum(x => x.IncomeAmount);
                series.Add(new PieSeries
                {
                    Title = "Доходы",
                    Values = new ChartValues<decimal> { totalIncome },
                    DataLabels = true
                });
            }

            pieChart.Series = series;
        }
    }
}
