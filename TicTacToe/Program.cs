// See https://aka.ms/new-console-template for more information
namespace TicTacToe
{
   class Program
   {

      //main game loop
      static void Main()
      {




         GameBoard game = new GameBoard();

         //loop while no one has one or there isn't a draw
         do
         {

            game.PrintGame();
            int inp = game.getUserInput();
            //take the users choice from console and add it to the board array
            //render the current game board
            game.UpdateBoard(inp);

            //after move check if its a win or draw it
         } while (game.CheckWin());
         //if not re loop
      }

      //create board 
   }
   class GameBoard
   {
      private List<string> gameArray;
      private string p1;
      private string p2;
      private Dictionary<string, List<int>> player1Tracking;

      private Dictionary<string, List<int>> player2Tracking;

      private int currentPlayer;
      private string currentPlayerName;

      public GameBoard()
      {
         gameArray = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
         p1 = "Player 1";
         p2 = "Player 2";
         currentPlayer = 1;
         currentPlayerName = "Player 1";

      }
      public void PrintGame()
      {
         Console.Clear();
         Console.WriteLine($"     {gameArray?[0]}  |  {gameArray?[1]}  |  {gameArray?[2]} ");
         Console.WriteLine($"   ------------------");
         Console.WriteLine($"     {gameArray?[3]}  |  {gameArray?[4]}  |  {gameArray?[5]} ");
         Console.WriteLine($"   ------------------");
         Console.WriteLine($"     {gameArray?[6]}  |  {gameArray?[7]}  |  {gameArray?[8]} ");
      }
      //returns index to change
      public int getUserInput()
      {
         if (currentPlayer % 2 == 0)
         {
            currentPlayerName = p2;
         }
         else
         {
            currentPlayerName = p1;
         }
         Console.WriteLine($"{currentPlayerName}:Enter a number 1-9 to fill that space ");
         string userChoice = Console.ReadLine() ?? "";
         while (!IsValid(userChoice))
         {
            Console.WriteLine($"Invalid Entry");
            Console.WriteLine($"Enter a number 1-9 to fill that space");
            userChoice = Console.ReadLine() ?? "";
         }
         return Int32.Parse(userChoice) - 1;
      }
      //take in the users input and add an letter to the proper persons dictinoary

      private void WinHelper(int arrayIndex, string symbol)
      {
         // player1Tracking.Add()

         //add to the appropriate keys based on the index 
         // D1 =[0, 4, 8];
         // D2 =[2, 4, 8];
         Dictionary<string, List<int>> curr = (currentPlayer % 2 == 1 ? player1Tracking : player2Tracking);
         string column = "C" + (arrayIndex % 3).ToString();
         string row = "R" + (arrayIndex / 3).ToString();

         if (!curr.ContainsKey(column))
         {
            curr.Add(column, new List<int>() { arrayIndex });
         }
         else
         {
            curr[column].Add(arrayIndex);
         }

         if (!curr.ContainsKey(row))
         {
            curr.Add(row, new List<int>() { arrayIndex });
         }
         else
         {
            curr[row].Add(arrayIndex);
         }

         if (arrayIndex == 0 || arrayIndex == 4 || arrayIndex == 8)
         {
            if (!curr.ContainsKey("D1"))
            {
               curr.Add("D1", new List<int>() { arrayIndex });
            }
            else
            {
               curr["D1"].Add(arrayIndex);
            }
         }
         if (arrayIndex == 0 || arrayIndex == 4 || arrayIndex == 8)
         {
            if (!curr.ContainsKey("D2"))
            {
               curr.Add("D2", new List<int>() { arrayIndex });
            }
            else
            {
               curr["D2"].Add(arrayIndex);
            }
         }

      }

      public void UpdateBoard(int arrayIndex)
      {
         string marker = currentPlayer % 2 == 0 ? "O" : "X";
         gameArray[arrayIndex] = marker;
         //checkwin on the index
         currentPlayer++;
      }

      public bool IsValid(string checkString)
      {
         if (!gameArray.Contains(checkString))
         {
            Console.WriteLine($"Enter a Valid space ");
            return false;
         }

         return true;
      }

      public bool CheckWin()
      {
         Dictionary<string, List<int>> curr = (currentPlayer % 2 == 1 ? player1Tracking : player2Tracking);


         foreach (KeyValuePair<string, List<int>> winCondition in curr)
         {
            int length = winCondition.Value?.Count ?? 0;
            if (winCondition.Value?.Count == 3)
            {
               return true;
            }
         }

         return false;
      }

   }
}


