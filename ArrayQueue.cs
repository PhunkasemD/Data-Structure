using System;
namespace Queues
{
    public class ArrayQueue : Queue
    {
        private object[] data;
        private int SIZE;
        private int cap;
        private int firstIndex;

        public ArrayQueue(int cap)
        {
            data = new object[cap];
            this.cap = cap;
            SIZE = 0;
            firstIndex = 0;
        }

        private void ensureCapacity() // เช็กการขยายขนาดเมื่อ queue เต็ม
        {
            if (SIZE == data.Length)
            {
                object[] tempData = new object[2 * data.Length];
                for (int i = 0; i < SIZE; i++)
                {
                    tempData[i] = data[(firstIndex + i) % data.Length];
                }
                data = tempData;
                firstIndex = 0;
            }
        }

        public void enqueue(object e)
        {
            ensureCapacity();
            data[(firstIndex + SIZE) % data.Length] = e; // เพิ่มค่าที่ตำแหน่ง (firstIndex + SIZE) mod data.Length
            SIZE++;
        }

        public object peek()
        {
            if (isEmpty())
            {
                throw new System.MissingMemberException();
            }
            return data[firstIndex]; // คืนค่า element แรกใน queue
        }

        public object dequeue()
        {
            object e = peek();
            data[firstIndex] = null; // ลบ element ที่ตำแหน่ง firstIndex
            firstIndex = (firstIndex + 1) % data.Length; // อัปเดต firstIndex ให้วนไปยังตำแหน่งถัดไป
            SIZE--;
            return e;
        }

        public bool isEmpty()
        {
            return SIZE == 0;
        }

        public int size()
        {
            return SIZE;
        }

        public static class RadixSortArrayQueue
        {
            // หาจำนวนหลักสูงสุดของตัวเลขในอาร์เรย์
            private static int GetMaxIndexCount(double[] array)
            {
                int maxInt = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    int IntCount = (int)Math.Floor(Math.Log10(Math.Abs(array[i])) + 1); // เพื่อหาค่าลอการิทึมฐาน 10 ของตัวเลขแบบสัมบูรณ์ (เพื่อจัดการทั้งตัวเลขบวกและลบ)
                    if (IntCount > maxInt)//เปรียบเทียบจำนวนหลักของแต่ละตัวกับจำนวนหลักที่มากที่สุด (เก็บไว้ในตัวแปร maxInt) เพื่อหาค่าหลักสูงสุดในอาร์เรย์
                    {
                        maxInt = IntCount;
                    }
                }
                return maxInt;
            }

            // ฟังก์ชัน Radix Sort สำหรับค่าบวก
            private static void RadixSortPositive(double[] array)
            {
                int maxIntCount = GetMaxIndexCount(array);//เรียก GetMaxIndexCount เพื่อหาจำนวนหลักสูงสุดของตัวเลขในอาร์เรย์
                ArrayQueue[] buckets = new ArrayQueue[10];
                for (int i = 0; i < 10; i++)
                {
                    buckets[i] = new ArrayQueue(array.Length);
                }

                for (int Index = 0; Index < maxIntCount + 2; Index++) //ทำการจัดเรียงตามหลักจากน้อยไปมาก โดยเริ่มต้นจากหลักทศนิยมที่ตำแหน่ง -2(จัดการตัวเลขทศนิยม) ไปจนถึงหลักสูงสุดที่ค้นพบ
                {
                    double pow10Index = Math.Pow(10, Index - 2); // เริ่มจากหลักทศนิยม (-2)
                    for (int i = 0; i < array.Length; i++)//แบ่งตัวเลขลงใน buckets ตามค่าของหลักที่สนใจในแต่ละรอบ
                    {
                        int digitValue = (int)(Math.Abs(array[i]) / pow10Index) % 10;
                        buckets[digitValue].enqueue(array[i]);
                    }

                    int arrayIndex = 0;
                    for (int i = 0; i < 10; i++)//ดึงค่าจาก buckets กลับมาใส่ในอาร์เรย์ตามลำดับที่ถูกต้อง
                    {
                        while (!buckets[i].isEmpty())
                        {
                            array[arrayIndex++] = (double)buckets[i].dequeue();
                        }
                    }
                }
            }

            // ฟังก์ชัน Radix Sort สำหรับค่าลบ (จัดเรียงลำดับกลับด้าน)
            private static void RadixSortNegative(double[] array)
            {
                // แปลงค่าลบเป็นบวกสำหรับการจัดเรียง
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = Math.Abs(array[i]);//แปลงตัวเลขลบให้เป็นตัวเลขบวกทั้งหมด
                }

                RadixSortPositive(array);//เรียกใช้ฟังก์ชัน RadixSortPositive เพื่อจัดเรียงตัวเลขที่ถูกแปลงเป็นบวก

                // แปลงกลับเป็นค่าลบและจัดเรียงลำดับกลับ
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = -array[i]; // คืนค่าลบ
                }
                Array.Reverse(array); // ใช้ Array.Reverse เพื่อสลับลำดับจากมากไปน้อย
            }

            // ฟังก์ชันเรียกใช้การ sort
            public static void Sort(double[] array)//เป็นฟังก์ชันหลักในการจัดเรียงอาร์เรย์โดยใช้ Radix Sort
            {
                // แยกค่าลบและค่าบวกออกจากกัน
                List<double> positiveNumbers = new List<double>();
                List<double> negativeNumbers = new List<double>();

                for (int i = 0; i < array.Length; i++)//แปลง List เป็นอาร์เรย์เพื่อนำไปจัดเรียงแยกกัน
                {
                    if (array[i] < 0)
                    {
                        negativeNumbers.Add(array[i]);
                    }
                    else
                    {
                        positiveNumbers.Add(array[i]);
                    }
                }

                // แปลงเป็นอาร์เรย์เพื่อส่งเข้า Radix Sort
                double[] positiveArray = positiveNumbers.ToArray();
                double[] negativeArray = negativeNumbers.ToArray();

                // จัดเรียงค่าลบและบวก
                if (positiveArray.Length > 0)
                {
                    RadixSortPositive(positiveArray); // จัดเรียงค่าบวก
                }

                if (negativeArray.Length > 0)
                {
                    RadixSortNegative(negativeArray); // จัดเรียงค่าลบ
                }

                // รวมตัวเลขบวกและลบที่จัดเรียงแล้วเข้าด้วยกัน โดยนำตัวเลขลบมาไว้ก่อน แล้วตามด้วยตัวเลขบวก
                int index = 0;
                for (int i = 0; i < negativeArray.Length; i++)
                {
                    array[index++] = negativeArray[i];
                }

                for (int i = 0; i < positiveArray.Length; i++)
                {
                    array[index++] = positiveArray[i];
                }
            }
        }
    }
}

            



