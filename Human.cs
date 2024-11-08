using System;
namespace PriorityQueues
{
	public class Human : IComparable
	{
        private float mass;
        private float height;

		public Human(float mass, float height)
		{
            this.mass = mass;
            this.height = height;
		}

        public int CompareTo(object e)
        {
            float bmi1 = mass / (height * height);
            Human E = (Human)e;
            float bmi2 = E.mass / (E.height * E.height);

            if (bmi1 > bmi2)
                return 1;
            if (bmi1 < bmi2)
                return -1;
            return 0;
        }
    }
}

