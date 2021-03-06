﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAffairs.Models
{
    public enum Departments
    {
        العلوم_الفيزيائية,
        الرياضيات_والفيزياء_والفلك,
        الرياضيات,
        الفيزياء,
        الفيزياء_علوم_الحاسب,
        الرياضيات_علوم_الحاسب,
        علوم_فضاء,
        احصاء_وعلوم_الحاسب,
        الفيزياء_حيوية_طبية,
        الفيزياء_والكيمياء,
        العلوم_البيولوجيه,
        علوم_الارض,
        بايوتكنولوجي,
        الكيمياء,
        الكيمياء_والكيمياء_التطبيقية,
        الكيمياء_و_الكيمياء_الحيويه,
        الكيمياء_والنبات,
        الكيمياء_وعلم_الحيوان,
        النبات,
        علم_الحيوان,
        الميكروبيولوجي,
        الميكروبيولوجي_والكيمياء_الحيويه,
        الجيولوجيا,
        الجيوفيزياء,
        أخر,
    }

    public enum Groups
    {
        الأولي = 0,
        الثانيه= 1,
        الثالثه= 2,
        الرابعه= 3
    }

    public enum GroupsFilter
    {
        الكل = 4,
        الأولي = 0,
        الثانيه = 1,
        الثالثه = 2,
        الرابعه = 3
    }
}