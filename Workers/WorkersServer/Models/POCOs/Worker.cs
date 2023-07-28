using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkersServer.Models.POCOs
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(256)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string MiddleName { get; set; }

        public DateTime Birthday { get; set; }

        public bool Sex { get; set; }

        public bool HaveChildren { get; set; }
    }
}
