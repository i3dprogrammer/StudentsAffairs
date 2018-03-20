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
        [NonAction]
        public void FillData(IExcelDataReader reader, int? department, int? group)
        {
            var evaluator = new DataTable(); // Evaluator for mathematics equations
            do //Loop through each sheet
            {
                while (reader.Read()) //Loop through each row if next row exists.
                {
                    // Checks if this is the first row in the sheet, or the row isn't complete, or first cell is empty.
                    if (reader.GetString(0) == "اسم الطالب" || reader.FieldCount < 17 || String.IsNullOrEmpty(reader.GetValue(0)?.ToString()))
                        continue;

                    var student = new Student();

                    try
                    {
                        // TODO: YOU SHOULD FIX THIS.
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(0)?.ToString()))
                                student.Name = reader.GetString(0);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(1)?.ToString()))
                                student.Sex = (StudentSex)Enum.Parse(typeof(StudentSex), reader.GetString(1));
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(2)?.ToString()))
                                student.Nationality = reader.GetString(2);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(3)?.ToString()))
                                student.Religion = (StudentReligion)Enum.Parse(typeof(StudentReligion), reader.GetString(3));
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(4)?.ToString()))
                                student.BirthDate = reader.GetDateTime(4);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(5)?.ToString()))
                                student.BirthPlace = reader.GetValue(5).ToString();
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(6)?.ToString()))
                                student.PersonalCardId = reader.GetValue(6).ToString();
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(7)?.ToString()))
                                student.CivilRegistry = reader.GetValue(7).ToString();
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(8)?.ToString()))
                                student.AcademicQualificationAndDate = reader.GetString(8);
                        }
                        catch { }
                        
                        try
                        {
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
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(10)?.ToString()))
                                student.Speciality = reader.GetString(10);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(11)?.ToString()))
                                student.StatusOfConstraint = reader.GetString(11);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(12)?.ToString()))
                                student.ContantMethod = reader.GetString(12);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(13)?.ToString()))
                                student.JoinDate = reader.GetDateTime(13);
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(14)?.ToString()))
                                student.PlaceOfResidence = reader.GetValue(14).ToString();
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(15)?.ToString()))
                                student.HomeNumber = GetIntFromString(reader.GetValue(15).ToString());
                        }
                        catch { }
                        
                        try
                        {
                            if (!String.IsNullOrEmpty(reader.GetValue(16)?.ToString()))
                                student.StreetNumber = reader.GetString(16);
                        }
                        catch { }
                        
                        try
                        {
                            if (department != null)
                                student.Department = (Departments)department;
                        }
                        catch { }
                        
                        try
                        {
                            if (group != null)
                                student.Group = (Groups)group;
                        }
                        catch { }

                        if (!String.IsNullOrEmpty(student?.Name))
                            db.Students.Add(student);
                    }
                    catch { }
                }
            } while (reader.NextResult());

            db.SaveChanges();
        }
        [NonAction]
        private void GetDepartmentSelectList()
        {
            ViewBag.depList = Enum.GetValues(typeof(StudentsAffairs.Models.Departments))
                    .Cast<StudentsAffairs.Models.Departments>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString().Replace("_", " ")
                    });
        } 

        public ActionResult Upload()
        {
            GetDepartmentSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file, int? department, int? group)
        {
            string[] allowedMimeTypes = new string[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.ms-excel" };
            if (file != null && file.ContentLength != 0 && department != null & group != null)
            {
                if (allowedMimeTypes.Contains(file.ContentType))
                {
                    FillData(ExcelReaderFactory.CreateReader(file.InputStream), department, group);
                    return Redirect("Index");
                }
                else
                {
                    ModelState.AddModelError("", "نوع الملف الذي اخترته خطا, تاكد من اختيار ملف بصيغة xlsx او xls.");
                }
            } else
            {
                ModelState.AddModelError("", "يرجي اختيار الملف الخاص بالطلبه وتحديد كل من الفرقة والقسم.");
            }

            GetDepartmentSelectList();
            return View();
        }

        private int GetIntFromString(string str)
        {
            string value = "0";
            str.ToList().ForEach(x =>
            {
                if (Char.IsDigit(x))
                {
                    value += x;
                }
            });
            return Int32.Parse(value);
        }

        // GET: Students
        // async to aviod query request delay leak.
        public ActionResult Index(string sort, string search, string group, int? page, string currentFilter)
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


            /* GROUP SEARCH
             * */
            
            /* END GROUP SEARCH
             * */


            // Check search process.
            students = SearchFilterationByName(search, students);
            students = SearchFilterationByGroup(group, students);
            // Set sorting process
            var studentsAsList = SortProcess(sort, students, search, page);
            // handle paging.
            var studentsAsPages = handlePaging(studentsAsList, search, sort, page);

            return View(studentsAsPages);

        }

        [NonAction]
        private IPagedList<Student> handlePaging(List<Student> students, string search, string sortOrder, int? page)
        {
            int pageSize = Constants.StudentsPerPage;
            int pageNumber = (page ?? 1);
            //saving data.
            ViewBag.CurrentFilterByName = search;
            ViewBag.PreviousPage = page;
            ViewBag.PreviousSearch = search;
            return students.ToPagedList(pageNumber, pageSize);
        }

        [NonAction]
        private IQueryable<Student> SearchFilterationByName(string searchString, IQueryable<Student> students)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(v => v.Name.StartsWith(searchString));
            }

            return students;
        }

        [NonAction]
        private IQueryable<Student> SearchFilterationByGroup(string group, IQueryable<Student> students)
        {
            if (group != null) {
                Groups selectedGroup;
                selectedGroup = (Groups)Enum.Parse(typeof(Groups), group);
                
                if (selectedGroup != Groups.الكل)
                    students = students.Where(x => x.Group == selectedGroup);
                
                ViewBag.currentFilterByDep = group;
            }
            

            return students;
        }

        [NonAction]
        private List<Student> SortProcess(string sortOrder, IQueryable<Student> students, string search, int? page)
        {
            if (!isChanged(search))
                SortSwitcher(sortOrder);

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

        [NonAction]
        private Boolean isChanged(string search)
        {
            return (ViewBag.PreviousSearcn != search);
        }

        [NonAction]
        private void SortSwitcher(string sortOrder)
        {
            ViewBag.CurrentSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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
        public ActionResult Create([Bind(Include = "ID,Name,Sex,Nationality,Religion,BirthDate,BirthPlace,PersonalCardId,CivilRegistry,AcademicQualificationAndDate,Total,Speciality,StatusOfConstraint,ContantMethod,JoinDate,PlaceOfResidence,HomeNumber,StreetNumber,OtherNotes,Department,Group")] Student student)
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
        public ActionResult Edit([Bind(Include = "ID,Name,Sex,Nationality,Religion,BirthDate,BirthPlace,PersonalCardId,CivilRegistry,AcademicQualificationAndDate,Total,Speciality,StatusOfConstraint,ContantMethod,JoinDate,PlaceOfResidence,HomeNumber,StreetNumber,OtherNotes,Department,Group")] Student student)
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
