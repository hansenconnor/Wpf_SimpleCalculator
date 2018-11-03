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
using System.Windows.Shapes;
using Wpf_SimpleCalculator.BLL;
using Wpf_SimpleCalculator.DAL;
using Wpf_SimpleCalculator.Models;

namespace Wpf_SimpleCalculator
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// "Login" button => Refresh token using client id and secred
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Define our data service
            JSONDataService jsonDataService = new JSONDataService();

            string accessToken = jsonDataService.getAccessToken();

                // 64 encoded auth
            //YWE0ZjZiZDIyYjc3NGFiMzk2ODI1NzQ5NzNjYjIyNWY6MzhiODYwMDhmOGJkNDhkNDkzYjMzZDI1NTA0NmQzNzk=
        }
    }
}
