using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOLIST
{
    internal class Queue<T>
    {

            private Node<T> front;
            private Node<T> rear;
            public int Count { get; private set; }

            public Queue()
            {
                front = rear = null;
                Count = 0;
            }

            // Add item to the end of the queue
            public void Enqueue(T data)
            {
                var newNode = new Node<T>(data);
                if (rear == null)
                {
                    front = rear = newNode;
                }
                else
                {
                    rear.Next = newNode;
                    rear = newNode;
                }
                Count++;
            }

            // Remove item from the front of the queue
            public T Dequeue()
            {
                if (IsEmpty())
                    throw new InvalidOperationException("Queue is empty.");

                T value = front.Data;
                front = front.Next;
                if (front == null)
                    rear = null;

                Count--;
                return value;
            }

            // View the item at the front without removing it
            public T Peek()
            {
                if (IsEmpty())
                    throw new InvalidOperationException("Queue is empty.");

                return front.Data;
            }

            // Check if the queue is empty
            public bool IsEmpty()
            {
                return Count == 0;
            }

            // Print all elements in the queue
            public void Print()
            {
                Node<T> current = front;
                while (current != null)
                {
                    Console.Write(current.Data + " -> ");
                    current = current.Next;
                }
                Console.WriteLine("null");
            }
        
    }

}
