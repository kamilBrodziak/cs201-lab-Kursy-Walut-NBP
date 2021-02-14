using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            EndDate.SelectedDate = null;
        }

        private void Submit_Click(object sender, RoutedEventArgs e) {
            var checkedCurr = CurrencyPanel.Children.OfType<RadioButton>()
                .FirstOrDefault(el => el.IsChecked.HasValue && el.IsChecked.Value).Content;
            if(StartDate.SelectedDate.HasValue) {
                var startDate = StartDate.SelectedDate.Value;
                if(EndDate.SelectedDate.HasValue) {
                    var endDate = EndDate.SelectedDate.Value;
                    Run((string)checkedCurr, startDate, endDate);
                } else {
                    InfoBlock.Text = "Select end date first!";
                }
            } else {
                InfoBlock.Text = "Select start date first!";
            }
        }

        private void Run(string code, DateTime startDate, DateTime endDate) {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.WriteLine("Wait a moment, informations are being retrieved.");
            try {
                InfoBlock.Text = "Fetching data, please wait...";
                AverageBlock.Text = StandardDeviationBlock.Text = MinBlock.Text = MaxBlock.Text = LargestDiffBlock.Text = LargestDiffDatesBlock.Text = "";
                var currencyInfoTools = new CurrencyInfoTools(
                CurrencyInfoRetriever.GetCurrencyInfoListFromFiles(code,
                CurrencyInfoRetriever.GetCurrencyXMLFilesNamesListFromFile(startDate, endDate)));
                AverageBlock.Text = $"{currencyInfoTools.Average()}";
                StandardDeviationBlock.Text = $"{currencyInfoTools.StandardDeviation()}";
                MinBlock.Text = $"{currencyInfoTools.Min().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}";
                MaxBlock.Text = $"{currencyInfoTools.Max().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}";
                foreach(var currPair in currencyInfoTools.LargestExchangeRatesDifferences()) {
                    LargestDiffBlock.Text = $"{Math.Abs(currPair.First() - currPair.Last())}";
                    LargestDiffDatesBlock.Text += $"{currPair.First().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}" +
                        $" - {currPair.Last().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}\n";
                }
                InfoBlock.Text = "Success";
            } catch(Exception) {
                InfoBlock.Text = "Error occurred.";
            }

        }
    }
}
