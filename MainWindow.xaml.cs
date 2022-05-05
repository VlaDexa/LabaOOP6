using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabaOOP6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly FileStream File = new FileStream("shapes.txt", FileMode.OpenOrCreate);
        readonly List<Shape> Shapes = new List<Shape>();
        readonly static Random Random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            File.SetLength(0);
            StreamWriter sw = new StreamWriter(File);
            foreach (var shape in Shapes)
                shape.Save(sw);
            sw.Close();
            sw.Dispose();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            Drawings.Children.Clear();
            StreamReader sr = new StreamReader(File);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] words = line.Split(' ');
                Shape shape;
                switch (words[0])
                {
                    case "Квадрат":
                        shape = new Square(words);
                        shape.Draw(Drawings);
                        Shapes.Add(shape);
                        break;
                    case "Круг":
                        shape = new Circle(words);
                        shape.Draw(Drawings);
                        Shapes.Add(shape);
                        break;
                    case "Треугольник":
                        shape = new Triangle(words);
                        shape.Draw(Drawings);
                        Shapes.Add(shape);
                        break;
                    default:
                        break;
                }
                line = sr.ReadLine();
            }
            sr.Close();
            sr.Dispose();
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            int shape = Random.Next(3);
            int size = Random.Next(20, 100);
            Point center = new Point(Random.Next((int)Drawings.ActualWidth), Random.Next((int)Drawings.ActualHeight));
            Color color = Color.FromRgb((byte)Random.Next(256), (byte)Random.Next(256), (byte)Random.Next(256));
            Shape shapeType;
            switch (shape)
            {
                case 0:
                    shapeType = new Square(center, color, size);
                    break;
                case 1:
                    shapeType = new Circle(center, color, size);
                    break;
                case 2:
                    shapeType = new Triangle(center, color, size);
                    break;
                default:
                    throw new NotImplementedException();
            }
            shapeType.Draw(Drawings);
            Shapes.Add(shapeType);
        }
    }
}
