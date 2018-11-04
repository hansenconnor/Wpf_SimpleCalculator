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
using Wpf_SimpleCalculator.DAL;
using Wpf_SimpleCalculator.Models;
using Wpf_SimpleCalculator.BLL;
using Newtonsoft.Json.Linq;

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


        // Form vars
        List<string> artistSeeds = new List<string>();


        /// <summary>
        /// Retrieve user search query from text box and GET possible artists from API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getArtists(object sender, RoutedEventArgs e)
        {
            // Get the search query
            var searchQuery = searchBox.Text;

            // Statically set type => could let user specify in future versions
            ItemType type = ItemType.artist;

            // Statically set limit => could let user specify in future versions
            int limit = 10;

            // Define our data service
            JSONDataService jsonDataService = new JSONDataService();
            
            // Create a new Artist BLL and pass our DataService
            ArtistsBLL artistBLL = new ArtistsBLL(jsonDataService);
            
            // Get a list of artists from the search query input
            List<Artist> artists = artistBLL.GetArtists( searchQuery, type, limit );

            // Add artist(s) ID to public list for use in reccommendation search
            foreach (Artist artist in artists)
            {
                artistSeeds.Add(artist.id);
            }

            //Append the results to the search bar
            seedStackPanel.Children.Add(
                new Label
                {
                    Content = artists[0].name,
                    Foreground = Brushes.White
                }
            );
        }


        /// <summary>
        /// Take all user form inputs and fetch recommendations from API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getReccommendationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Get artist seeds from the stack panel
            foreach (var artistId in artistSeeds)
            {
                Console.WriteLine(artistId);
            }

            // Get remaining form values
            string danceability = danceabilitySlider.Value.ToString();
            string energy       = energySlider.Value.ToString();
            string popularity   = popularitySlider.Value.ToString();
            string resultLimit = resultLimitSlider.Value.ToString();

            JSONDataService dataService = new JSONDataService();

            // Call results windows containing reccommendations
            JObject recommendationsObject = dataService.getRecommendations(artistSeeds, danceability, energy, popularity, resultLimit);

            Console.WriteLine();
        }
    }
}
