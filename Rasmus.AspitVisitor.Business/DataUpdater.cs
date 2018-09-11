using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.AspitVisitor.DataAccess.EF;

namespace Rasmus.AspitVisitor.Business
{
    public class DataUpdater
    {
        private AspitVisitorContext db;

        public DataUpdater(AspitVisitorContext dB)
        {
            DB = dB;
        }

        public AspitVisitorContext DB
        {
            get { return db; }
            set { db = value; }
        }

        public void UpdateAdmin(Admin oldAdmin, string adminName)
        {
            var admin = db.Admins.Where(a => a == oldAdmin).Single();
            admin.name = adminName;
            db.SaveChanges();
        }

        public void UpdateAnswer(Answer oldAnswer, string answerBody, Question question, Visitor visitor)
        {
            var answer = db.Answers.Where(a => a == oldAnswer).Single();
            answer.answerBody = answerBody;
            answer.Question = question;
            answer.Visitor1 = visitor;
            db.SaveChanges();
        }
    }
}
