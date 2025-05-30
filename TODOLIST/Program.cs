using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TODOLIST
{
    internal class Program
    {
        static SinglyLinkedList<TaskItem> toDoTasks = new SinglyLinkedList<TaskItem>();
        static Stack<TaskItem> inProgressTasks = new Stack<TaskItem>();
        static Queue<TaskItem> completedTasks = new Queue<TaskItem>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nTask Manager Menu:");
                Console.WriteLine("1. Add New Task");
                Console.WriteLine("2. View To-Do Tasks");
                Console.WriteLine("3. Move Task To In-Progress");
                Console.WriteLine("4. View In-Progress Tasks");
                Console.WriteLine("5. Complete In-Progress Task");
                Console.WriteLine("6. View Completed Tasks");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewTask();
                        break;
                    case "2":
                        Console.WriteLine("\nTo-Do Tasks:");
                        toDoTasks.PrintAll();
                        break;
                    case "3":
                        MoveTaskToInProgress();
                        break;
                    case "4":
                        Console.WriteLine("\nIn-Progress Tasks:");
                        inProgressTasks.Print();
                        break;
                    case "5":
                        CompleteInProgressTask();
                        break;
                    case "6":
                        Console.WriteLine("\nCompleted Tasks:");
                        completedTasks.Print();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddNewTask()
        {
            Console.Write("Enter Task ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Date (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            TaskItem task = new TaskItem(id, name, description, date);
            InsertToDoTaskSorted(task);
            Console.WriteLine("Task added to To-Do list.");
        }

        static void InsertToDoTaskSorted(TaskItem task)
        {
            var headField = typeof(SinglyLinkedList<TaskItem>)
                .GetField("head", BindingFlags.NonPublic | BindingFlags.Instance);

            Node<TaskItem> head = headField.GetValue(toDoTasks) as Node<TaskItem>;

            if (head == null || task.Date < head.Data.Date)
            {
                var newNode = new Node<TaskItem>(task, head);
                headField.SetValue(toDoTasks, newNode);
                IncrementCount(toDoTasks);
                return;
            }

            Node<TaskItem> current = head;
            while (current.Next != null && current.Next.Data.Date <= task.Date)
            {
                current = current.Next;
            }

            Node<TaskItem> newNodeInsert = new Node<TaskItem>(task, current.Next);
            current.Next = newNodeInsert;
            IncrementCount(toDoTasks);
        }

        static void MoveTaskToInProgress()
        {
            Console.Write("Enter Task ID to move to In-Progress: ");
            int id = int.Parse(Console.ReadLine());

            var headField = typeof(SinglyLinkedList<TaskItem>)
                .GetField("head", BindingFlags.NonPublic | BindingFlags.Instance);

            Node<TaskItem> head = headField.GetValue(toDoTasks) as Node<TaskItem>;

            Node<TaskItem> current = head;
            Node<TaskItem> prev = null;

            while (current != null)
            {
                if (current.Data.ID == id)
                {
                    TaskItem task = current.Data;
                    task.Status = TaskStatus.InProgress;

                    if (prev == null)
                        headField.SetValue(toDoTasks, current.Next);
                    else
                        prev.Next = current.Next;

                    DecrementCount(toDoTasks);
                    inProgressTasks.Push(task);

                    Console.WriteLine("Task moved to In-Progress.");
                    return;
                }

                prev = current;
                current = current.Next;
            }

            Console.WriteLine("Task not found.");
        }

        static void CompleteInProgressTask()
        {
            if (inProgressTasks.IsEmpty())
            {
                Console.WriteLine("No tasks in progress.");
                return;
            }

            TaskItem task = inProgressTasks.Pop();
            task.Status = TaskStatus.Completed;
            completedTasks.Enqueue(task);

            Console.WriteLine("Task completed and moved to Completed list.");
        }

        // Helper methods to adjust private Count field using reflection
        static void IncrementCount(SinglyLinkedList<TaskItem> list)
        {
            var countField = typeof(SinglyLinkedList<TaskItem>)
                .GetProperty("Count", BindingFlags.Public | BindingFlags.Instance);
            int currentCount = (int)countField.GetValue(list);
            countField.SetValue(list, currentCount + 1);
        }

        static void DecrementCount(SinglyLinkedList<TaskItem> list)
        {
            var countField = typeof(SinglyLinkedList<TaskItem>)
                .GetProperty("Count", BindingFlags.Public | BindingFlags.Instance);
            int currentCount = (int)countField.GetValue(list);
            countField.SetValue(list, currentCount - 1);
        }
    }
}
