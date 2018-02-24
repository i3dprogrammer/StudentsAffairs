using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAffairs.Models
{
    public class Student
    {
        public int ID { get; set; }

        //[Required(ErrorMessage = "يجب إدخال اسم الطالب")]
        [Display(Name = "إسم الطالب")]
        [StringLength(50, ErrorMessage = "يجب ان يكون هذا الحقل بين 10 و 50 حرف", MinimumLength = 10)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "يجب اختيار نوع الطالب")]
        [Display(Name = "النوع")]
        public StudentSex Sex { get; set; }

        //[Required(ErrorMessage = "يجب إدخال جنسية الطالب")]
        [Display(Name = "الجنسيه")]
        public string Nationality { get; set; }

        //[Required(ErrorMessage = "يجب اختيار الديانة")]
        [Display(Name = "الديانه")]
        public StudentReligion Religion { get; set; }

        //[Required(ErrorMessage = "يجب ادخال تاريخ الميلاد")]
        [Display(Name = "تاريخ الميلاد")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        //[Required(ErrorMessage = "يجب ادخال جهة الميلاد")]
        [Display(Name = "جهه الميلاد")]
        public string BirthPlace { get; set; }

        //[Required(ErrorMessage = "يجب ادخال رقم البطاقة الشخصية")]
        [Display(Name = "رقم البطاقه الشخصيه ")]
        [RegularExpression("[0-9]+", ErrorMessage = "رقم البطاقة يجب ان يحتوي علي ارقام فقط")]
        public string PersonalCardId { get; set; }

        //[Required(ErrorMessage = "يجب ادخال السجل المدني")]
        [Display(Name = "السجل المدنى")]
        public string CivilRegistry { get; set; }

        //[Required(ErrorMessage = "يجب ادخال المؤهل الدراسى وتاريخه")]
        [Display(Name = "المؤهل الدراسى وتاريخه")]
        public string AcademicQualificationAndDate { get; set; }

        //[Required(ErrorMessage = "يجب ادخال المجموع")]
        [Display(Name = "المجموع")]
        [RegularExpression(@"[0-9]+(\.[0-9]*)?", ErrorMessage = "يجب ان يكون المجموع علي هيئة عدد عشري او صحيح")]
        public double? Total { get; set; }

        //[Required(ErrorMessage = "يجب ادخال التخصص")]
        [Display(Name = "التخصص")]
        public string Speciality { get; set; }

        //[Required(ErrorMessage = "يجب ادخال حاله القيد")]
        [Display(Name = "حاله القيد")]
        public string StatusOfConstraint { get; set; }

        //[Required(ErrorMessage = "يجب ادخال طريقة الاتصال")]
        [Display(Name = "طريقة الاتصال")]
        public string ContantMethod { get; set; }

        //[Required(ErrorMessage = "يجب ادخال تاريخ الالتحاق")]
        [Display(Name = "تاريخ الالتحاق")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? JoinDate { get; set; }

        //[Required(ErrorMessage = "يجب ادخال محل الاقامه (محافظه)")]
        [Display(Name = "محل الاقامه (محافظه)")]
        public string PlaceOfResidence { get; set; }

        //[Required(ErrorMessage = "يجب ادخال رقم المنزل")]
        [Display(Name = "رقم المنزل")]
        public int? HomeNumber { get; set; }

        //[Required(ErrorMessage = "يجب ادخال رقم الشارع")]
        [Display(Name = "رقم الشارع")]
        public string StreetNumber { get; set; }

        //[Required(ErrorMessage = "يجب ادخال ملاحظات اخرى")]
        [Display(Name = "ملاحظات اخرى")]
        public string OtherNotes { get; set; }

        //[Required(ErrorMessage = "يجب اختيار القسم")]
        [Display(Name="القسم")]
        public Departments Department { get; set; }
        
        //[Required(ErrorMessage = "يجب اختيار الفرقة")]
        [Display(Name="الفرقة")]
        public Groups Group { get; set; }
    }

    public enum StudentReligion
    {
        مسلم,
        مسيحى,
        مسلمة,
        مسيحية,
        أخر,
    }

    public enum StudentSex
    {
        ذكر,
        انثى,
        أخر,
    }
}