using FoodForThrought.Data;
using FoodForThrought.Migrations.QuestionnaireDb;
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
        public async Task<IActionResult> UpdateQuestion(QuestionnaireModel questioning)
        {
            var existingQuestion = _questionnaireDbContext.Question.FirstOrDefault(u => u.Id == questioning.old_id);


            if (existingQuestion != null)
            {
                    existingQuestion.emotion_questtion =   questioning.emotion_questtion;
                    existingQuestion.first_option      =   questioning.first_option;
                    existingQuestion.second_option     =   questioning.second_option;
                    existingQuestion.third_option      =   questioning.third_option;
                    existingQuestion.forth_option      =   questioning.forth_option;
                    existingQuestion.select_emotion    =   questioning.select_emotion;
                 
                try
                    {
                              _questionnaireDbContext.Update(existingQuestion);
                        await _questionnaireDbContext.SaveChangesAsync();
                        TempData["confirm"] = "Question Updated Successfully";
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                        {
                            ViewBag.ErrorMessage = e.InnerException.Message;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = e.Message;
                        }
                        TempData["confirm"] = "There is some error. Question not updated.";
                    }
               
            }
            else
            {
                TempData["confirm"] = "Question Not Found";
            }

            return RedirectToAction("SearchandUpdateQuestion");
        }


        public IActionResult Search(QuestionnaireModel searchquestion)
        {

            var question = _questionnaireDbContext.Question.FirstOrDefault(u => u.Id == searchquestion.old_id);


            if (question == null)
            {
                // User not found

                TempData["confirm"] = "Question not found Check Question ID Plz";
                return View();
            }

            TempData["Question"] = question.emotion_questtion;
            TempData["Option1"] = question.first_option;
            TempData["Option2"] = question.second_option;
            TempData["Option3"] = question.third_option;
            TempData["Option4"] = question.forth_option;
            TempData["Emotion"] = question.select_emotion;
            TempData["QuestionID"] = question.Id;



            // Pass the user data to the view
            return RedirectToAction("SearchandUpdateQuestion");
        }
    }
}
