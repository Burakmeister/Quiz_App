using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Models
{
    public class Question
    {
        public virtual int Id { get; set; }
        public virtual string Content { get; set; }
        public virtual int CorrectAnswerId { get; set; }
        public virtual ISet<Answer> Answers { get; set; }
    }
}
