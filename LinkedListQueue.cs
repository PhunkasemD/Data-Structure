using System;
using Lists;

namespace Queues
{
    public class LinkedListQueue : Queue
    {
        private List list = new LinkedList(); 

        // เพิ่มข้อมูลไปที่ท้ายลิสต์ (ตามหลัก FIFO)
        public void enqueue(object e)
        {
            list.add(e); // เพิ่ม element ไปที่ท้ายของ LinkedList
        }

        // ดูข้อมูลที่หัวลิสต์โดยไม่ลบออก
        public object peek()
        {
            if (isEmpty())
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }
            return list.get(0); // คืนค่า element ที่หัวของ LinkedList (index 0)
        }

        // นำข้อมูลที่หัวลิสต์ออก
        public object dequeue()
        {
            object e = peek();  // ดูข้อมูลที่หัวก่อน
            list.remove(0);     // ลบข้อมูลที่หัวของลิสต์ (index 0)
            return e;           // คืนค่าข้อมูลที่นำออก
        }

        // ตรวจสอบว่า Queue ว่างหรือไม่
        public bool isEmpty()
        {
            return list.isEmpty();
        }

        // คืนค่าขนาดของ Queue (จำนวน element ใน Queue)
        public int size()
        {
            return list.size();
        }
    }

}

