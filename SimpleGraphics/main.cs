using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class SimpleGraphics : Form {
  private PictureBox displayBox;
  private Bitmap canvas;

  public void DisplayGUI() {
    this.Name = "Simple Graphics Example";
    this.Size = new Size(600, 600);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.BackColor = Color.FromArgb(200, 200, 200);

    displayBox = new PictureBox();
    displayBox.ClientSize = new Size(500, 500);
    displayBox.SizeMode = PictureBoxSizeMode.StretchImage;

    canvas = new Bitmap(displayBox.Width, displayBox.Height);

    updateCanvas();

    this.Controls.Add(displayBox);
  }

  public void updateCanvas() {
    displayBox.Image = canvas;
  }

  public SimpleGraphics() {
    DisplayGUI();

    Graphics g = Graphics.FromImage(canvas);
    g.Clear(Color.White);

    g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(100, 100, 100, 100));

    g.Dispose();
    updateCanvas();
  }

  public static void Main(string[] args) {
    Application.Run(new SimpleGraphics());
  }
}
