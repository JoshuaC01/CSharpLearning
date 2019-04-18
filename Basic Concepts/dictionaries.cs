using System;

//Must be used for Dictorionaries
using System.Collections.Generic;

public class dictExplanation {

  public static void Main(string[] args) {

    //Dictionaries hold a key and a value of a given type respectively
    Dictionary<string, long> phonebook = new Dictionary<string, long>();

    //Two different ways of defining a key & value
    phonebook.Add("Alex", 0000111222);
    phonebook["Jess"] = 1112223334;

    //Check if dict contains a key
    if(phonebook.ContainsKey("Alex")) {
      Console.WriteLine("Alex's Number is " + phonebook["Alex"]);
    }

    //Remove a key from dict
    phonebook.Remove("Jess");

  }

}
