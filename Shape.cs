using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace LabaOOP6
{
    abstract class Shape
    {
        protected Point Center;
        protected Color Color;
        protected double Size;
        protected string Name;

        public abstract double Area();

        public abstract System.Windows.Shapes.Shape Draw(Canvas canvas);

        public abstract bool IsInside(Point point);

        public virtual void ShowInfo()
        {
            MessageBox.Show($"{Name} с центром в точке ({Center.X} , {Center.Y}). Площадь: {Area()}");
        }

        public void Click(object sender, RoutedEventArgs e)
        {
            ShowInfo();
        }

        public void Save(StreamWriter sb)
        {
            sb.WriteLine($"{Name} {Color.R} {Color.G} {Color.B} {Size} {Center.X} {Center.Y}");
        }
    }
    class Square: Shape
    {
            
        public Square(Point center, Color color, double size)
        {
            Center = center;
            Color = color;
            Size = size;
            Name = "Квадрат";
        }

        public Square(string[] vs)
        {
            Color = Color.FromRgb(byte.Parse(vs[1]), byte.Parse(vs[2]), byte.Parse(vs[3]));
            Size = double.Parse(vs[4]);
            Center = new Point(double.Parse(vs[5]), double.Parse(vs[6]));
            Name = "Квадрат";
        }

        public override double Area()
        {
            return Size * Size;
        }

        public override System.Windows.Shapes.Shape Draw(Canvas canvas)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Width = Size,
                Height = Size,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 1,
                Fill = new SolidColorBrush(Color),
            };
            rect.MouseLeftButtonUp += Click;
            Canvas.SetLeft(rect, Center.X - Size / 2);
            Canvas.SetTop(rect, Center.Y - Size / 2);
            canvas.Children.Add(rect);
            return rect;
        }

        public override bool IsInside(Point point)
        {
            return (point.X >= Center.X - Size / 2 && point.X <= Center.X + Size / 2) &&
                   (point.Y >= Center.Y - Size / 2 && point.Y <= Center.Y + Size / 2);
        }
    }

    class Circle: Shape
    {
        public Circle(Point center, Color color, double size)
        {
            Center = center;
            Color = color;
            Size = size;
            Name = "Круг";
        }

        public Circle(string[] vs)
        {
            Color = Color.FromRgb(byte.Parse(vs[1]), byte.Parse(vs[2]), byte.Parse(vs[3]));
            Size = double.Parse(vs[4]);
            Center = new Point(double.Parse(vs[5]), double.Parse(vs[6]));
            Name = "Круг";
        }

        public override double Area()
        {
            return Math.PI * Size * Size;
        }

        public override System.Windows.Shapes.Shape Draw(Canvas canvas)
        {
            var ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = Size,
                Height = Size,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 1, 
                Fill = new SolidColorBrush(Color),
            };
            ellipse.MouseLeftButtonUp += Click;
            Canvas.SetLeft(ellipse, Center.X - Size / 2);
            Canvas.SetTop(ellipse, Center.Y - Size / 2);
            canvas.Children.Add(ellipse);
            return ellipse;
        }
        
        public override bool IsInside(Point point)
        {
            return (point.X - Center.X) * (point.X - Center.X) + (point.Y - Center.Y) * (point.Y - Center.Y) <= Size * Size;
        }
    }

    class Triangle: Shape
    {
        public Triangle(Point center, Color color, double size)
        {
            Center = center;
            Color = color;
            Size = size;
            Name = "Треугольник";
        }

        public Triangle(string[] vs)
        {
            Color = Color.FromRgb(byte.Parse(vs[1]), byte.Parse(vs[2]), byte.Parse(vs[3]));
            Size = double.Parse(vs[4]);
            Center = new Point(double.Parse(vs[5]), double.Parse(vs[6]));
            Name = "Треугольник";
        }

        public override double Area()
        {
            return Size * Size * Math.Sqrt(3) / 4;
        }

        public override System.Windows.Shapes.Shape Draw(Canvas canvas)
        {
            var polygon = new System.Windows.Shapes.Polygon
            {
                Points = new PointCollection
                {
                    new Point(0, - Size / 2),
                    new Point(Size / 2, Size / 2),
                    new Point(- Size / 2, Size / 2)
                },
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = 1,
                Fill = new SolidColorBrush(Color),
            };
            polygon.MouseLeftButtonUp += Click;
            Canvas.SetLeft(polygon, Center.X - Size / 2);
            Canvas.SetTop(polygon, Center.Y - Size / 2);            
            canvas.Children.Add(polygon);
            return polygon; 
        }

        public override bool IsInside(Point point)
        {
            return (point.X >= Center.X - Size / 2 && point.X <= Center.X + Size / 2) &&
                   (point.Y >= Center.Y - Size / 2 && point.Y <= Center.Y + Size / 2);
        }
    }
}
