using System;
namespace Lists
{
    public class ArrayList : List
    {
        private int SIZE, cap;
        private object[] data;


        public ArrayList(int cap) //Constructor
        {
            this.cap = cap;
            data = new object[cap];
        }

        private void ensureCapacity()
        {
            if (SIZE + 1 > data.Length) {

                object[] tempdata = new object[SIZE * 2];
                for (int i = 0; i < SIZE; i++)
                    tempdata[i] = data[i];
                data = tempdata;
            }
            
        }

        public void add(int index, object e)
        {
            ensureCapacity();
            for (int i = SIZE; i > index; i--)
                data[i] = data[i - 1];
            data[index] = e;
            SIZE++;
        }

        public void add(object e)
        {
            add(SIZE, e);
        }

        public void remove(int index)
        {
            if (index >= SIZE) return;
            for (int i = index + 1; i < SIZE; i++)
                data[i - 1] = data[i];
            data[--SIZE] = null;

        }

        public void remove(object e)
        {
            int i = indexOf(e);
            if (i > -1)
                remove(i);
        }

        public bool contains(object e)
        {
            return indexOf(e) >= 0;
        }


        public int indexOf(object e)
        {
            for (int i = 0; i < SIZE; i++)
                if (data[i] != null && data[i].Equals(e))
                    return i;
            return -1;
        }

        public object get(int index)
        {
            return data[index];
        }

        public void set(int index, object e)
        {
            data[index] = e;
        }

        public bool isEmpty()
        {
            return SIZE == 0;
        }

        public int size()
        {
            return SIZE;
        }

        public object[] ToArray()
        {
            object?[] result = new object?[SIZE]; // สร้างอาเรย์ใหม่
            for (int i = 0; i < SIZE; i++)
            {
                result[i] = data[i]; // คัดลอกข้อมูลจาก data
            }
            return result; // คืนค่าอาเรย์ใหม่
        }
    }
}

