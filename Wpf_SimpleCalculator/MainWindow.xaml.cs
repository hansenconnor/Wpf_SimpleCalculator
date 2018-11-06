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
            if (artists == null)
            {
                MessageBox.Show("Not found!");
                return;
            }
            foreach (Artist artist in artists)
            {
                if (artist != null)
                {
                    artistSeeds.Add(artist.id);
                }
            }

            //Append the results to the search bar
            seedStackPanel.Children.Insert(0,
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
            // Return if artistSeeds is empty
            if (artistSeeds == null || artistSeeds.Count() == 0)
            {
                MessageBox.Show("Please enter some artists!");
                return;
            }

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


            List <TrackRecommendation> trackRecommendations = new List<TrackRecommendation>();


            // Call results windows containing reccommendations
            JObject recommendationsObject = dataService.getRecommendations(artistSeeds, danceability, energy, popularity, resultLimit);
            if (recommendationsObject == null)
            {
                MessageBox.Show("Something went wrong! Please clear your selection and try again.");
            }
            foreach (var recommendationObject in recommendationsObject["tracks"])
            {
                TrackRecommendation trackRecommendation = new TrackRecommendation();

                // Get track name and link
                string trackName = recommendationObject["name"].ToString();
                string trackLink = recommendationObject["external_urls"]["spotify"].ToString();
                trackRecommendation.track = new string[,]
                {
                    {trackName, trackLink } // Add the track name and link to the list of recommendations
                };

                // Get album name and link
                string albumName = recommendationObject["album"]["name"].ToString();
                string albumLink = recommendationObject["album"]["external_urls"]["spotify"].ToString();
                trackRecommendation.album = new string[,]
                {
                    {albumName, albumLink } // Add the album name and link to the list of recommendations
                };

                // Get the album images
                trackRecommendation.images = new List<string>();
                foreach (var image in recommendationObject["album"]["images"])
                {
                    string imageUrl = image["url"].ToString();
                    trackRecommendation.images.Add(imageUrl); // Add the image urls to the list of recommendations
                }

                // Get the artists featured on the track
                foreach (var artist in recommendationObject["artists"])
                {
                    string artistName = "";
                    string artistLink = "";

                    artistName = artist["name"].ToString();
                    artistLink = artist["external_urls"]["spotify"].ToString();

                    string[,] artistNameAndLink = new string[,] { 
                        { artistName, artistLink }
                    };

                    trackRecommendation.artists = new List<string[,]>();
                    trackRecommendation.artists.Add(artistNameAndLink);

                    // Add artist list to list of track recommendations
                    //trackRecommendations.Add(trackRecommendation);
                }

                // Add track recommendation to the list of track recommendations
                trackRecommendations.Add(trackRecommendation);
            }

            // Display the results window
            Window solutionWindow = new SolutionWindow(trackRecommendations);
            solutionWindow.Show();

            //string artistName = (string)recommendationsObject["artists"][0]["name"];
            //string artistId = (string)recommendationsObject["artists"]["items"][0]["id"];

            Console.WriteLine();
        }


        // Help button was clicked
        private void helpLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Display the help screen
            Window helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        // Reset fields
        private void getReccommendationsButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            // Clear list of artist seeds
            artistSeeds.Clear();

            // Remove artist labels from seedBox
            seedStackPanel.Children.Clear();

            // Reset all form inputs
            searchBox.Text = "";
            danceabilitySlider.Value = 0.5;
            energySlider.Value = 0.5;
            popularitySlider.Value = 50;
            resultLimitSlider.Value = 20;
        }
    }
}
