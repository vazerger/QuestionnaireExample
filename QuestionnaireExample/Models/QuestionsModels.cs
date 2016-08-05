using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireExample.Models.DB;

namespace QuestionnaireExample.Models
{
    public class QuestionsModels
    {
        public int qUserId { get; set; }
        public string qUserName { get; set; }
        public int qCountQustions { get; set; }

        public IEnumerable<QE_Questions> qQuestions { get; set; }
        
    }

    public class Question
    {
        public int id { get; set; }
        public string name { get; set; }
        public int n { get; set; }
    }

    public class QuestionKeys
    {
        public int id { get; set; }
    }

    public class QuestionUserValue
    {
        public int questionid { get; set; }
        public int userid { get; set; }
        public int value { get; set; }
    }


}