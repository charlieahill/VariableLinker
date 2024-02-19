using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

using VariableLinker.Properties;

namespace VariableLinker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WebsiteLinks = SaveLoad.Load(FilePath);
            RefreshListBoxItemsSource();

            StandardArgumentsTextBox.DataContext = this;
            StandardArgumentsTextBox.Text = StandardArguments;
        }

        public string FilePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/CHillSW/VarLink/links.not";

        public BindingList<WebsiteModel> WebsiteLinks { get; set; } = new BindingList<WebsiteModel>();

        public ObservableCollection<WebsiteModel> DataModels { get; set; }

        public string StandardArguments = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

        private void AddNewLinkButton_Click(object sender, RoutedEventArgs e)
        {
            WebsiteLinks.Add(new WebsiteModel());
            RefreshListBoxItemsSource();
        }

        private void RefreshListBoxItemsSource()
        {
            QuickLinksListView.ItemsSource = null;
            QuickLinksListView.ItemsSource = WebsiteLinks;
        }

        private void NumberInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            
            ProcessInputCommand((WebsiteModel)QuickLinksListView.SelectedItem);
        }

        private void ProcessInputCommand(WebsiteModel websiteModel)
        {
            //todo: Work out if to import or override default args
            //todo: trims - and only take the first part of a string etc.
            Process.Start(StandardArguments, $"{websiteModel.SiteUrlPrecursor}{websiteModel.UserInput}{websiteModel.SiteUrlPostcursor}{websiteModel.OverrideDefaultArgumennts}");
            //MessageBox.Show($"{websiteModel.SiteName}: {websiteModel.SiteUrlPrecursor}{websiteModel.UserInput}{websiteModel.SiteUrlPostcursor}{websiteModel.OverrideDefaultArgumennts}");
        }

        private void PasteSingleButton_Click(object sender, RoutedEventArgs e)
        {
            ((WebsiteModel)(QuickLinksListView.SelectedItem)).UserInput = Clipboard.GetText();

            ProcessInputCommand((WebsiteModel)QuickLinksListView.SelectedItem);
        }

        private void PasteMultiButton_Click(object sender, RoutedEventArgs e)
        {
            //todo: Implement paste multi button
        }

        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            //todo: Implement delete row
        }

        private void ShowSettingsOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuickLinksListView.Width == 246)
                QuickLinksListView.Width = double.NaN;
            else
                QuickLinksListView.Width = 246;

            if(StandardArgumentsTextBox.Visibility == Visibility.Hidden)
                StandardArgumentsTextBox.Visibility = Visibility.Visible;
            else
                StandardArgumentsTextBox.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            foreach (WebsiteModel link in WebsiteLinks)
                link.UserInput = string.Empty;

            WebsiteLinks.Save(FilePath);
        }
    }
}

//todo: window jumps size around content
//todo: Add new sites
//todo: delete old sites
//todo: move sites up and down list in terms of priority
//todo: Quick links to other things
//todo: Intelligent quick links - deviations list etc.
//todo: intelligent quick links with copy paste functionality etc
//todo: always on top
//todo: When pasting in text - the text box does not update
//todo: the Normal Arguements text box at the bottom of the page does not update
//todo: The noermal arguemnts text box at the bottom of the page does not save
//todo: Do I need to split the arguments into two parts? Browser and addons?