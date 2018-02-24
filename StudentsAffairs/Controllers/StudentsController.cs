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
using System.Web.Security;

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
            return Content("NO, DATA ALREADY IN THERE :@");
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
                            if (!String.IsNullOrEmpty(reader.GetValue(0)?.ToString()))
                                student.Name = reader.GetString(0);
                            if (!String.IsNullOrEmpty(reader.GetValue(1)?.ToString()))
                                student.Sex = (StudentSex)Enum.Parse(typeof(StudentSex), reader.GetString(1));
                            if (!String.IsNullOrEmpty(reader.GetValue(2)?.ToString()))
                                student.Nationality = reader.GetString(2);
                            if (!String.IsNullOrEmpty(reader.GetValue(3)?.ToString()))
                                student.Religion = (StudentReligion)Enum.Parse(typeof(StudentReligion), reader.GetString(3));
                            if (!String.IsNullOrEmpty(reader.GetValue(4)?.ToString()))
                                student.BirthDate = reader.GetDateTime(4);
                            if (!String.IsNullOrEmpty(reader.GetValue(5)?.ToString()))
                                student.BirthPlace = reader.GetValue(5).ToString();
                            if (!String.IsNullOrEmpty(reader.GetValue(6)?.ToString()))
                                student.PersonalCardId = reader.GetValue(6).ToString();
                            if (!String.IsNullOrEmpty(reader.GetValue(7)?.ToString()))
                                student.CivilRegistry = reader.GetValue(7).ToString();
                            if (!String.IsNullOrEmpty(reader.GetValue(8)?.ToString()))
                                student.AcademicQualificationAndDate = reader.GetString(8);
                            if (!String.IsNullOrEmpty(reader.GetValue(9)?.ToString()))
                            {
                                try
                                {
                                    student.Total = reader.GetDouble(9);
                                }
                                catch
                                {
                                    student.Total = double.Parse(evaluator.Compute(reader.GetValue(9).ToString(), "").ToString());
                                }
                            }
                            if (!String.IsNullOrEmpty(reader.GetValue(10)?.ToString()))
                                student.Speciality = reader.GetString(10);
                            if (!String.IsNullOrEmpty(reader.GetValue(11)?.ToString()))
                                student.StatusOfConstraint = reader.GetString(11);
                            if (!String.IsNullOrEmpty(reader.GetValue(12)?.ToString()))
                                student.ContantMethod = reader.GetString(12);
                            if (!String.IsNullOrEmpty(reader.GetValue(13)?.ToString()))
                                student.JoinDate = reader.GetDateTime(13);
                            if (!String.IsNullOrEmpty(reader.GetValue(14)?.ToString()))
                                student.PlaceOfResidence = reader.GetValue(14).ToString();
                            if (!String.IsNullOrEmpty(reader.GetValue(15)?.ToString()))
                                student.HomeNumber = Int32.Parse(reader.GetValue(15).ToString());
                            if (!String.IsNullOrEmpty(reader.GetValue(16)?.ToString()))
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
        public ActionResult Index(int? page)
        {
            var students = db.Students.OrderBy(s => s.ID);

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
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
