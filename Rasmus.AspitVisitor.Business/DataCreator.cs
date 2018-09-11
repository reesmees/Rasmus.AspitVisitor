using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.AspitVisitor.DataAccess.EF;

namespace Rasmus.AspitVisitor.Business
{
    /// <summary>
    /// Class responsible for creating data and adding it to the database
    /// </summary>
    public class DataCreator
    {
        private AspitVisitorContext db;

        public DataCreator(AspitVisitorContext dB)
        {
            DB = dB;
        }

        public AspitVisitorContext DB
        {
            get { return db; }
            set { db = value; }
        }

        public void CreateAdmin(string adminName)
        {
            DB.Admins.Add(new Admin { name = adminName });
            DB.SaveChanges();
        }

        public void CreateAnswer(string answerBody, Question question, Visitor visitor)
        {
            Answer answer = new Answer { answerBody = answerBody };
            DB.Visitors.Where(v => v.lastName == visitor.lastName && v.firstName == visitor.firstName).Single().Answers.Add(answer);
            DB.Questions.Where(q => q.questionBody == question.questionBody).Single().Answers.Add(answer);
            DB.SaveChanges();
        }

        public void CreateAspitDepartment(string departmentName, string streetName, int houseNumber, int floor, int zipCode, string city)
        {
            DB.AspitDepartments.Add(new AspitDepartment { departmentName = departmentName, streeName = streetName, houseNumber = houseNumber, floor = floor, zipCode = zipCode, city = city });
            DB.SaveChanges();
        }

        public void CreateVisitor(string firstName, string lastName, int age, string municipality)
        {
            DB.Visitors.Add(new Visitor { firstName = firstName, lastName = lastName, age = age, municipality = municipality });
            DB.SaveChanges();
        }

        public void CreateVisit(DateTime startTime, DateTime endTime, Visitor visitor, AspitDepartment department, bool wantToApply)
        {
            Visit visit = new Visit { visitEndTime = endTime, visitStartTime = startTime, visitorWantsToStudyAtAspit = wantToApply };
            DB.Visitors.Where(v => v.firstName == visitor.firstName && v.lastName == visitor.lastName).First().Visits.Add(visit);
            DB.AspitDepartments.Where(d => d.departmentName == department.departmentName).Single().Visits.Add(visit);
            DB.SaveChanges();
        }

        public void CreateQuestionaire(Admin creator, bool isActive)
        {
            Questionaire questionaire = new Questionaire { isActive = isActive };
            DB.Admins.Where(a => a.name == creator.name).Single().Questionaires.Add(questionaire);
            DB.SaveChanges();
        }

        public void CreateQuestion(Questionaire questionaire, string questionBody, bool isMultipleChoice)
        {
            Question question = new Question { questionBody = questionBody, isMultipleChoice = isMultipleChoice };
            DB.Questionaires.Where(q => q.Admin.name == questionaire.Admin.name).Single().Questions.Add(question);
            DB.SaveChanges();
        }

        public void CreateMultipleChoiceAnswer(string choiceBody)
        {
            DB.MultipleChoiceAnswers.Add(new MultipleChoiceAnswer { choiceBody = choiceBody });
            DB.SaveChanges();
        }

        public void CreateQuestionVsMultipleChoiceAnswer(Question question, MultipleChoiceAnswer answer)
        {
            QuestionsVsMultipleChoiceAnswer questionVsAnswer = new QuestionsVsMultipleChoiceAnswer();
            DB.Questions.Where(q => q.questionBody == question.questionBody).Single().QuestionsVsMultipleChoiceAnswers.Add(questionVsAnswer);
            DB.MultipleChoiceAnswers.Where(m => m.choiceBody == answer.choiceBody).Single().QuestionsVsMultipleChoiceAnswers.Add(questionVsAnswer);
            DB.SaveChanges();
        }
    }
}
