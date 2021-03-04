using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace backend_week3.DTO
{
    public class AddStudent
    {
        [Required]
        public string grade { get; set; }
        [Required]
        public int students_id { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string country { get; set; }
    }
}
