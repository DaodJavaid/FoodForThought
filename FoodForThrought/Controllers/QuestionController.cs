using FoodForThrought.Data;
using FoodForThrought.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FoodForThrought.Controllers
{
    public class QuestionController : Controller
    {

        public readonly QuestionnaireDbContext _questionnaireDbContext;

        public QuestionController(QuestionnaireDbContext questionnaireDbContext)
        {
            _questionnaireDbContext = questionnaireDbContext;
        }

        public IActionResult AddQuestion()
        {
            return View();
        }

        public IActionResult SearchandUpdateQuestion()
        {
            var SearchandUpdate_Question = _questionnaireDbContext.Question.ToList();

            return View(SearchandUpdate_Question);
        }

        public IActionResult DeleteQuestion()
        {
            var SearchandUpdate_Question = _questionnaireDbContext.Question.ToList();

            return View(SearchandUpdate_Question);
        }

        public IActionResult ViewAllQuestion()
        {
            var SearchandUpdate_Question = _questionnaireDbContext.Question.ToList();

            return View(SearchandUpdate_Question);
        }

        [HttpPost]
        public IActionResult Adding_question(QuestionnaireModel question)
        {
                    try
                    {
                         _questionnaireDbContext.Add(question);
                         _questionnaireDbContext.SaveChanges();
                        TempData["confirm"] = "Question Add Successfully";
                    }
                    catch (Exception)
                    {
                        TempData["confirm"] = "There is Some Error. user Not register";

                    }

            return RedirectToAction("AddQuestion");
        }

        [HttpPost]
        public IActionResult Deleting_question(QuestionnaireModel questions)
        {
            try
            {
                var question = _questionnaireDbContext.Question.Find(questions.old_id);
                if (question != null)
                {
                    _questionnaireDbContext.Question.Remove(question);
                    _questionnaireDbContext.SaveChanges();
                    TempData["confirm"] = "Question Deleted Successfully";
                }
                else
                {
                    TempData["confirm"] = "Question Not Found";
                }
            }
            catch (Exception)
            {
                TempData["confirm"] = "There is Some Error. Question Not Deleted";
            }

            return RedirectToAction("DeleteQuestion");
        }

        [HttpPost]
        public IActionResult UpdateQuestion(QuestionnaireModel questioning)
        {
            var delete_product = _questionnaireDbContext.Question.ToList();

            Deleting_question(questioning);

            Adding_question(questioning);

            TempData["confirm"] = "Product Update Successfully";

            return RedirectToAction("SearchandUpdateQuestion");
        }


    }
}
