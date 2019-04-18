using System;

//Must be included for lists
using System.Collections.Generic;

public class ListExplanation {
  public static void Main(string[] args) {

    //List are very similar to java, hold variables in order

    List<int> nums = new List<int>();
    nums.Add(1);
    nums.Add(2);
    nums.Add(3);

    //Add whole array of values to list
    int[] array = new int[] {4, 5, 6, 7};
    nums.AddRange(array);

    //Removing from Arrays

    //Remove a given value
    nums.Remove(5); //Removes the number 5, not the number at position 5

    //Remove at a position
    nums.RemoveAt(0); //Removes first item


    //Add lists together
    List<int> moreNums = new List<int>();
    moreNums.Add(10);
    moreNums.Add(11);
    moreNums.Add(12);

    nums.AddRange(moreNums);

  }
}
