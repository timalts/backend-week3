using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_week3.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string grade { get; set; }

    }
}
