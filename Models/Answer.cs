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
        public virtual Question Question { get; set; }
        public virtual bool IsCorrect { get; set; }
        public Answer(string content)
        {
            Content = content;
        }

        public Answer() { }
        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj == this) return true;
            if(obj.GetType() != typeof(Answer)) return false;
            Answer ans = (Answer)obj;
            return Content.Equals(ans.Content);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
