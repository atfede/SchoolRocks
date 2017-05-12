using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wispero.Web.Models
{
    public class QuestionAndAnswerModel
    {
        [Required]
        [Display(Name ="Enter a question here..")]
        public string Question { get; set; }
        
        [Required]
        [Display(Name = "Enter an answer here..")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Enter keywords separated by commas...")]
        
        //Tags field with lowercase.
        public string Tags { get; set; }

        public string LowerCaseTags
        {
            get
            {
                return Tags.ToLower();
            }
        }

    }

    public class QuestionAndAnswerItemModel : QuestionAndAnswerModel
    {
        ////LastUpdateOn field is set with DateTime.Now

        public int Id { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string LastUpdateOn { get; set; }
    }

    public class QuesitonAndAnswerEditModel : QuestionAndAnswerModel
    {
        public int Id { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
}