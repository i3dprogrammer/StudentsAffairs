using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentsAffairs.DAL;
using StudentsAffairs.Models;
using PagedList.Mvc;
using PagedList;
using ExcelDataReader;
using System.Globalization;
using System.Threading.Tasks;

namespace StudentsAffairs.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private StudentsAffairsContext db = new StudentsAffairsContext();

        // GET: /Students/FillData
        // Access this function by running the website (Ctrl+F5 or F5) then
        // navigating to http://localhost:xxxx/Students/FillData where xxxx is
        // the port number.
        public ActionResult FillData()
        {
            var evaluator = new DataTable(); // Evaluator for mathematics equations

            // Read the xlsx file, make sure the patch actually exists on your system.
            using (var stream = System.IO.File.Open(@"C:\Users\VisualStudio\Desktop\file.xlsx", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do //Loop through each sheet
                    {
                        while (reader.Read()) //Loop through each row if next row exists.
                        {
                            // Checks if this is the first row in the sheet, or the row isn't complete.
                            if (reader.GetString(0) == "اسم الطالب" || reader.FieldCount < 17)
                                continue;

                            var student = new Student();

                            // TODO: YOU SHOULD FIX THIS.
                            student.Name = reader.GetString(0);
                            student.Sex = (StudentSex)Enum.Parse(typeof(StudentSex), reader.GetString(1));
                            student.Nationality = reader.GetString(2);
                            student.Religion = (StudentReligion)Enum.Parse(typeof(StudentReligion), reader.GetString(3));
                            student.BirthDate = reader.GetDateTime(4);
                            //student.BirthDate = DateTime.Parse(reader.GetString(4));//DateTime.ParseExact(reader.GetString(i++), "d", CultureInfo.InvariantCulture);
                            student.BirthPlace = reader.GetString(5);
                            student.PersonalCardId = reader.GetString(6);
                            student.CivilRegistry = reader.GetString(7);
                            student.AcademicQualificationAndDate = reader.GetString(8);
                            student.Total = (double)evaluator.Compute(reader.GetString(9), "");
                            student.Speciality = reader.GetString(10);
                            student.StatusOfConstraint = reader.GetString(11);
                            student.ContantMethod = reader.GetString(12);
                            //student.JoinDate = DateTime.Parse(reader.GetString(13)); //DateTime.ParseExact(reader.GetString(i++), "d", CultureInfo.InvariantCulture);
                            student.JoinDate = reader.GetDateTime(13);
                            student.PlaceOfResidence = reader.GetString(14);
                            student.HomeNumber = Int32.Parse(reader.GetString(15));
                            student.StreetNumber = reader.GetString(16);

                            db.Students.Add(student);
                        }
                    } while (reader.NextResult());
                }
            }
            db.SaveChanges();
            return Content("Added the data succesfully to the database.");
        }

        // GET: Students
        // async to aviod query request delay leak.
        public  ActionResult Index(string sort, string search, int? page, string currentFilter)
        {
            // Changing during paging
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter; // set previous search.
            }

            var students = from v in db.Students select v;
            // Check search process.
            students = SearchFilteration(search, students);
            // Set sorting process
            var studentsAsList =  SortingBy(sort, students);
            // handle paging.
            var studentsAsPages = handlePaging(studentsAsList, search, sort, page);

            return View(studentsAsPages);
           
        }

        [NonAction]
        private IPagedList<Student> handlePaging(List<Student> students, string search, string sortOrder, int? page)
        {
           
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //saving data.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = search;
            return students.ToPagedList(pageNumber, pageSize);
        }

        [NonAction]
        private IQueryable<Student> SearchFilteration(string searchString, IQueryable<Student> students)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(v => v.Name.Contains(searchString));
            }
            
            return students;
        }

        [NonAction]
        private List<Student> SortingBy(string sortOrder, IQueryable<Student> students)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderBy(v => v.Name);
                    break;
                // case of department.
                default:
                    students = students.OrderBy(v => v.ID);
                    break;
            }

            return students.ToList();
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Sex,Nationality,Religion,BirthDate,BirthPlace,PersonalCardId,CivilRegistry,AcademicQualificationAndDate,Total,Speciality,StatusOfConstraint,ContantMethod,JoinDate,PlaceOfResidence,HomeNumber,StreetNumber,OtherNotes")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Sex,Nationality,Religion,BirthDate,BirthPlace,PersonalCardId,CivilRegistry,AcademicQualificationAndDate,Total,Speciality,StatusOfConstraint,ContantMethod,JoinDate,PlaceOfResidence,HomeNumber,StreetNumber,OtherNotes")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
