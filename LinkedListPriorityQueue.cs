using System;
using Lists;
using Collections;

namespace PriorityQueues
{
    public class LinkedListPriorityQueue : PriorityQueue
    {
        private LinkedList list;

        public LinkedListPriorityQueue()
        {
            list = new LinkedList();
        }

        // enqueue เพิ่ม element เข้าไปใน list
        public void enqueue(object e)
        {
            list.add(e); // ใช้เมธอด add ของ LinkedList
        }

        // ค้นหา index ที่มี priority สูงสุด
        public int HighestPriorityIndex()
        {
            int index = 0;
            int highestIndex = 0;
            Lists.LinkedList.LinkedNode current = list.nodeAt(0); // เริ่มที่ node ตัวแรกหลังจาก header
            IComparable highest = (IComparable)current.e; // เริ่มจากข้อมูลของ node แรก

            while (index < list.size()) // Traversal ตามจำนวน SIZE ของ list
            {
                IComparable currentData = (IComparable)current.e;
                if (currentData.CompareTo(highest) < 0) // ค้นหา element ที่มีค่าน้อยที่สุด
                {
                    highest = currentData;
                    highestIndex = index;
                }
                current = current.next; // ไปยัง node ถัดไป
                index++;
            }
            return highestIndex;
        }

        // peek คืนค่า element ที่มี priority สูงสุด (โดยไม่ลบ)
        public object peek()
        {
            return list.get(HighestPriorityIndex());
        }

        // dequeue คืนค่าและลบ element ที่มี priority สูงสุด
        public object dequeue()
        {
            int i = HighestPriorityIndex();
            object e = list.get(i);
            list.remove(i);
            return e;
        }

        // ตรวจสอบว่า list ว่างหรือไม่
        public bool isEmpty()
        {
            return list.isEmpty();
        }

        // คืนค่าขนาดของ list
        public int size()
        {
            return list.size();
        }
    }
}
