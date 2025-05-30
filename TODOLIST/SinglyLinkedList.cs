using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOLIST
{
    internal class SinglyLinkedList<T>
    {
        private Node<T> head;

        public int Count { get; private set; }

        public SinglyLinkedList()
        {
            head = null;
            Count = 0;
        }

        // Add to end
        public void AddLast(T data)
        {
            var newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }

            Count++;
        }

        // Add to beginning
        public void AddFirst(T data)
        {
            var newNode = new Node<T>(data, head);
            head = newNode;
            Count++;
        }

        // Remove first occurrence of a value
        public bool Remove(T data)
        {
            if (head == null)
                return false;

            if (head.Data.Equals(data))
            {
                head = head.Next;
                Count--;
                return true;
            }

            Node<T> current = head;
            while (current.Next != null && !current.Next.Data.Equals(data))
            {
                current = current.Next;
            }

            if (current.Next == null)
                return false;

            current.Next = current.Next.Next;
            Count--;
            return true;
        }

        // Search for a value
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // Print all elements
        public void PrintAll()
        {
            Node<T> current = head;
            while (current != null)
            {
                Console.Write(current.Data + " -> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
    }
}
