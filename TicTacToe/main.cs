using System;

using System.Collections.Generic;

class TicTacToe {

  public static void Main(string[] args) {
    int boardSize = 3;

    Board board = new Board(boardSize, boardSize);
    string currentTurn = "X";
    bool finished = false;

    while (!finished) {
      Console.WriteLine("Current Turn: " + currentTurn);
      board.display();

      int[] coords = getCoords();
      int x = coords[0];
      int y = coords[1];

      Console.WriteLine($"Placing At: {x}, {y}");

      bool placed = board.place(x, y, currentTurn);

      while(!placed){
        coords = getCoords();
        x = coords[0];
        y = coords[1];

        Console.WriteLine($"Placing At: {x}, {y}");
        placed = board.place(x, y, currentTurn);

      }

      currentTurn = currentTurn == "X" ? "O" : "X";
    }
  }

  public static int[] getCoords() {
    Console.Write("Place Piece at: ");
    string[] input = Console.ReadLine().Split(' ');

    while(input.Length != 2) {
      Console.WriteLine("Enter Coords seperated by a space");
      Console.Write("Place Piece at: ");
      input = Console.ReadLine().Split(' ');
    }

    int y = Int32.Parse(input[0]);
    int x = Int32.Parse(input[1]);

    return new int[] {x, y};
  }

}
