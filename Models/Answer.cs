using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Models
{
    public class Answer
    {
        public virtual int Id { get; set; }
        public virtual string Content { get; set; }
        //public virtual bool IsCorrect { get; set; }
    }
}
