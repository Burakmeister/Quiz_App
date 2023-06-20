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
        public virtual Quiz Quiz { get; set; }
        public virtual ISet<Answer> Answers { get; set; }
        public Question(string content, ISet<Answer> answers)
        {
            Content = content;
            Answers = answers;
        }
        public Question() { }

        public override string ToString()
        {
            return Content;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != typeof(Quiz)) return false;
            Question question = obj as Question;
            return question.Content.Equals(Content);
        }
    }
}
