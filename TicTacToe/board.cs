using System;

class Board {

  string[,] board;
  int gapScale = 1;

  public Board(int xSize, int ySize) {
    board = new string[xSize, ySize];
    reset();
  }

  public void reset() {
    for(int x = 0; x < board.GetLength(0); x++) {
      for(int y = 0; y < board.GetLength(1); y++) {
        board[x, y] = "-";
      }
    }
  }

  public void display() {
    for(int row = 0; row < board.GetLength(0); row++) {
      string rowStr = "";

      for(int col = 0; col < board.GetLength(1); col++) {
        rowStr += board[row, col];
        if(col != board.GetLength(1) - 1) {
          for(int i = 0; i < gapScale; i++) {
            rowStr += " ";
          }
        }
      }

      Console.WriteLine(rowStr);
      for(int i = 1; i < gapScale; i++) {
        Console.WriteLine();
      }
    }
  }

  public bool place(int x, int y, string piece) {
    if(board[x, y] == "-") {
      board[x, y] = piece;
      return true;
    } else {
      return false;
    }
  }

}
