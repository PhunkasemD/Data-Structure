using System;
namespace Lists
{
	public class SinglyLinkedList : List
	{

        private class LinkedNode
        {
            public object e;
            public LinkedNode next;

            public LinkedNode(object e, LinkedNode next)
            {
                this.e = e;
                this.next = next;
            }
        }
        private int SIZE;
        private LinkedNode first = new LinkedNode(null, null);

        public SinglyLinkedList() { }

        private LinkedNode nodeAt(int index) //มี index ตำแหน่งชัดเจนใช้ for
        {
            LinkedNode node = first;
            for (int i = -1; i < index; i++)
                node = node.next;
            return node;
        }

        public void add(int index, object e)
        {
            if (index <= SIZE)
            {
                LinkedNode node = nodeAt(index - 1);
                node.next = new LinkedNode(e, node.next);
                SIZE++;
            }


        }

        public void add(object e)
        {
            add(SIZE, e);
        }

        public bool contains(object e)
        {
            return indexOf(e) >= 0;
        }

        public object get(int index)
        {
            return nodeAt(index).e;
        }

        public int indexOf(object e)
        {
            LinkedNode node = first.next;
            for (int i = 0; i < SIZE; i++)
            {
                if (node.e.Equals(e)) return i;
                node = node.next;
            }
            return -1;
        }

        public bool isEmpty()
        {
            return SIZE == 0;
        }
        private void removeAfter(LinkedNode node)
        {
            if (node.next != null)
            {
                node.next = node.next.next;
                SIZE--;
            }
        }

        public void remove(int index)
        {
            LinkedNode node = nodeAt(index - 1);
            removeAfter(node);
        }

        public void remove(object e)
        {
            LinkedNode node = first;
            while (node.next != null && !node.next.e.Equals(e))
                node = node.next;
            removeAfter(node);

        }

        public void set(int index, object e)
        {
            nodeAt(index).e = e;
        }

        public int size()
        {
            return SIZE;
        }

        public object[] ToArray()
        {
            object[] result = new object[SIZE]; // สร้างอาเรย์ใหม่
            LinkedNode node = first.next; // เริ่มจาก node แรกที่ไม่ใช่ dummy node

            for (int i = 0; i < SIZE; i++)
            {
                result[i] = node.e; // คัดลอกค่าจาก node ลงในอาเรย์
                node = node.next; // เลื่อนไปยัง node ถัดไป
            }

            return result; // คืนค่าอาเรย์
        }
    }
}

