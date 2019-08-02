using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbAccessLayer.Models
{
    public class VideoToFrom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //attribute makes the Message Id auto generated in Db
        public int Id { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        public bool IsConnected { get; set; }
    }
}
