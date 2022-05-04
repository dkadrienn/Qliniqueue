using System;
using System.Collections.Generic;
using System.Text;

namespace Qliniqueue.Models
{

    public class Reservation
    {
        public string name { get; set; }
        public int age { get; set; }
        public string sex { get; set; }
        public string symptoms { get; set; }
        public string diseases { get; set; }
        public string allergies { get; set; }
        public bool routine { get; set; }
        public string doctorId { get; set; }
        public DateTime date { get; set; }

    }
}
