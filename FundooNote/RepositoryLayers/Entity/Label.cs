﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }

        public int? userId { get; set; }

        public virtual User User { get; set; }

        public int? NoteId { get; set; }

        public virtual Note Note { get; set; }
       
    }
}
