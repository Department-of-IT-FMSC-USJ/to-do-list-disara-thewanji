using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TODOLIST
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Completed
    }
    internal class TaskItem
    {
 
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public TaskStatus Status { get; set; }

            public TaskItem(int id, string name, string description, DateTime date)
            {
                ID = id;
                Name = name;
                Description = description;
                Date = date;
                Status = TaskStatus.ToDo;
            }

            public override string ToString()
            {
                return $"[{ID}] {Name} - {Status} - {Date.ToShortDateString()}";
            }
        
    }

}
