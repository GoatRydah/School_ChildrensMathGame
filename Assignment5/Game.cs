using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    /// <summary>
    /// The game class holds the business logic behind the gameplay of the game
    /// throughout the user's experience.
    /// </summary>
    public class Game
    {

        #region Game Attributes

        /// <summary>
        ///  This attribute keeps track of which type of game the user chose to play.
        /// </summary>
        private string gameType;

        /// <summary>
        ///  This attribute keeps track of the score as the game goes on.
        /// </summary>
        private int score;

        /// <summary>
        /// This attribute is used to store the percentage the student scored at the end of
        /// a game. It will be passed on.
        /// </summary>
        private double percentage;

        /// <summary>
        /// This attribute keeps track of the total number of questions answered.
        /// </summary>
        private int numProblemsAnswered;

        /// <summary>
        /// This attribute will be used at the end to conver the percentage into a string.
        /// </summary>
        private string sPercentage;

        /// <summary>
        /// This attribute is used to generate random numbers throughout gameplay.
        /// </summary>
        Random rand = new Random();

        /// <summary>
        /// This attribute is used to keep track of the left operand for each 
        /// current problem.
        /// </summary>
        private int leftOperand;

        /// <summary>
        /// This attribute is used to keep track of the right operand for each
        /// current problem.
        /// </summary>
        private int rightOperand;

        /// <summary>
        /// This attribute will be combined with .ToString() version of the 
        /// left and right operands to be passed to the game window for
        /// displaying to the user.
        /// </summary>
        private string Operator;

        #endregion

        #region Getters/Setters

        /// <summary>
        /// Getter Setter for gameType to keep track of which type of game the
        /// user wants to play
        /// </summary>
        public string GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }

        /// <summary>
        /// Getter Setter for user's current score as the game goes on.
        /// </summary>
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// Getter Setter for how many problems the user has answered.
        /// </summary>
        public int NumProblemsAnswered
        {
            get { return numProblemsAnswered; }
            set { numProblemsAnswered = value; }
        }

        /// <summary>
        /// Getter Setter for what percentage user got in their game.
        /// </summary>
        public string SPercentage
        {
            get { return sPercentage; }
            set { sPercentage = value; }
        }

        /// <summary>
        /// Getter Setter for the current left operand in the game.
        /// </summary>
        public int LeftOperand
        {
            get { return leftOperand; }
            set { leftOperand = value; }
        }

        /// <summary>
        /// Getter Setter for the current right operand in the game.
        /// </summary>
        public int RightOperand
        {
            get { return rightOperand; }
            set { rightOperand = value; }
        }

        /// <summary>
        /// Getter Setter for the current operator used in the game (+, -, *, /).
        /// </summary>
        public string currOperator
        {
            get { return Operator; }
            set { Operator = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Basic constructor that sets up a Game object.
        /// </summary>
        public Game()
        {
            score = 0;
            percentage = 0;
            numProblemsAnswered = 0;
            sPercentage = "0%";
            gameType = "";
            leftOperand = 0;
            rightOperand = 0;
            Operator = "";
        }

        #endregion

        #region Methods / Game Logic

        /// <summary>
        /// This will be called after a game is complete to set the final scores for the game.
        /// </summary>
        public void SetFinalScores(string time)
        {
            try
            {
                percentage = ((double)score / numProblemsAnswered) * 100;
                sPercentage = percentage.ToString("0.#") + "%";
                User.EndTime = time + " seconds";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                    MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will set up and get the left and right operands and stores them.
        /// It sets the correct operand as well.
        /// </summary>
        public void SetUpProblem()
        {
            try
            {
                switch (gameType)
                {
                    case "Addition":
                        leftOperand = GetRandomNumber();
                        rightOperand = GetRandomNumber();
                        Operator = " + ";
                        break;
                    case "Subtraction":
                        leftOperand = GetRandomNumber();
                        rightOperand = GetRightOperandForSubtraction();
                        Operator = " - ";
                        break;
                    case "Multiplication":
                        leftOperand = GetRandomNumber();
                        rightOperand = GetRandomNumber();
                        Operator = " X ";
                        break;
                    case "Division":
                        double lOp = GetLeftDivisionOperand();
                        Int32.TryParse(lOp.ToString(), out leftOperand);
                        double rOp = GetRightDivisonOperand();
                        Int32.TryParse(rOp.ToString(), out rightOperand);
                        Operator = " ÷ ";
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
        /// This method will be used frequently to get the next numbers for each problem.
        /// It will be called in the UI classes and the values will be used there to display
        /// the numbers. They will then be passed in to the add/subtract/mult/divide methods
        /// in this class.
        /// </summary>
        /// <returns>A random number between 1 and 10</returns>
        public int GetRandomNumber()
        {
            try
            {
                return rand.Next(1, 11);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will get a random number for the right operand for subtraction. It will
        /// ensure that the right operand is less than or equal to the left operand so that the
        /// student will never need to enter a negative number.
        /// </summary>
        /// <param name="lOperand"></param>
        /// <returns>The right operand for Subtraction.</returns>
        public int GetRightOperandForSubtraction()
        {
            try
            {
                int rOperand = rand.Next(1, 7);

                if (rOperand > leftOperand)
                {
                    return GetRightOperandForSubtraction();
                }
                return rOperand;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// Will get the left operand for divison. Won't allow the value to be less than 2.
        /// </summary>
        /// <returns>The left operand as a double for divison.</returns>
        public double GetLeftDivisionOperand()
        {
            try
            {
                return rand.Next(1, 11);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will get a random number for the right operand for division. It will
        /// ensure that the right operandis cleanly divisible by the left operand so that the
        /// student will never have an answer that's not a whole number.
        /// </summary>
        /// <param name="lOperand"></param>
        /// <returns></returns>
        public double GetRightDivisonOperand()
        {
            try
            {
                double value = rand.Next(1, 11);

                if (leftOperand % value != 0)
                {
                    return GetRightDivisonOperand();
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will check to see if the user's answer is correct with addition. It 
        /// will be called 10 times per game within the UI classes.
        /// </summary>
        /// <param name="leftNum">Left number to be added.</param>
        /// <param name="rightNum">Right number to be added.</param>
        /// <param name="answer">User's answer.</param>
        /// <returns></returns>
        public bool AddNumberPair(string answer)
        {
            try
            {
                int convertedAnswer = 0;

                if (!Int32.TryParse(answer, out convertedAnswer))
                    return false;

                numProblemsAnswered++;

                if (leftOperand + rightOperand == convertedAnswer)
                {
                    score++;
                    User.WhichCorrect[numProblemsAnswered - 1] = true;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will check to see if the user's answer is correct with subtraction. It 
        /// will be called 10 times per game within the UI classes.
        /// </summary>
        /// <param name="leftNum">Left number to be added.</param>
        /// <param name="rightNum">Right number to be added.</param>
        /// <param name="answer">User's answer.</param>
        /// <returns></returns>
        public bool SubtractNumberPair(string answer)
        {
            try
            {
                int convertedAnswer = 0;

                if (!Int32.TryParse(answer, out convertedAnswer))
                    return false;

                numProblemsAnswered++;

                if (leftOperand - rightOperand == convertedAnswer)
                {
                    score++;
                    User.WhichCorrect[numProblemsAnswered - 1] = true;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will check to see if the user's answer is correct with multiplication. It 
        /// will be called 10 times per game within the UI classes.
        /// </summary>
        /// <param name="leftNum">Left number to be added.</param>
        /// <param name="rightNum">Right number to be added.</param>
        /// <param name="answer">User's answer.</param>
        /// <returns></returns>
        public bool MultiplyNumberPair(string answer)
        {
            try
            {
                int convertedAnswer = 0;

                if (!Int32.TryParse(answer, out convertedAnswer))
                    return false;

                numProblemsAnswered++;

                if (leftOperand * rightOperand == convertedAnswer)
                {
                    score++;
                    User.WhichCorrect[numProblemsAnswered - 1] = true;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                   MethodInfo.GetCurrentMethod().Name + "." + ex.Message);
            }
        }

        /// <summary>
        /// This method will check to see if the user's answer is correct with addition. It 
        /// will be called 10 times per game within the UI classes.
        /// </summary>
        /// <param name="leftNum">Left number to be added.</param>
        /// <param name="rightNum">Right number to be added.</param>
        /// <param name="answer">User's answer.</param>
        /// <returns></returns>
        public bool DivideNumberPair(string answer)
        {
            try
            {
                double convertedAnswer = 0;

                if (!Double.TryParse(answer, out convertedAnswer))
                    return false;

                numProblemsAnswered++;

                if ((double)leftOperand / rightOperand == convertedAnswer)
                {
                    score++;
                    User.WhichCorrect[numProblemsAnswered - 1] = true;
                    return true;
                }
                else
                    return false;
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
