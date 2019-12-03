using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    /// <summary>
    /// The User class is here to keep track of the current user's information and
    /// can be used globaly since it's a static class.
    /// </summary>
    public static class User
    {
        #region User Attributes

        /// <summary>
        /// This attribute keeps track of the user's name throughout the duration
        /// of the program.
        /// </summary>
        private static string username;

        /// <summary>
        /// This attribute keeps track of the user's age throughout the duration
        /// of the program.
        /// </summary>
        private static int age;

        /// <summary>
        /// This attribute keeps trac of the user's choice in type of game to 
        /// play. When they select a type of game to play, this string is 
        /// updated.
        /// </summary>
        private static string usersLatestChoice;

        /// <summary>
        /// This attribute keeps track of the user's score when a game is
        /// complete
        /// </summary>
        private static string score;

        /// <summary>
        /// This boolean keeps track of if the user has set their username
        /// and age or not.
        /// </summary>
        private static bool set = false;

        /// <summary>
        /// This string will be set to display the users total game time
        /// at the end of the game.
        /// </summary>
        private static string endTime;

        /// <summary>
        /// This array keeps track of which questions were answered 
        /// correctly throughout each game.
        /// </summary>
        private static bool[] whichCorrect = new bool[10];

        #endregion

        #region Getters/Setters

        /// <summary>
        /// Getter Setter for user's username.
        /// </summary>
        public static string UserName
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Getter Setter for user's age.
        /// </summary>
        public static int Age
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// Getter Setter for user's most recent choice in game type to play.
        /// </summary>
        public static string UsersLatestChoice
        {
            get { return usersLatestChoice; }
            set { usersLatestChoice = value; }
        }

        /// <summary>
        /// Getter Setter for user's score at the end of the game.
        /// </summary>
        public static string Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// Getter Setter for if user has set their name and age.
        /// </summary>
        public static bool Set
        {
            get { return set; }
            set { set = value; }
        }

        /// <summary>
        /// Getter Setter for the time at the end of the game.
        /// </summary>
        public static string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// Getter Setter for the array keeping track of which anwers are correct.
        /// </summary>
        public static bool[] WhichCorrect
        {
            get { return whichCorrect; }
            set { whichCorrect = value; }
        }

        #endregion

        #region Exception Handler

        /// <summary>
        /// This method is used globaly for top level methods for
        /// exception handling.
        /// </summary>
        /// <param name="Class">The Class where the exception ocurred.</param>
        /// <param name="Method">The Method where the exception ocurred.</param>
        /// <param name="Message">The Exception Message itself.</param>
        public static void HandleError(string Class, string Method, string Message)
        {
            try
            {
                System.Windows.MessageBox.Show($"Error Found: Class.Method.Message = {Class}.{Method}.{Message}");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method will be called when a new game is started and will set the array
        /// to false so that all questions are considered unanswered.
        /// </summary>
        public static void SetArrayToFalse()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    User.whichCorrect[i] = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        #endregion
    }
}
