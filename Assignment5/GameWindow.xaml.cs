using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        /// <summary>
        /// Sets up an instance of a Game Object.
        /// </summary>
        Game newGame = new Game();

        /// <summary>
        /// Sets up audio for correct answers.
        /// </summary>
        SoundPlayer correct = new SoundPlayer("correct.wav");

        /// <summary>
        /// Sets up audio for incorrect answers.
        /// </summary>
        SoundPlayer incorrect = new SoundPlayer("incorrect.wav");

        /// <summary>
        /// A string that displays the full problem, including the
        /// left and right operands and the operator.
        /// </summary>
        string operandDisplay;

        /// <summary>
        /// Sets up the timer used in the game.
        /// </summary>
        DispatcherTimer myTimer;

        /// <summary>
        /// Keeps track of if the user exited the game early or not.
        /// </summary>
        public bool cancelled = false;

        /// <summary>
        /// The time when the user starts a game.
        /// </summary>
        DateTime startTime;

        /// <summary>
        /// The time when the user ends a game.
        /// </summary>
        DateTime endTime;

        /// <summary>
        /// The endTime - the startTime. Thus, the total time the 
        /// user played the game for.
        /// </summary>
        int totalTime;

        /// <summary>
        /// Initializes the game. Sets up the timer the rest of the way.
        /// </summary>
        public GameWindow()
        {
            try
            {
                InitializeComponent();

                //timer set up
                myTimer = new DispatcherTimer();
                myTimer.Interval = TimeSpan.FromMilliseconds(1000);
                myTimer.Tick += MyTimer_Tick;
                myTimer.Start();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This is called each second and passes the time to the timer label.
        /// Only passes the time, not the date itself.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                TimerLabel.Content = DateTime.Now.ToString().Substring(11, 10);
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method will help get the UI set up for a new game and will
        /// work with other classes to reset variables for a new game.
        /// </summary>
        public void SetGameUp()
        {
            try
            {
                //Setting game variables
                newGame.GameType = User.UsersLatestChoice;
                newGame.NumProblemsAnswered = 0;
                newGame.Score = 0;
                newGame.SetUpProblem();

                cancelled = false;

                //Clearing Displays
                FeedbackLabel.Content = "";
                ProblemAnswerTextBox.IsEnabled = true;
                ProblemNumberLabel.Content = "";
                ProblemAnswerTextBox.Text = "";
                CurrentProblemLabel.Content = "";
                operandDisplay = newGame.LeftOperand.ToString() + newGame.currOperator + newGame.RightOperand.ToString();
                CurrentProblemLabel.Content = operandDisplay;
                Keyboard.Focus(ProblemAnswerTextBox);
                Feedback2Label.Content = "";
                AnimalImage.SetValue(System.Windows.Controls.Image.SourceProperty, null);

                //Reseting Answers
                User.SetArrayToFalse();

                //Reseting timer
                startTime = DateTime.Now;
                myTimer.Interval = new TimeSpan(0, 0, 1);
                myTimer.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                      MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will check for validation when the button is pressed and if
        /// validation succeeds, will work with the game class to verify the user's
        /// answer to the problem. It will then call a method to get the next problem
        /// set up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validation
                foreach (char c in ProblemAnswerTextBox.Text)
                {
                    if (!Char.IsDigit(c))
                    {
                        FeedbackLabel.Foreground = new SolidColorBrush(Colors.Red);
                        FeedbackLabel.Content = "Try entering a positive number.";
                        return;
                    }
                }

                //Validation
                if (ProblemAnswerTextBox.Text == "")
                {
                    FeedbackLabel.Foreground = new SolidColorBrush(Colors.Red);
                    FeedbackLabel.Content = "Try again, you didn't give an answer.";
                    return;
                }


                FeedbackLabel.Content = "";
                CheckAnswer();

                if (newGame.NumProblemsAnswered == 10)
                {
                    GameOver();
                }

                NextProblem();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Works with the Game game class to check the user's answer and see if
        /// it was right. It will handle showing user correct/incorrect information
        /// and will display an animal if correct.
        /// </summary>
        private void CheckAnswer()
        {
            try
            {
                switch (newGame.GameType)
                {
                    case "Addition":
                        if (newGame.AddNumberPair(ProblemAnswerTextBox.Text))
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Green);
                            FeedbackLabel.Content = "Correct!";
                            correct.Play();
                            GetAnimal();
                            User.WhichCorrect[newGame.NumProblemsAnswered - 1] = true;
                        }
                        else
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Red);
                            FeedbackLabel.Content = "Incorrect.";
                            incorrect.Play();
                            Feedback2Label.Content = "";
                            AnimalImage.SetValue(System.Windows.Controls.Image.SourceProperty, null);
                        }
                        break;
                    case "Subtraction":
                        if (newGame.SubtractNumberPair(ProblemAnswerTextBox.Text))
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Green);
                            FeedbackLabel.Content = "Correct!";
                            correct.Play();
                            GetAnimal();
                            User.WhichCorrect[newGame.NumProblemsAnswered - 1] = true;
                        }
                        else
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Red);
                            FeedbackLabel.Content = "Incorrect.";
                            incorrect.Play();
                            Feedback2Label.Content = "";
                            AnimalImage.SetValue(System.Windows.Controls.Image.SourceProperty, null);
                        }
                        break;
                    case "Multiplication":
                        if (newGame.MultiplyNumberPair(ProblemAnswerTextBox.Text))
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Green);
                            FeedbackLabel.Content = "Correct!";
                            correct.Play();
                            GetAnimal();
                            User.WhichCorrect[newGame.NumProblemsAnswered - 1] = true;
                        }
                        else
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Red);
                            FeedbackLabel.Content = "Incorrect.";
                            incorrect.Play();
                            Feedback2Label.Content = "";
                            AnimalImage.SetValue(System.Windows.Controls.Image.SourceProperty, null);
                        }
                        break;
                    case "Division":
                        if (newGame.DivideNumberPair(ProblemAnswerTextBox.Text))
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Green);
                            FeedbackLabel.Content = "Correct!";
                            correct.Play();
                            GetAnimal();
                            User.WhichCorrect[newGame.NumProblemsAnswered - 1] = true;
                        }
                        else
                        {
                            FeedbackLabel.Foreground = new SolidColorBrush(Colors.Red);
                            FeedbackLabel.Content = "Incorrect.";
                            incorrect.Play();
                            Feedback2Label.Content = "";
                            AnimalImage.SetValue(System.Windows.Controls.Image.SourceProperty, null);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method helps set up the next problem for the user.
        /// </summary>
        private void NextProblem()
        {
            try
            {

                newGame.SetUpProblem();

                //Sets up UI portion
                operandDisplay = newGame.LeftOperand.ToString() + newGame.currOperator + newGame.RightOperand.ToString();
                CurrentProblemLabel.Content = operandDisplay;
                Keyboard.Focus(ProblemAnswerTextBox);
                ProblemAnswerTextBox.Text = "";
                ProblemNumberLabel.Content = "Problem Number: " + (newGame.NumProblemsAnswered + 1); 
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will end the game and save scores for setup for 
        /// the scores screen.
        /// </summary>
        private void GameOver()
        {
            try
            {
                //Gets time
                myTimer.Stop();
                endTime = DateTime.Now;
                TimeSpan ts = new TimeSpan();
                ts = endTime.Subtract(startTime);
                Int32.TryParse(ts.Seconds.ToString(), out totalTime);

                //Sets scores
                newGame.SetFinalScores(totalTime.ToString());
                User.Score = newGame.SPercentage;

                this.Hide();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will set the cancelled boolean to false so we know
        /// the game was ended early.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                cancelled = true;
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method will set the cancelled boolean to false so we know
        /// the game was ended early.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cancelled = true;
                newGame.NumProblemsAnswered = 0;
                newGame.Score = 0;

                this.Hide();
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method will get an animal to display when an answer was
        /// given correctly. It's based off which problem number the user
        /// is at.
        /// </summary>
        private void GetAnimal()
        {
            try
            {
                switch (newGame.NumProblemsAnswered)
                {
                    case 1:
                        Feedback2Label.Content = "You helped Crocky find a camel!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\camel.png", UriKind.Relative));
                        break;
                    case 2:
                        Feedback2Label.Content = "You helped Crocky find an elephant!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\elephant.png", UriKind.Relative));
                        break;
                    case 3:
                        Feedback2Label.Content = "You helped Crocky find a giraffe!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\giraffe.png", UriKind.Relative));
                        break;
                    case 4:
                        Feedback2Label.Content = "You helped Crocky find a hippo!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\hippo.png", UriKind.Relative));
                        break;
                    case 5:
                        Feedback2Label.Content = "You helped Crocky find a kangaroo!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\kangaroo.png", UriKind.Relative));
                        break;
                    case 6:
                        Feedback2Label.Content = "You helped Crocky find a lion!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\lion.png", UriKind.Relative));
                        break;
                    case 7:
                        Feedback2Label.Content = "You helped Crocky find a meerkat!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\meerkat.png", UriKind.Relative));
                        break;
                    case 8:
                        Feedback2Label.Content = "You helped Crocky find an ostrich!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\ostrich.png", UriKind.Relative));
                        break;
                    case 9:
                        Feedback2Label.Content = "You helped Crocky find a rhino!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\rhino.png", UriKind.Relative));
                        break;
                    case 10:
                        Feedback2Label.Content = "You helped Crocky find a zebra!";
                        AnimalImage.Source = new BitmapImage(new Uri(@"Images\zebra.png", UriKind.Relative));
                        break;
                    default:
                        FeedbackLabel.Content = "An error occurred when loading animal.";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                      MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will allow the user to press the 'enter' key to
        /// submit answers if their keyboard focus is in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProblemAnswerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    e.Handled = true;
                    SubmitAnswerButton_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                User.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
