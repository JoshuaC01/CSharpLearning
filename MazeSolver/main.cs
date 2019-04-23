using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class point {
  public int x, y, value;
  public bool isJunc = false;
  public juncPoint junction = null;

  public point() {
    this.x = 0;
    this.y = 0;
    this.value = -1;
  }

  public point(int x, int y, int value) {
    this.x = x;
    this.y = y;
    this.value = value;
  }

  public override string ToString()
  {
    return $"X: {this.x}, Y: {this.y}";
  }
}

public class juncPoint {
  public point origin;
  public bool up, down, left, right;
  public int distToStart, distToEnd, totalDist;
  public List<point> cameFrom;
  public juncPoint nUp, nDown, nLeft, nRight;

  public juncPoint() {
    this.up = this.down = this.left = this.right = false;
    this.distToStart = this.distToEnd = this.totalDist = -1;
  }

  public juncPoint(point origin, bool up, bool down, bool left, bool right, point startPoint, point endPoint) {
    this.origin = origin;
    this.origin.junction = this;
    this.origin.isJunc = true;

    this.up = up;
    this.down = down;
    this.left = left;
    this.right = right;

    this.distToEnd = (int) (Math.Pow(endPoint.x - origin.x, 2) + Math.Pow(endPoint.y - origin.y, 2));
    this.totalDist = int.MaxValue;
  }

  public override string ToString() {
    String output = $"Origin({this.origin})";

    output += this.up ? $", Up" : "";
    output += this.down ? $", Down" : "";
    output += this.left ? $", Left" : "";
    output += this.right ? $", Right" : "";

    return output;
  }

}

public class MazeSolver : Form {

  private PictureBox displayBox;
  private Bitmap canvas;

  private Color startColor = Color.FromArgb(0, 255, 0);
  private Color endColor = Color.FromArgb(0, 0, 255);
  private Color junctionColor = Color.FromArgb(255, 0, 0);

  public static Color lerpColors(Color startColor, Color endColor, float t) {
    float red = startColor.R * (1f - t) + endColor.R * t;
    float green = startColor.G * (1f - t) + endColor.G * t;
    float blue = startColor.B * (1f - t) + endColor.B * t;

    return Color.FromArgb((int) red, (int) green, (int) blue);
  }

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

  public void DisplayGUI() {
    this.Name = "Maze Solver GUI";
    this.Text = "Maze Solver GUI";
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

  public Tuple<point, point, point[,]> importImage(string imageName) {
    Bitmap mazeImg = new Bitmap(imageName);

    point[,] mazeArray = new point[mazeImg.Width, mazeImg.Height];

    point startPoint, endPoint;
    startPoint = endPoint = null;

    for(int x = 0; x < mazeImg.Width; x++) {
      for(int y = 0; y < mazeImg.Height; y++) {
        Color pixel = mazeImg.GetPixel(x, y);
        if(pixel.R == 0 && pixel.G == 0 && pixel.B == 0) {
          mazeArray[x, y] = new point(x, y, 0);
        } else {
          mazeArray[x, y] = new point(x, y, 1);
        }

        if(y == 0 && pixel.R == 255 && pixel.G == 255 && pixel.B == 255)  {
          startPoint = mazeArray[x, y];
        } else if(y == mazeImg.Height - 1 && pixel.R == 255 && pixel.G == 255 && pixel.B == 255)  {
          endPoint = mazeArray[x, y];
        }
      }
    }

    //Print2DArray(mazeArray);
    //displayBox.Image = mazeImg;

    return Tuple.Create(startPoint, endPoint, mazeArray);
  }

  public List<juncPoint> findJunctions(point[,] mazeArray, point startPoint, point endPoint) {

    List<juncPoint> junctions = new List<juncPoint>();

    for(int x = 0; x < mazeArray.GetLength(0); x++) {
      for(int y = 0; y < mazeArray.GetLength(1); y++) {
        if(x == 0 || x == mazeArray.GetLength(0) - 1 || y == 0 || y == mazeArray.GetLength(1) - 1) {
          continue;
        }

        if(mazeArray[x, y].value == 0) {
          continue;
        }

        bool up, down, left, right;
        up = down = left = right = false;

        up = mazeArray[x, y - 1].value == 1;
        down = mazeArray[x, y + 1].value == 1;
        left = mazeArray[x - 1, y].value == 1;
        right = mazeArray[x + 1, y].value == 1;

        if((up && left) || (up && right) || (down && left) || (down && right)) {
          junctions.Add(new juncPoint(mazeArray[x, y], up, down, left, right, startPoint, endPoint));
        }

      }
    }

    Console.WriteLine("Found Junctions: ");
    junctions.ForEach(Console.WriteLine);
    Console.WriteLine("Number of Junctions: " + junctions.Count);

    return junctions;
  }

  public void drawMaze(point startPoint, point endPoint, point[,] mazeArray) {
    int width = mazeArray.GetLength(0);
    int height = mazeArray.GetLength(1);

    Console.WriteLine("Maze Size: " + width + ", " + height);

    int boxScale = (int) canvas.Width / width;

    Console.WriteLine("Box Scale: " + boxScale);

    Graphics g = Graphics.FromImage(canvas);
    g.Clear(Color.White);

    for(int x = 0; x < width; x++) {
      for(int y = 0; y < height; y++) {
        if(x == startPoint.x && y == startPoint.y) {
          g.FillRectangle(new SolidBrush(startColor), new Rectangle(x * boxScale, y * boxScale, boxScale, boxScale));
          continue;
        }

        if(x == endPoint.x && y == endPoint.y) {
          g.FillRectangle(new SolidBrush(endColor), new Rectangle(x * boxScale, y * boxScale, boxScale, boxScale));
          continue;
        }

        Color brushColor = Color.White; //lerpColors(startColor, endColor, (y + 1) / (float) height);

        if(mazeArray[x, y].value == 1) {
          g.FillRectangle(new SolidBrush(brushColor), new Rectangle(x * boxScale, y * boxScale, boxScale, boxScale));
        }

        if(mazeArray[x, y].value == 0) {
          g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x * boxScale, y * boxScale, boxScale, boxScale));
          continue;
        }
      }
    }

    List<juncPoint> junctions = findJunctions(mazeArray, startPoint, endPoint);

    foreach(juncPoint junc in junctions) {
      g.FillRectangle(new SolidBrush(junctionColor), new Rectangle(junc.origin.x * boxScale, junc.origin.y * boxScale, boxScale, boxScale));
    }
    updateCanvas();
    g.Dispose();

    canvas.Save("NEW - maze.png");

    List<juncPoint> path =  aStar(junctions, startPoint, endPoint);

    Console.WriteLine("Found Path: ");
    path.ForEach(Console.WriteLine);
  }

  public juncPoint findNeighbours(juncPoint currentPoint, List<juncPoint> allPoints, point[,] mazeArray, point startPoint, point endPoint) {
    if(currentPoint.up) {
      int currentY = currentPoint.origin.y - 1;
      point currentFound = mazeArray[currentPoint.origin.x, currentY];
      while(!currentPoint.isJunc) {
        currentY--;
        currentFound = mazeArray[currentPoint.origin.x, currentY];
        if(currentFound.value == 0) {
          currentFound = null;
          break;
        }
      }

      currentPoint.nUp = currentFound.junction;
    }

    if(currentPoint.down) {
      int currentY = currentPoint.origin.y + 1;
      point currentFound = mazeArray[currentPoint.origin.x, currentY];
      while(!currentPoint.isJunc) {
        currentY++;
        currentFound = mazeArray[currentPoint.origin.x, currentY];
        if(currentFound.value == 0) {
          currentFound = null;
          break;
        }
      }

      currentPoint.nDown = currentFound.junction;
    }

    if(currentPoint.left) {
      int currentX = currentPoint.origin.x - 1;
      point currentFound = mazeArray[currentX, currentPoint.origin.y];
      while(!currentPoint.isJunc) {
        currentX--;
        currentFound = mazeArray[currentX, currentPoint.origin.y];
        if(currentFound.value == 0) {
          currentFound = null;
          break;
        }
      }

      currentPoint.nLeft = currentFound.junction;
    }

    if(currentPoint.right) {
      int currentX = currentPoint.origin.x + 1;
      point currentFound = mazeArray[currentX, currentPoint.origin.y];
      while(!currentPoint.isJunc) {
        currentX++;
        currentFound = mazeArray[currentX, currentPoint.origin.y];
        if(currentFound.value == 0) {
          currentFound = null;
          break;
        }
      }

      currentPoint.nRight = currentFound.junction;
    }

    return currentPoint;
  }

  public List<juncPoint> aStar(List<juncPoint> allJunctions, point startPoint, point endPoint) {
    Console.WriteLine("Starting A* Solver");
    List<juncPoint> currentPath = new List<juncPoint>();

    List<juncPoint> openPoints = new List<juncPoint>();
    List<juncPoint> closedPoints = new List<juncPoint>();

    juncPoint juncStartPoint = new juncPoint(startPoint, false, false, false ,false, startPoint, endPoint);

    juncStartPoint.distToStart = 0;

    juncPoint juncEndPoint = new juncPoint(endPoint, false, false, false ,false, startPoint, endPoint);

    openPoints.Add(juncStartPoint);

    while(openPoints.Count > 0) {
      openPoints.Sort((a, b) => a.totalDist.CompareTo(b.totalDist));

      juncPoint current = openPoints[0];

      if(current == juncEndPoint) {
        break;
      }

      openPoints.Remove(current);
      closedPoints.Add(current);
    }

    return currentPath;
  }

  public MazeSolver() {
    DisplayGUI();

    var imageData = importImage("maze.png");

    point[,] mazeArray = imageData.Item3;
    drawMaze(imageData.Item1, imageData.Item2, mazeArray);
  }

  public static void Main(String[] args) {
    Application.Run(new MazeSolver());
  }

}
