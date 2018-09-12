using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.AspitVisitor.DataAccess.EF;

namespace Rasmus.AspitVisitor.Business
{
    /// <summary>
    /// Class responsible for reading certain data from the database
    /// </summary>
    public class DataReader
    {
        private AspitVisitorContext db;

        public DataReader(AspitVisitorContext dB)
        {
            DB = dB;
        }

        public AspitVisitorContext DB
        {
            get { return db; }
            set { db = value; }
        }

        public int CountAllVisits()
        {
            return db.Visits.Count();
        }

        public int CountVisitsByDepartment(AspitDepartment department)
        {
            return db.Visits.Where(v => v.AspitDepartment.departmentName == department.departmentName).Count();
        }

        public int CountPotentialStudents()
        {
            return db.Visits.Where(v => v.visitorWantsToStudyAtAspit == true).Count();
        }

        public int CountPotentialStudentsByDepartment(AspitDepartment department)
        {
            return db.Visits.Where(v => v.AspitDepartment.departmentName == department.departmentName && v.visitorWantsToStudyAtAspit == true).Count();
        }

        public int CalculateAgeSpread()
        {
            int minVistorAge = db.Visitors.OrderBy(v => v.age).FirstOrDefault().age;
            int maxVisitorAge = db.Visitors.OrderByDescending(v => v.age).FirstOrDefault().age;
            return maxVisitorAge - minVistorAge;
        }

        public double CalculateAverageVisitorAge()
        {
            return db.Visitors.Select(v => v.age).Average();
        }

        public int CountVisitsByDepartmentAndDate(AspitDepartment department, DateTime date)
        {
            return db.Visits.Where(v => v.AspitDepartment.departmentName == department.departmentName && v.visitStartTime.Date == date.Date).Count();
        }

        public int CountNumberOfAnsweredQuestionaires()
        {
            return db.Visitors.Where(v => v.Answers.Count > 0).Count();
        }

        public int CountNumberOfMunicipalities()
        {
            return db.Visitors.Select(v => v.municipality).Distinct().Count();
        }

        public int CountNumberOfVisitsByMunicipality(string municipality)
        {
            return db.Visitors.Where(v => v.municipality == municipality).Count();
        }
    }
}
