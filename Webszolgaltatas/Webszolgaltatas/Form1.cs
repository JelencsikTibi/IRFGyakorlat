using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Webszolgaltatas.Entities;
using Webszolgaltatas.MnbServiceReference;

namespace Webszolgaltatas
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        

        public Form1()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();
            string xmlstring = Consume();
            LoadXml(xmlstring);
            dataGridView1.DataSource = Rates;
            DrawChart();
        }

        private void DrawChart()
        {
            chartRateData.DataSource = Rates;

            Series series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            series.Color = Color.OrangeRed;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void LoadXml(string input)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(input);

            foreach (XmlElement element in xml.DocumentElement)
            {
                RateData rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
                
            }
        }

        string Consume()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = valutaComboBox.SelectedItem.ToString(); //"EUR"
            request.startDate = tolDatePicker.Value.ToString("yyyy-MM-dd"); //"2020-01-01"
            request.endDate = igDatePicker.Value.ToString("yyyy-MM-dd"); //"2020-06-30"
            var response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            return result;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        
    }
}
