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
        public virtual Answer CorrectAnswer { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual ISet<Answer> Answers { get; set; }
        public Question(string content, Answer correctAnswer, ISet<Answer> answers)
        {
            Content = content;
            CorrectAnswer = correctAnswer;
            Answers = answers;
        }
        public Question() { }

        public override string ToString()
        {
            return Content;
        }
    }
}
