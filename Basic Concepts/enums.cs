using System;

//Select integers from list of options
public enum CarType
{
  Toyota = 1,
  Honda = 2,
  Ford = 3,
}

public class EnumExplanation {

  public static void Main(string[] args)
  {
    Console.WriteLine($"Car type of Toyota: {CarType.Toyota}");
  }

}
