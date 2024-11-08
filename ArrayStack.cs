using System;
using System.Text;
using Collections;
using Lists;
namespace Stacks
{
	public class ArrayStack : Stack
	{
		private object[] data;
		private int SIZE;
		private int cap;

		public ArrayStack(int cap)
		{
            data = new object[cap];
            this.cap = cap;
        }

        private void ensureCapacity()// เช็ก Error ของ push
        {
            if (SIZE + 1 > data.Length)
            {
                object[] tempdata = new object[2 * SIZE];
                for (int i = 0; i < SIZE; i++)
                    tempdata[i] = data[i];
                data = tempdata;
            }
        }

        public void push(object e)
        {
            ensureCapacity();
            data[SIZE++] = e;

        }

        public object peek()
        {
            if (isEmpty())
                throw new System.MissingMemberException();
            return data[SIZE - 1];
        }

        public object pop()
        {
            object e = peek();
            data[--SIZE] = null;
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

        private int IndexOf(object e)
        {

            for (int i = 0; i < SIZE; i++)
            {

                if (data[i].Equals(e))
                    return i;

            }
            return -1;
        }

        public static bool IsCorrectParentheses(string t) {

            ArrayStack stack = new ArrayStack(t.Length);
            string open = "([{<";
            string close = ")]}>";

            for (int i = 0; i < t.Length; i++)
            {

                char cht = t[i];

                if (open.IndexOf(cht) != -1)
                {
                    stack.push(cht);

                }
                else if (close.IndexOf(cht) != -1)
                {

                    if (stack.isEmpty()) return false;

                    char LastPush = (char)stack.pop();
                    if (open.IndexOf(LastPush) != close.IndexOf(cht)) 
                        return false;

                }
            }
            return stack.isEmpty();
        }


        // Method to convert infix expression to postfix
        public static List InfixToPostfix(List infix)
        {
            // แปลง List เป็น string เพื่อตรวจสอบวงเล็บ
            string infixString = ListToString(infix);

            // เช็ควงเล็บก่อน
            if (!IsCorrectParentheses(infixString))
            {
                Console.WriteLine("False");
                return null; // เปลี่ยนเป็น null ถ้าหากวงเล็บไม่ถูกต้อง
            }

            ArrayList postfix = new ArrayList(infix.size()); // ใช้ ArrayList เป็นตัวเก็บผลลัพธ์ postfix
            Stack stack = new ArrayStack(infix.size()); // ใช้ ArrayStack สำหรับจัดการกับ operator
            string operators = "+-*/";

            for (int i = 0; i < infix.size(); i++)
            {
                string ch = infix.get(i).ToString(); // เข้าถึงแต่ละตัวใน List

                // ถ้าเป็น operand ให้ใส่ลงใน postfix list
                if (Char.IsDigit(ch[0]))
                {
                    postfix.add(ch);
                }
                // ถ้าเป็นวงเล็บเปิด ให้ push ลง stack
                else if (ch == "(")
                {
                    stack.push(ch);
                }
                // ถ้าเป็นวงเล็บปิด ให้ pop และ append จนกว่าจะเจอวงเล็บเปิด
                else if (ch == ")")
                {
                    while (!stack.isEmpty() && stack.peek().ToString() != "(")
                    {
                        postfix.add(stack.pop().ToString());
                    }
                    stack.pop(); // pop วงเล็บเปิดทิ้ง
                }
                // ถ้าเป็น operator
                else if (operators.IndexOf(ch) != -1)
                {
                    while (!stack.isEmpty() && Precedence(stack.peek().ToString()[0]) >= Precedence(ch[0]))
                    {
                        postfix.add(stack.pop().ToString());
                    }
                    stack.push(ch);
                }
            }

            // Pop ตัวดำเนินการทั้งหมดออกจาก stack และเพิ่มลง postfix
            while (!stack.isEmpty())
            {
                postfix.add(stack.pop().ToString());
            }

            return postfix;
        }

        // เมธอดสำหรับแปลง List เป็น string
        public static string ListToString(List infix)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < infix.size(); i++)
            {
                result.Append(infix.get(i).ToString());
            }
            return result.ToString();
        }

        // ฟังก์ชันสำหรับการให้ค่าความสำคัญ (precedence) ของ operator ยังคงไม่เปลี่ยนแปลง
        private static int Precedence(char op)
        {
            if (op == '+' || op == '-')
                return 1;
            if (op == '*' || op == '/')
                return 2;
            return 0;
        }

        // เมธอดสำหรับคำนวณผลลัพธ์ของนิพจน์แบบ postfix
        public static float CalculatePostfix(string postfix)
        {
            ArrayStack stack = new ArrayStack(postfix.Length);

            for (int i = 0; i < postfix.Length; i++)
            {
                char ch = postfix[i];

                // ถ้าตัวอักษรเป็น digit ให้ push ลงใน stack (ใช้ float)
                if (Char.IsDigit(ch))
                {
                    stack.push((float)(ch - '0')); // แปลงตัวเลขให้เป็น float
                }
                // ถ้าตัวอักษรเป็น operator ให้ pop สอง operand และใช้ operator
                else
                {
                    float operand2 = (float)stack.pop(); // เปลี่ยนเป็น float
                    float operand1 = (float)stack.pop(); // เปลี่ยนเป็น float
                    switch (ch)
                    {
                        case '+': stack.push(operand1 + operand2); break;
                        case '-': stack.push(operand1 - operand2); break;
                        case '*': stack.push(operand1 * operand2); break;
                        case '/': stack.push(operand1 / operand2); break;
                    }
                }
            }

            // ผลลัพธ์สุดท้ายจะอยู่ที่ด้านบนของ stack (เป็น float)
            return (float)stack.pop();
        }

    }
}

