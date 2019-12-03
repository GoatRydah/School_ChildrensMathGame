using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Sets up a game window for use throughout runtime.
        /// </summary>
        GameWindow gameWindow;

        /// <summary>
        /// Sets up a high scores window for use throughout runtime.
        /// </summary>
        HighScoresWindow highScores;

        /// <summary>
        /// Sets up a user info window for use throughout runtime.
        /// </summary>
        InfoWindow infoWindow;

        /// <summary>
        /// Sets up a results window for use throughout runtime.
        /// </summary>
        ResultsWindow scoreboard;

        /// <summary>
        /// Sets up audio for the intro music.
        /// </summary>
        SoundPlayer music = new SoundPlayer("back_music.wav");

        /// <summary>
        /// Sets up the main window and assigns each other window.
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                gameWindow = new GameWindow();
                highScores = new HighScoresWindow();
                infoWindow = new InfoWindow();
                scoreboard = new ResultsWindow();
                music.PlayLooping();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// If validation is passed, sets up a new game for user by calling the game window
        /// that was set up before, calling its set up method and opening it up. When game 
        /// window is complete, opens score window. When score window closes
        /// Pauses and resumes music appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validation
                if (!User.Set)
                {
                    ErrorLabel.Content = "Please enter your info first by" + "\r\n" + "clicking the set info button.";
                    return;
                }

                //Validation
                if (AddRadioButton.IsChecked == false && SubtractRadioButton.IsChecked == false && MultiplyRadioButton.IsChecked == false && DivideRadioButton.IsChecked == false)
                {
                    ErrorLabel.Content = "Choose a game type first.";
                    return;
                }

                this.Hide();

                //Game Window
                gameWindow.SetGameUp();
                music.Stop();
                gameWindow.ShowDialog();

                //Score Window
                if (!gameWindow.cancelled)
                    ShowScoreBoard();

                //Return to main window
                music.PlayLooping();
                this.Show();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Validates that user has set info. Then sends game type choice to User class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!User.Set)
                {
                    ErrorLabel.Content = "You must click the set info Button" + "\r\n" + "and fill out your name first.";
                    AddRadioButton.IsChecked = false;
                    return;
                }
                User.UsersLatestChoice = "Addition";
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        ///  Validates that user has set info. Then sends game type choice to User class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubtractRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!User.Set)
                {
                    ErrorLabel.Content = "You must click the set info Button" + "\r\n" + "and fill out your name first.";
                    SubtractRadioButton.IsChecked = false;
                    return;
                }
                User.UsersLatestChoice = "Subtraction";
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        ///  Validates that user has set info. Then sends game type choice to User class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiplyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!User.Set)
                {
                    ErrorLabel.Content = "You must click the set info Button" + "\r\n" + "and fill out your name first.";
                    MultiplyRadioButton.IsChecked = false;
                    return;
                }
                User.UsersLatestChoice = "Multiplication";
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        ///  Validates that user has set info. Then sends game type choice to User class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivideRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!User.Set)
                {
                    ErrorLabel.Content = "You must click the set info Button" + "\r\n" + "and fill out your name first.";
                    DivideRadioButton.IsChecked = false;
                    return;
                }
                User.UsersLatestChoice = "Division";
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Calls methods in the Results window to set up and display the user's score and animals found.
        /// </summary>
        public void ShowScoreBoard()
        {
            try
            {

                this.Hide();

                scoreboard.NameLabel.Content = User.UserName;
                scoreboard.PercentageLabel.Content = User.Score;
                scoreboard.TimeLabel.Content = User.EndTime;
                scoreboard.DisplayCorrectAnimals();
                scoreboard.ShowDialog();

                this.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// Opens up the info page for the user to set their info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();

                ErrorLabel.Content = "";
                infoWindow.ErrorLabel.Content = "";
                infoWindow.ShowDialog();

                this.Show();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
