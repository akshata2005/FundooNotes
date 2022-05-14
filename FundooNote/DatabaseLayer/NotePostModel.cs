using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer
{
    public class NotePostModel
    {
        [Required(ErrorMessage = "Title should not be empty.")]
        [RegularExpression("^[A-Z][a-z]{2,}", ErrorMessage = "Title should start with capital letters")]
        [DataType(DataType.Text)]
        public string Title { get; set; 

        [Required(ErrorMessage = "Description should not be empty")]
        [RegularExpression("^[A-Z][a-z]{4,}", ErrorMessage = "Description should start with capital letters and")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public string BGColor { get; set; }

        [Required]
        public bool IsArchive { get; set; }

        [Required]
        public bool IsReminder { get; set; }

        [Required]
        public bool IsPin { get; set; }

        [Required]
        public bool IsTrash { get; set; }
    }
}
