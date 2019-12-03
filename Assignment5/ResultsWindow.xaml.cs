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
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        /// <summary>
        /// Sets up window and sets labels for score and time.
        /// </summary>
        public ResultsWindow()
        {
            try
            {
                InitializeComponent();
                PercentageLabel.Content = User.Score;
                TimeLabel.Content = User.EndTime;
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Takes the user back to the main screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Users the array in the User class to see which problems were ansewred correctly. 
        /// Animals will be displayed based on which answers were correct.
        /// </summary>
        public void DisplayCorrectAnimals()
        {
            try
            {
                if (User.WhichCorrect[0])
                    Image1.Source = new BitmapImage(new Uri(@"Images\camel.png", UriKind.Relative));
                else
                    Image1.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[1])
                    Image2.Source = new BitmapImage(new Uri(@"Images\elephant.png", UriKind.Relative));
                else
                    Image2.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[2])
                    Image3.Source = new BitmapImage(new Uri(@"Images\giraffe.png", UriKind.Relative));
                else
                    Image3.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[3])
                    Image4.Source = new BitmapImage(new Uri(@"Images\hippo.png", UriKind.Relative));
                else
                    Image4.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[4])
                    Image5.Source = new BitmapImage(new Uri(@"Images\kangaroo.png", UriKind.Relative));
                else
                    Image5.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[5])
                    Image6.Source = new BitmapImage(new Uri(@"Images\lion.png", UriKind.Relative));
                else
                    Image6.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[6])
                    Image7.Source = new BitmapImage(new Uri(@"Images\meerkat.png", UriKind.Relative));
                else
                    Image7.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[7])
                    Image8.Source = new BitmapImage(new Uri(@"Images\ostrich.png", UriKind.Relative));
                else
                    Image8.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[8])
                    Image9.Source = new BitmapImage(new Uri(@"Images\rhino.png", UriKind.Relative));
                else
                    Image9.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));

                if (User.WhichCorrect[9])
                    Image10.Source = new BitmapImage(new Uri(@"Images\zebra.png", UriKind.Relative));
                else
                    Image10.Source = new BitmapImage(new Uri(@"Images\shadow.png", UriKind.Relative));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }
    }
}
