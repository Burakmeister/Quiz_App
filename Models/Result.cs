using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Models
{
    public class Result
    {
        public virtual int Id { get; set; }
        public virtual int Score { get; set; }
        public virtual int Percentage { get; set; }
        public virtual Quiz Quiz { get; set; }

        public Result() { }

        public Result(int score, Quiz quiz)
        {
            Score = score;
            Quiz = quiz;
        }

        public override string ToString()
        {
            return Percentage.ToString(); 
        }
    }
}
