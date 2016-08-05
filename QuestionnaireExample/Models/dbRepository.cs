using QuestionnaireExample.Models.DB;
using QuestionnaireExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace QuestionnaireExample.Models
{
    public class dbRepository
    {
        private AnketaEntities entity = new AnketaEntities();

        // добавляет пользователя
        public bool AddQUser(string name)
        {
            // отслеживает успешное добавление
            bool flag = false;

            if (!String.IsNullOrWhiteSpace(name))
            {
                QE_User user = new QE_User();
                user.QUSER_NAME = HttpUtility.HtmlDecode(name);

                try
                {
                    entity.QE_User.Add(user);
                    entity.SaveChanges();
                    flag = true;
                }
                catch (Exception e)
                {
                    // можно отправить сообщение по почте или записать в базу
                }
            }

            return flag;
        }

        // все вопросы
        public IEnumerable<QE_Questions> GetQuestions()
        {
            return entity.QE_Questions.OrderBy(c => c.Q_ID);
        }

        // все вопросы
        public List<QuestionKeys> GetKeyQuestions()
        {
            List<QuestionKeys> list = new List<QuestionKeys>();
            var model = entity.QE_Questions.OrderBy(c => c.Q_ID);
            foreach (var item in model)
            {
                list.Add(new QuestionKeys { id = item.Q_ID });
            }
            return list;
        }

        // вопрос по id
        public Question GetQuestion(int id)
        {
            Question model = new Question();
            var questions = entity.QE_Questions.SingleOrDefault(c => c.Q_ID == id);
            model.id = questions.Q_ID;
            model.name = questions.Q_QUESTIONS;
            model.n = (int)questions.Q_MAXN;

            return model;
        }

        // создать пользователя и вернуть его звначение
        public int GetUserId(string name)
        {
            QE_User usmodel = new QE_User();
            usmodel.QUSER_NAME = name;
            entity.QE_User.Add(usmodel);
            entity.SaveChanges();
            return usmodel.QUSER_ID;

        }

        public bool AddQuestionsUserValue(QuestionUserValue questionsvalue)
        {
            bool flag = true;

            QE_QuestionsUserValue model = new QE_QuestionsUserValue();
            model.QE_QUSERID = questionsvalue.userid;
            model.QE_QID = questionsvalue.questionid;
            model.QE_VALUE = questionsvalue.value;

            entity.QE_QuestionsUserValue.Add(model);
            entity.SaveChanges();


            return flag;

        }

        public List<Report> GetResultUserById(int id)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            string command = "SELECT QE_Questions.Q_QUESTIONS, QE_QuestionsUserValue.QE_VALUE, QE_Questions.Q_ID FROM QE_QuestionsUserValue INNER JOIN QE_Questions ON QE_QuestionsUserValue.QE_QID = QE_Questions.Q_ID WHERE (QE_QuestionsUserValue.QE_QUSERID = @id)";
            SqlCommand cmd = new SqlCommand(command, cn);
            cn.Open();
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Report> model = new List<Report>();
            while (dr.Read())
            {
                model.Add(new Report { question = dr[0].ToString(), value = dr[1].ToString() });
            }
            dr.Close();
            cn.Close();

            return model;
        }

        public GReportModels GetGeneralReport()
        {
            GReportModels model = new GReportModels();
            decimal count = 0;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string command = "SELECT QE_Questions.Q_QUESTIONS, AVG(QE_QuestionsUserValue.QE_VALUE) AS Expr1 " +
                "FROM QE_QuestionsUserValue INNER JOIN " +
                "QE_Questions ON QE_QuestionsUserValue.QE_QID = QE_Questions.Q_ID " +
                "GROUP BY QE_Questions.Q_ID, QE_Questions.Q_QUESTIONS";
            SqlCommand cmd = new SqlCommand(command, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<Report> reports = new List<Report>();
            while (dr.Read())
            {
                decimal par = Convert.ToDecimal(dr[1].ToString());
                reports.Add(new Report { question = dr[0].ToString(), value = string.Format("{0:0.0}", par) });

                count += par;
            }
            dr.Close();
            cn.Close();

            model.reports = reports;
            model.resume = string.Format("{0:0.0}", count);
            model.allUsers = entity.QE_User.OrderBy(c => c.QUSER_ID).Count();

            return model;
        }

        public List<MyReportModel> GetMyReport()
        {           

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            string command = "SELECT QE_User.QUSER_ID, QE_User.QUSER_NAME, MAX(QE_QuestionsUserValue.QE_VALUE) AS Expr1 FROM QE_QuestionsUserValue INNER JOIN QE_User ON QE_QuestionsUserValue.QE_QUSERID = QE_User.QUSER_ID GROUP BY QE_User.QUSER_NAME, QE_User.QUSER_ID";
            
            SqlCommand cmd = new SqlCommand(command, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<MyReportModel> model = new List<MyReportModel>();
            while (dr.Read())
            {
                model.Add(new MyReportModel { 
                    id = Convert.ToInt32(dr[0].ToString()),
                    username = dr[1].ToString(),
                    value = dr[2].ToString()                   
                });
            }
            dr.Close();
            cn.Close();

            return model;
        }

        public int CountUsers()
        {
            return entity.QE_User.Count();
        }

        public void Removied()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string command = "DELETE FROM QE_User";
            SqlCommand cmd = new SqlCommand(command, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}