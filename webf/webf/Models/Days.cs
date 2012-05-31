using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webf.Models
{
    public class Days
    {
        
        private Dictionary<int,string> DaysOfWeek { get; set; }
        public SelectList List { get; set; }
        public int CurrentDay { get; set; }

        public Days()
        {
            DaysOfWeek = new Dictionary<int, string>()
                             {
                                 {0,"понедельник"},
                                 {1,"вторник"},
                                 {2,"среда"},
                                 {3,"четверг"},
                                 {4,"пятьница"},
                                 {5,"суббота"},
                                 {6,"воскресение"}
                             };
            List = new SelectList(DaysOfWeek, "Key", "Value");
        }

    }
}