using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOLIST
{
    internal class Stack<T>
    {
        private Node<T> top;
        public int Count { get; private set; }

        public Stack()
        {
            top = null;
            Count = 0;
        }

        // Push item onto the stack
        public void Push(T data)
        {
            var newNode = new Node<T>(data, top);
            top = newNode;
            Count++;
        }

        // Pop item from the stack
        public T Pop()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Stack is empty.");

            T value = top.Data;
            top = top.Next;
            Count--;
            return value;
        }

        // Peek at the top item without removing it
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Stack is empty.");

            return top.Data;
        }

        // Check if the stack is empty
        public bool IsEmpty()
        {
            return Count == 0;
        }

        // Print all items in the stack
        public void Print()
        {
            Node<T> current = top;
            while (current != null)
            {
                Console.Write(current.Data + " -> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
    }
}
