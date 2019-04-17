using System;

public class HelloWorld {

  public static void Main(string[] args)
  {
    Console.WriteLine("What is your name?");

    string name = Console.ReadLine();
    DateTime date = DateTime.Now;

    Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}!");
    Console.Write("\nPress any key to continue...\n");
    Console.ReadKey(true);
  }

}
