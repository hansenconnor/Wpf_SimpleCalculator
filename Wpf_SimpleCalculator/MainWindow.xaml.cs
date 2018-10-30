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

namespace Wpf_SimpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retrieve user search query from text box and GET possible artists from API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getArtists(object sender, RoutedEventArgs e)
        {
            // Get the search query
            var searchQuery = searchBox.Text;

            // Handle empty search query
            if (searchQuery == "")
            {
                // Empty query => return
                return;
            }

            // Create new GET request

        }
    }
}
