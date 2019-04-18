using System;

//Everything in C# is an object made from a class
class myClass {
  //Public vars are accesible by class reference
  public string Text;
  public float MultVal;

  public float MultiplyByValue(float input) {
    return input * MultVal;
  }

  //Use a constructor to initialize an object with custom values
  public myClass(string text, float multVal) {
    Text = text;
    MultVal = multVal;
  }

}

class MainClass {
  public static void Main(string[] args) {
    //New object of class type myClass with a value of a new myClass instance
    myClass myObject = new myClass("New String by Constructor", 3);

    Console.WriteLine(myObject.Text);
    Console.WriteLine(myObject.MultiplyByValue(3.5f));
  }
}
