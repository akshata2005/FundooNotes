﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime registerdDate { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public virtual IList<Note> Notes { get; set; }
        public virtual ICollection<Label> Labels { get; set;}
       
    }
}
