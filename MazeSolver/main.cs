using System;
using System.Collections.Generic;
using System.Drawing;

public class MazeSolver {

  public static void Print2DArray<T>(T[,] matrix)
  {
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
          for (int j = 0; j < matrix.GetLength(1); j++)
          {
              Console.Write(matrix[i,j] + "\t");
          }
          Console.WriteLine();
      }
  }

  public static void Main(String[] args) {
    Bitmap mazeImg = new Bitmap("maze.png");

    int[,] mazeArray = new int[mazeImg.Width, mazeImg.Height];

    for(int x = 0; x < mazeImg.Width; x++) {
      for(int y = 0; y < mazeImg.Height; y++) {
        Color pixel = mazeImg.GetPixel(x, y);

        if(pixel.R == 0 && pixel.G == 0 && pixel.B == 0) {
          mazeArray[x, y] = 0;
        } else {
          mazeArray[x, y] = 1;
        }

      }
    }

    Print2DArray(mazeArray);

    mazeImg.Save("NewMaze.png");

  }

}
