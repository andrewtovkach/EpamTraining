using System.Drawing;
using System.Web.Mvc;
using BLL;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class ChartsController : Controller
    {
        public ActionResult Bar()
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { Type = ChartTypes.Bar })
                .SetTitle(new Title { Text = "Historic World Population by Region" })
                .SetSubtitle(new Subtitle { Text = "Source: Wikipedia.org" })
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "Africa", "America", "Asia", "Europe", "Oceania" },
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle
                    {
                        Text = "Population (millions)",
                        Align = AxisTitleAligns.High
                    }
                })
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' millions'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -100,
                    Y = 100,
                    Floating = true,
                    BorderWidth = 1,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Year 1800", Data = new Data(new object[] { 107, 31, 635, 203, 2 }) },
                    new Series { Name = "Year 1900", Data = new Data(new object[] { 133, 156, 947, 408, 6 }) },
                    new Series { Name = "Year 2008", Data = new Data(new object[] { 973, 914, 4054, 732, 34 }) }
                });

            return View(chart);
        }

        public ActionResult Line()
        {
            var list = new DataForLineChart().ListLineDatas;
            Series[] array = new Series[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = new Series {Name = list[i].Name, Data = new Data(list[i].List)};
            }
            string[] cat = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Line,
                    MarginRight = 130,
                    MarginBottom = 25,
                    ClassName = "chart"
                })
                .SetTitle(new Title
                {
                    Text = "Monthly Average Sales of Goods",
                    X = -20
                })
                .SetXAxis(new XAxis { Categories = cat })
                .SetYAxis(new YAxis
                {
                    Title = new YAxisTitle { Text = "BYR)" },
                    PlotLines = new[]
                    {
                        new YAxisPlotLines
                        {
                            Value = 0,
                            Width = 1,
                            Color = ColorTranslator.FromHtml("#808080")
                        }
                    }
                })
                .SetTooltip(new Tooltip
                {
                    Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'BYR';
                                }"
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -10,
                    Y = 100,
                    BorderWidth = 0
                })
                .SetSeries(array);

            return View(chart);
        }

        public ActionResult Pie()
        {
            var list = new DataForPieChart().ListPieDatas;
            object[] objects = new object[list.Count];
            for (int i = 0; i < list.Count; i++)
                objects[i] = list[i];
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "The structure of the sale of goods" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Product sale share",
                    Data = new Data(objects)
                });

            return View(chart);
        }
    }
}