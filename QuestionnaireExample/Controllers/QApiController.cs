using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuestionnaireExample.Models;
using QuestionnaireExample.Models.DB;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace QuestionnaireExample.Controllers
{
    public class QApiController : ApiController
    {
        private dbRepository repository = new dbRepository();

        // PUT: api/QApi/Name  
        [ResponseType(typeof(void))]
        public IHttpActionResult PutName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            
            // добавляем
            var addFlag = repository.AddQUser(name);
            
            if (!addFlag)
            {
                    return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/QApi/GetQuestions
        [ResponseType(typeof(QE_Questions))]
        public IHttpActionResult GetQuestions()
        {
            var questions = repository.GetQuestions().ToList();
            
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // GET: api/QApi/GetQuestions
        [ResponseType(typeof(QuestionKeys))]
        public IHttpActionResult GetKeyQuestions()
        {
            var questions = repository.GetKeyQuestions();

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // GET: api/QApi/GetQuestion/id
        [ResponseType(typeof(QE_Questions))]
        public IHttpActionResult GetQuestion(int id)
        {
            var question = repository.GetQuestion(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // GET: api/QApi/GetUserId/id/
        [ResponseType(typeof(QuestionKeys))]
        public IHttpActionResult GetUserId(string id)
        {
            var key = 0;
            key = repository.GetUserId(id);

            if (key == 0)
            {
                return NotFound();
            }

            return Ok(key);
        }

        // POST: api/QApi/PostQuestionUserValue  
        [ResponseType(typeof(QuestionUserValue))]
        public IHttpActionResult PostQuestionUserValue(QuestionUserValue questionsvalue)  
        {  
            if (!ModelState.IsValid)  
            {  
                return BadRequest(ModelState);  
            }

            var result = repository.AddQuestionsUserValue(questionsvalue);

            return CreatedAtRoute("DefaultApi", new { id = questionsvalue.value }, questionsvalue);  
        }

        // GET: api/QApi/GetResultUser/id/
        [ResponseType(typeof(Report))]
        public IHttpActionResult GetResultUser(int id)
        {
            var result = repository.GetResultUserById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/QApi/GetGeneralReport
        [ResponseType(typeof(Report))]
        public IHttpActionResult GetGeneralReport()
        {
            var result = repository.GetGeneralReport();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/QApi/GetMyReport
        [ResponseType(typeof(MyReportModel))]
        public IHttpActionResult GetMyReport()
        {
            var result = repository.GetMyReport();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // DELETE: api/DeleteQuestionsValue  
        [ResponseType(typeof(Report))]
        public IHttpActionResult DeleteQuestionsValue()
        {
            bool isRemovied = false;
            try
            {
                repository.Removied();
                isRemovied = true;
            }
            catch
            {
                return NotFound();
            }

            return Ok(isRemovied);
        }
        
    }
}
