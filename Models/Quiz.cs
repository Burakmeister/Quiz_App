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

        public virtual string Name { get; set; }
        public virtual User User { get; set; }
        public virtual int? TimeInMin { get; set; }

        public virtual ISet<Question> Questions { get; set; }
        public virtual ISet<Result> Scores { get; set; }

        public Quiz() { }
        public Quiz(string name, ISet<Question> questions, User user, int? timeInMin)
        {
            Name = name;
            Questions = questions;
            User = user;
            TimeInMin = timeInMin;
        }

        public override String ToString()
        {
            return Name;
        }

        public static implicit operator List<object>(Quiz v)
        {
            throw new NotImplementedException();
        }
    }
}
