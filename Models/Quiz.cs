using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Models
{
    public class Quiz
    {
        public virtual int Id { get; set; }

        public virtual ISet<Question> Questions { get; set; }
    }
}
