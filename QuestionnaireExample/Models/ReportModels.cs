using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireExample.Models
{
    public class Report
    {
        public string question { get; set; }
        public string value { get; set; }
    }

    public class GReportModels
    {
        public int allUsers { get; set; }
        public string resume { get; set; }

        public List<Report> reports { get; set; }
    }

    public class MyReportModel : Report
    {
        public int id { get; set; }
        public string username { get; set; }
    }
}