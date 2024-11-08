using System;
using System.Collections.Generic;
using Lists;
namespace PriorityQueues
{
    public class BinaryHeap : PriorityQueue
    {
        public object[] data;
        private int SIZE;
        private int cap;

    public BinaryHeap(int cap)
        {
            data = new object[cap];
            this.cap = cap;
        }

        private void ensureCapacity()
        {
            if (SIZE + 1 > data.Length)
            {
                object[] tempdata = new object[2 * data.Length];
                for (int i = 0; i < SIZE; i++)
                    tempdata[i] = data[i];
                data = tempdata;
            }
        }

        private void reorderUp(int k)
        {
            while (k > 0)
            {
                int p = (k - 1) / 2;
                if (!isGreaterThan(k, p)) break;
                swap(k, p);
                k = p;
            }
        }

        private void swap(int i, int j)
        {
            object t = data[i];
            data[i] = data[j];
            data[j] = t;
        }

        private bool isGreaterThan(int i, int j)
        {
            return ((IComparable)data[i]).CompareTo(data[j]) > 0;
        }

        public void enqueue(object e)
        {
            ensureCapacity();
            data[SIZE] = e;
            reorderUp(SIZE++);
        }

        public object peek()
        {
            if (isEmpty()) throw new InvalidOperationException("Heap is empty");
            return data[0];
        }

        public object dequeue()
        {
            object e = peek();
            data[0] = data[--SIZE];
            data[SIZE] = null;
            if (SIZE > 1) reorderDown(0);
            return e;
        }

        private void reorderDown(int k)
        {
            int c;
            while ((c = 2 * k + 1) < SIZE)
            {
                if (c + 1 < SIZE && isGreaterThan(c + 1, c))
                    c++;
                if (isGreaterThan(k, c)) break;
                swap(k, c);
                k = c;
            }
        }

        public static void heapSort(object[] x)
        {
            BinaryHeap h = new BinaryHeap(x.Length);
            h.data = x;
            h.SIZE = x.Length;

            // สร้าง heap โดยการ reorderDown จากตำแหน่งกลางลงไป
            for (int k = (h.size() / 2) - 1; k >= 0; k--)
            {
                h.reorderDown(k);
            }

            // ดึงค่าที่มากที่สุดจาก heap และจัดเก็บใน x
            for (int k = h.size() - 1; k >= 0; k--)
            {
                // นำค่าที่มากที่สุดไปไว้ที่ตำแหน่งสุดท้าย
                x[k] = h.dequeue(); // เอาค่าที่มากที่สุดออกจาก heap
            }
        }

        public bool isEmpty()
        {
            return SIZE == 0;
        }

        public int size()
        {
            return SIZE;
        }
    }


}