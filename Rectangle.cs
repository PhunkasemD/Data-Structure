using System;
namespace PriorityQueues
{
	public class Rectangle : IComparable
	{
        private double W;
        private double H;

		public Rectangle(double W, double H)
		{
            this.W = W;
            this.H = H;
		}

        public int CompareTo(object e)
        {
            Rectangle E = (Rectangle)e;
            double Area1 = W * H;
            double Area2 = E.W * E.H;

            if (Area1 > Area2)
                return 1;
            if (Area1 < Area2)
                return -1;
            return 0;
        }
    }
}

