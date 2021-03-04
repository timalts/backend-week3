using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_week3.DTO
{
    public class StudentDTO
    {
        public int id { get; set; }
        public string grade { get; set; }
        public int students_id { get; set; }
        public int age { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string country { get; set; }

    }
}
