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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf_SimpleCalculator.Models;

namespace Wpf_SimpleCalculator
{
    /// <summary>
    /// Interaction logic for SolutionWindow.xaml
    /// </summary>
    public partial class SolutionWindow : Window
    {
        public SolutionWindow(List<TrackRecommendation> recommendations)
        {
            InitializeComponent();

            DisplayRecommendations(recommendations);



            //Paragraph p = new Paragraph();

            //Run run1 = new Run("Hyperlink Sample ");
            //Run run2 = new Run(" Hyperlink added");
            //Run run3 = new Run("C# Corner ");
            //// Create a Hyperlink and set NavigateUri  
            //Hyperlink hlink = new Hyperlink(run3);
            //hlink.NavigateUri = new Uri("http://www.c-sharpcorner.com");
            //// Add Runs and Hyperlink to Paragraph  
            //p.Inlines.Add(run1);
            //p.Inlines.Add(hlink);
            //p.Inlines.Add(run2);

            
        }

        private void DisplayRecommendations(List<TrackRecommendation> recommendations)
        {
            foreach (var recommendation in recommendations)
            {
                List<Hyperlink> artistLinks = new List<Hyperlink>();

                foreach (var artist in recommendation.artists)
                {
                    // Create the artist link
                    Run artistName = new Run(artist[0, 0]);
                    Hyperlink artistLink = new Hyperlink(artistName);
                    artistLink.NavigateUri = new Uri(artist[0, 1]);
                    artistLinks.Add(artistLink);
                }

                Paragraph artists = new Paragraph();
                artists.Inlines.Add(artistLinks.ToString());

                // Format the list of artist links with comma delimeter

                // Create new XAML layout
                // resultsPanel.Children.Add(artists);

                // Add data to layout
                string albumImageUrl = recommendation.images.First();
                string stringPath = albumImageUrl;
                Uri imageUri = new Uri(stringPath, UriKind.Absolute);
                BitmapImage albumImageBitmap = new BitmapImage(imageUri);

                CreateRecommendationLayout(albumImageBitmap);
            }
        }

        private void CreateRecommendationLayout(BitmapImage albumImageBitmap)
        {
            // Create grid to hold recommendations
            Grid myGrid = new Grid();
            myGrid.Width = 250;
            myGrid.Height = 100;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = false;
            myGrid.Margin = new Thickness(0, 20, 0, 20);

            var myBorder = new Border();

            DropShadowEffect myDropShadowEffect = new DropShadowEffect();
            // Set the color of the shadow to Black.
            Color myShadowColor = new Color();
            myShadowColor.ScA = 1;
            myShadowColor.ScB = 0;
            myShadowColor.ScG = 0;
            myShadowColor.ScR = 0;
            myDropShadowEffect.Color = myShadowColor;

            // Set the direction of where the shadow is cast to 320 degrees.
            myDropShadowEffect.Direction = 320;

            // Set the depth of the shadow being cast.
            myDropShadowEffect.ShadowDepth = 25;

            // Set the shadow softness to the maximum (range of 0-1).

            // Set the shadow opacity to half opaque or in other words - half transparent.
            // The range is 0-1.
            myDropShadowEffect.Opacity = 0.5;

            myBorder.Effect = myDropShadowEffect;
            myGrid.Effect = myDropShadowEffect;

            Grid.SetColumnSpan(myBorder, 2);
            Grid.SetRowSpan(myBorder, 2);



            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef2);

            // Add the album image to the grid
            Image albumImage = new Image();
            albumImage.Source = albumImageBitmap;
            Grid.SetColumnSpan(albumImage, 1);
            Grid.SetRowSpan(albumImage, 2);

            myGrid.Children.Add(albumImage);
            myGrid.Children.Add(myBorder);
            resultsPanel.Children.Add(myGrid);
            Console.WriteLine();
        }


        private void CreateDynamicStackPanel()
        {
            // Create a StackPanel and set its properties  
            StackPanel dynamicStackPanel = new StackPanel();
            dynamicStackPanel.Width = 300;
            dynamicStackPanel.Height = 200;
            dynamicStackPanel.Background = new SolidColorBrush(Colors.LightBlue);
            dynamicStackPanel.Orientation = Orientation.Horizontal;
            // Create Ellipses and add to StackPanel  
            Ellipse redCircle = new Ellipse();
            redCircle.Width = 100;
            redCircle.Height = 100;
            redCircle.Fill = new SolidColorBrush(Colors.Red);
            dynamicStackPanel.Children.Add(redCircle);
            Ellipse orangeCircle = new Ellipse();
            orangeCircle.Width = 80;
            orangeCircle.Height = 80;
            orangeCircle.Fill = new SolidColorBrush(Colors.Orange);
            dynamicStackPanel.Children.Add(orangeCircle);
            Ellipse yellowCircle = new Ellipse();
            yellowCircle.Width = 60;
            yellowCircle.Height = 60;
            yellowCircle.Fill = new SolidColorBrush(Colors.Yellow);
            dynamicStackPanel.Children.Add(yellowCircle);
            Ellipse greenCircle = new Ellipse();
            greenCircle.Width = 40;
            greenCircle.Height = 40;
            greenCircle.Fill = new SolidColorBrush(Colors.Green);
            dynamicStackPanel.Children.Add(greenCircle);
            Ellipse blueCircle = new Ellipse();
            blueCircle.Width = 20;
            blueCircle.Height = 20;
            blueCircle.Fill = new SolidColorBrush(Colors.Blue);
            dynamicStackPanel.Children.Add(blueCircle);
            // Display StackPanel into a Window  
            solutionWindow.Content = dynamicStackPanel;
        }
    }
}


//privatevoid CreateDynamicStackPanel()
//{
//    // Create a StackPanel and set its properties  
//    StackPanel dynamicStackPanel = newStackPanel();
//    dynamicStackPanel.Width = 300;
//    dynamicStackPanel.Height = 200;
//    dynamicStackPanel.Background = newSolidColorBrush(Colors.LightBlue);
//    dynamicStackPanel.Orientation = Orientation.Horizontal;
//    // Create Ellipses and add to StackPanel  
//    Ellipse redCircle = newEllipse();
//    redCircle.Width = 100;
//    redCircle.Height = 100;
//    redCircle.Fill = newSolidColorBrush(Colors.Red);
//    dynamicStackPanel.Children.Add(redCircle);
//    Ellipse orangeCircle = newEllipse();
//    orangeCircle.Width = 80;
//    orangeCircle.Height = 80;
//    orangeCircle.Fill = newSolidColorBrush(Colors.Orange);
//    dynamicStackPanel.Children.Add(orangeCircle);
//    Ellipse yellowCircle = newEllipse();
//    yellowCircle.Width = 60;
//    yellowCircle.Height = 60;
//    yellowCircle.Fill = newSolidColorBrush(Colors.Yellow);
//    dynamicStackPanel.Children.Add(yellowCircle);
//    Ellipse greenCircle = newEllipse();
//    greenCircle.Width = 40;
//    greenCircle.Height = 40;
//    greenCircle.Fill = newSolidColorBrush(Colors.Green);
//    dynamicStackPanel.Children.Add(greenCircle);
//    Ellipse blueCircle = newEllipse();
//    blueCircle.Width = 20;
//    blueCircle.Height = 20;
//    blueCircle.Fill = newSolidColorBrush(Colors.Blue);
//    dynamicStackPanel.Children.Add(blueCircle);
//    // Display StackPanel into a Window  
//    RootWindow.Content = dynamicStackPanel;
//}