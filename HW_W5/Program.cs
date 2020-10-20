using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_W5
{
    //delegate
    delegate double doubletodouble(double a, double b);
    internal class Box:IComparable<Box>
    {
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Height { get; set; }
        public string Name { set; get; }
        public Box() { }
        public Box(string name) { Name = name; }
        //public Box(string name,double length, double breadth, double height)
        //{
        //    Name = name;
        //    Length = length;
        //    Breadth = breadth;
        //    Height = height;
        //}
        //tính thể tích khối hộp
        public double Volume => Length * Breadth * Height;
        public double getVolume()
        {
            return Length * Breadth * Height;
        }
        // nạp chồng phép cộng
        public static Box operator +(Box b, Box c)
        {
            doubletodouble dbtodb = new doubletodouble(Sum);
            Box box = new Box
            {
                //delegate
                Length = dbtodb(b.Length, c.Length),
                Breadth=dbtodb(b.Breadth,c.Breadth),
                Height=dbtodb(b.Height,c.Height)
            };
            return box;
        }
        public static double Sum(double a,double b)
        {
            double c = a + b;
            return c;
        }
        // nạp chồng phép so sánh bằng
        public static bool operator ==(Box lhs, Box rhs)
        {
            bool status = false;
            if (lhs.Length == rhs.Length && lhs.Height == rhs.Height
               && lhs.Breadth == rhs.Breadth)
            {
                status = true;
            }
            return status;
        }
        // nạp chồng phép so sánh khác
        public static bool operator !=(Box lhs, Box rhs)
        {
            bool status = false;
            if (lhs.Length != rhs.Length || lhs.Height != rhs.Height ||
               lhs.Breadth != rhs.Breadth)
            {
                status = true;
            }
            return status;
        }
        // nạp chồng phép so sánh nhỏ hơn
        public static bool operator <(Box lhs, Box rhs)
        {
            bool status = false;
            if (lhs.Length < rhs.Length && lhs.Height < rhs.Height
               && lhs.Breadth < rhs.Breadth)
            {
                status = true;
            }
            return status;
        }
        // nạp chồng phép so sánh lớn hơn
        public static bool operator >(Box lhs, Box rhs)
        {
            bool status = false;
            if (lhs.Length > rhs.Length && lhs.Height >
               rhs.Height && lhs.Breadth > rhs.Breadth)
            {
                status = true;
            }
            return status;
        }
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", Length, Breadth, Height);
        }
        // Compare To
        public int CompareTo(Box other)
        {
            return this.getVolume().CompareTo(other.getVolume());
        }
        //Event
        public void ChangeClass(String name)
        {
            if (_ShowEvent != null)
                _ShowEvent(this, new NameChangedEvenetArgs(name));
        }

        public event EventHandler<NameChangedEvenetArgs> ShowEvent
        {
            add
            {
                _ShowEvent += value;
            }
            remove
            {
                _ShowEvent -= value;
            }
        }
        private event EventHandler<NameChangedEvenetArgs> _ShowEvent;

        public class NameChangedEvenetArgs : EventArgs
        {
            public String Name { get; set; }
            public NameChangedEvenetArgs(String name)
            {
                this.Name = name;
            }
        }
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            Box Box1 = new Box("box1");
            Box Box2 = new Box("box2");
            Box Box3 = new Box("box3");
            //add box1
            Box1.Length = 6;
            Box1.Breadth = 7;
            Box1.Height = 5;
            //add box2
            Box2.Length = 12;
            Box2.Breadth = 13;
            Box2.Height = 10;
            /* phép cộng hai hình hộp cho ra hình hộp khác có kích thước 
             * bằng tổng kích thước của hai hộp */
            Box3 = Box1 + Box2;
            Box3.Name = "box3";
            Console.WriteLine("Ho va Ten: Phan Ngoc Kim Cuong");
            Console.WriteLine("MSSV: 2175475");
            Console.WriteLine("Homework Week 05");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Overloading operator ");
            Console.WriteLine("Box 3: {0}", Box3.ToString());
            Console.WriteLine("Volume of Box3 : {0}", Box3.Volume);
            // so sánh hai hình hộp
            if (Box1 > Box2)
                Console.WriteLine("Box1 lon hon Box2");
            else
                Console.WriteLine("Box1 khong lon hon Box2");
            if (Box3 == Box1)
                Console.WriteLine("Box3 bang Box1");
            else
                Console.WriteLine("Box3 khong bang Box1");
            Console.WriteLine("------------------------------------------");
            //Event
            Console.WriteLine("Event");
            Box Box4 = new Box("box4");
            Console.WriteLine("Box 4: {0} ", Box4);
            Box4.ShowEvent += Bx_Create;
            Box4.Name = "box4";
            Box4.Length = 6;
            Box4.Height = 4;
            Box4.Breadth = 5;
            Console.WriteLine("Updated Box 4: {0} ",Box4);
            Console.WriteLine("------------------------------------------");
            //Compare
            Console.WriteLine("Compare Objects");
            Console.WriteLine("\nIComparable");
            List<Box> BoxLists  = new List<Box>();
            BoxLists.Add(Box1);
            BoxLists.Add(Box2);
            BoxLists.Add(Box3);
            BoxLists.Add(Box4);
            BoxLists.Sort();
            foreach (var bl in BoxLists)
            {
                Console.WriteLine(bl.Name + " volume: " + bl.getVolume());
            }
            Console.WriteLine("\nIComparer");
            BoxLists.Sort(new SortByHeightRB());//Sort by Height

            foreach (var bl in BoxLists)
            {
                Console.WriteLine(bl.Name + " height: " + bl.Height);
            }
            Console.ReadKey();
        }
        private static void Bx_Create(object sender, Box.NameChangedEvenetArgs e)
        {
            Console.WriteLine("Update Class Success: " + e.Name);
        }
        class SortByHeightRB : IComparer<Box>
        {
            public int Compare(Box x, Box y)
            {
                return x.Height.CompareTo(y.Height);
            }
        }
    }
}
