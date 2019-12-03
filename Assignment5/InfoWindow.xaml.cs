using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        /// <summary>
        /// Sets up the window.
        /// </summary>
        public InfoWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Closes the window to go back to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// If validation is passed, saves the user's name and age.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validaton
                if (NameTextBox.Text.Length == 0 || AgeTextBox.Text.Length == 0)
                {
                    ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                    ErrorLabel.Content = "You must enter a name" + "\r\n" + "and an age.";
                    return;
                }

                //Validation
                foreach (char c in AgeTextBox.Text)
                {
                    if (!Char.IsDigit(c))
                    {
                        ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                        ErrorLabel.Content = "Please enter a number" + "\r\n" + "greater than 0 in the Age field.";
                        return;
                    }
                }

                //Validation
                int ageConverted = 0;
                Int32.TryParse(AgeTextBox.Text, out ageConverted);
                if (ageConverted < 1)
                {
                    ErrorLabel.Content = "Please enter a number" + "\r\n" + "greater than 0 in the Age field.";
                    return;
                }

                
                ErrorLabel.Foreground = new SolidColorBrush(Colors.Blue);
                ErrorLabel.Content = "Saved! Click the 'Back'" + "\r\n" + "button to continue.";

                //Saving user's info
                User.UserName = NameTextBox.Text;
                User.Age = ageConverted;
                User.Set = true;
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
