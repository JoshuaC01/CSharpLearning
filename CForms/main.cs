using System;
using System.Drawing;
using System.Windows.Forms;


  public class CForms : Form {

    private Button button;

    public CForms() {
      DisplayGUI();
    }

    private void DisplayGUI() {
      this.Name = "C# Form Example";
      this.Text = "C# Form Example";
      this.Size = new Size(600, 600);
      this.StartPosition = FormStartPosition.CenterScreen;

      button = new Button();
      button.Name = "Button";
      button.Text = "Click";
      button.Size = new Size(this.Width - 50, this.Height - 50);
      button.Location = new Point((this.Width - button.Width) / 3, (this.Height - button.Height) / 3);

      button.Click += new System.EventHandler(this.MyButtonClick);

      this.Controls.Add(button);
    }

    private void MyButtonClick(object source, EventArgs e) {
      MessageBox.Show("Button Clicked!");
    }

    public static void Main(String[] args) {
      Application.Run(new CForms());
    }

  }
