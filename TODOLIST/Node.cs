using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOLIST
{
    internal class Node<T>
    {
        // Properties
        public T Data { get; set; }
        public Node<T>? Next { get; set; }

        // Constructors
        public Node(T data)
        {
            Data = data;
            Next = null;
        }

        public Node(T data, Node<T> next)
        {
            Data = data;
            Next = next;
        }

        // Overrides
        public override string ToString()
        {
            return Data?.ToString();
        }
    }
}
