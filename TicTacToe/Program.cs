// See https://aka.ms/new-console-template for more information
namespace TicTacToe
{
   class Program
   {

      //main game loop
      static void Main()
      {
         //loop while no one has one or there isn't a draw
         // while(true)
         //in loop clear the console then write the game instructions and header 

         //render the current game board

         //take the users choice from console and add it to the board array

         //after move check if its a win or draw it
         //if not re loop
         GameBoard game = new GameBoard();

         do
         {

            game.PrintGame();
            int inp = game.getUserInput();
            game.UpdateBoard(inp);

         } while (true);
      }

      //create board 
   }
   class GameBoard
   {
      private List<string> gameArray;
      private string p1;
      private string p2;
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

      public void UpdateBoard(int arrayIndex)
      {
         string marker = currentPlayer % 2 == 0 ? "O" : "X";
         gameArray[arrayIndex] = marker;
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

      // public bool CheckWin()
      // {

      // }

   }
}


