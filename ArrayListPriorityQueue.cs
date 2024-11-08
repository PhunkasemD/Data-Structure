using System;
using Lists;

namespace PriorityQueues
{
	public class ArrayListPriorityQueue : PriorityQueue
	{
        private List list;
		public ArrayListPriorityQueue(int cap)
		{
            list = new ArrayList(cap);
		}

        public void enqueue(object e)
        {
            list.add(e);
        }

        public int HighestPriorityIndex()
        {
            int j = 0;
            for (int i = 1; i < list.size(); i++)
            {
                IComparable I = (IComparable)list.get(i);
                if (I.CompareTo(list.get(j)) < 0)
                    j = i;
            }
            return j;
        }

        public object peek()
        {
            return list.get(HighestPriorityIndex());
        }

        public object dequeue()
        {
            int i = HighestPriorityIndex();
            object e = list.get(i);
            list.remove(i);
            return e;

        }

        public bool isEmpty()
        {
            return list.isEmpty();
        }

        public int size()
        {
            return list.size();
        }
    }
}

