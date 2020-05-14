using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace ForWinQuant
{
    class ChartHelper
    {

        public static void AddSeries(Chart chart, string seriesName, SeriesChartType chartType, Color color, Color markColor, bool showValue = false)
        {
            chart.Series.Add(seriesName);
            chart.Series[seriesName].ChartType = chartType;
            chart.Series[seriesName].Color = color;
            if (showValue)
            {
                chart.Series[seriesName].IsValueShownAsLabel = true;
                chart.Series[seriesName].MarkerStyle = MarkerStyle.Circle;
                chart.Series[seriesName].MarkerColor = markColor;
                chart.Series[seriesName].LabelForeColor = color;
                chart.Series[seriesName].LabelAngle = -90;
            }
        }


        public static void SetTitle(Chart chart, string chartName, Font font, Docking docking, Color foreColor)
        {
            chart.Titles.Add(chartName);
            chart.Titles[0].Font = font;
            chart.Titles[0].Docking = docking;
            chart.Titles[0].ForeColor = foreColor;
        }


        public static void SetStyle(Chart chart, Color backColor, Color foreColor)
        {
            chart.BackColor = backColor;
            chart.ChartAreas[0].BackColor = backColor;
            chart.ForeColor = Color.Red;
        }


        public static void SetLegend(Chart chart, Docking docking, StringAlignment align, Color backColor, Color foreColor)
        {
            chart.Legends[0].Docking = docking;
            chart.Legends[0].Alignment = align;
            chart.Legends[0].BackColor = backColor;
            chart.Legends[0].ForeColor = foreColor;
        }


        public static void SetXY(Chart chart, string xTitle, string yTitle, StringAlignment align, Color foreColor, Color lineColor, AxisArrowStyle arrowStyle, double xInterval, double yInterval)
        {
            chart.ChartAreas[0].AxisX.Title = xTitle;
            chart.ChartAreas[0].AxisY.Title = yTitle;
            chart.ChartAreas[0].AxisX.TitleAlignment = align;
            chart.ChartAreas[0].AxisY.TitleAlignment = align;
            chart.ChartAreas[0].AxisX.TitleForeColor = foreColor;
            chart.ChartAreas[0].AxisY.TitleForeColor = foreColor;
            chart.ChartAreas[0].AxisX.LabelStyle = new LabelStyle() { ForeColor = foreColor };
            chart.ChartAreas[0].AxisY.LabelStyle = new LabelStyle() { ForeColor = foreColor };
            chart.ChartAreas[0].AxisX.LineColor = lineColor;
            chart.ChartAreas[0].AxisY.LineColor = lineColor;
            chart.ChartAreas[0].AxisX.ArrowStyle = arrowStyle;
            chart.ChartAreas[0].AxisY.ArrowStyle = arrowStyle;
            chart.ChartAreas[0].AxisX.Interval = xInterval;
            chart.ChartAreas[0].AxisY.Interval = yInterval;
        }


        public static void SetMajorGrid(Chart chart, Color lineColor, double xInterval, double yInterval)
        {
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = lineColor;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = lineColor;
            chart.ChartAreas[0].AxisX.MajorGrid.Interval = xInterval;
            chart.ChartAreas[0].AxisY.MajorGrid.Interval = yInterval;
        }
    }
}
