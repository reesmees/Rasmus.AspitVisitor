using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rasmus.AspitVisitor.Business;
using Rasmus.AspitVisitor.DataAccess.EF;

namespace Rasmus.AspitVisitor.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public static AspitVisitorContext db = new AspitVisitorContext();
        public static DataHandler handler = new DataHandler(db);
        public static Admin testAdmin = new Admin { name = "TestAdminName" };
        public static Answer testAnswer = new Answer { answerBody = "TestAnswerBody" };
        public static AspitDepartment testDepartment = new AspitDepartment { departmentName = "TestName", city = "TestCity", streeName = "TestStreet", floor = 0, houseNumber = 0, zipCode = 0};
        public static Visitor testVisitor = new Visitor { firstName = "Test", lastName = "Test", age = 18, municipality = "København"};
        public static Visit testVisit = new Visit { AspitDepartment = testDepartment, Visitor1 = testVisitor, visitStartTime = new DateTime(2018, 5, 5), visitEndTime = DateTime.Now, visitorWantsToStudyAtAspit = false };

        
        [TestMethod]
        public void AddDepartment()
        {
            handler.Creator.CreateAspitDepartment(testDepartment.departmentName, testDepartment.streeName, testDepartment.houseNumber, testDepartment.floor, testDepartment.zipCode, testDepartment.city);
        }

        [TestMethod]
        public void AddVisitor()
        {
            handler.Creator.CreateVisitor(testVisitor.firstName, testVisitor.lastName, testVisitor.age, testVisitor.municipality);
        }

        [TestMethod]
        public void AddVisit()
        {
            handler.Creator.CreateVisit(testVisit.visitStartTime, testVisit.visitEndTime, testVisit.Visitor1, testVisit.AspitDepartment, testVisit.visitorWantsToStudyAtAspit);
        }

        [TestMethod]
        public void AddAdmin()
        {
            handler.Creator.CreateAdmin("TestName2");
        }

        [TestMethod]
        public void AddQuestionaire()
        {
            handler.Creator.CreateQuestionaire(new Admin { name = "TestName2" }, true);
        }
    }
}
