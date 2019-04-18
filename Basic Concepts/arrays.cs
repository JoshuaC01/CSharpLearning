using System;

public class arrayExplanation {
  public static void Main(string[] args) {

    //Define array with values
    int[] nums = {1, 2, 3, 4 ,5};

    //Define Empty Arrays, wit a size of 10
    int[] empty = new int[10];

    //Get Length of Array with .Length
    Console.WriteLine($"Length of empty Array = {empty.Length}\n");

    //Change values at a given point
    empty[2] = 3;

    //Get value at given point in array
    Console.WriteLine($"Value at point 2 = {empty[2]}\n");

    //Multidimensional Arrays

    //Create empty 2x2 matrix
    int[,] matrix = new int[2,2];

    matrix[0,0] = 1;

    Console.WriteLine($"Matrix 0,0 = {matrix[0,0]}\n");

  }
}
