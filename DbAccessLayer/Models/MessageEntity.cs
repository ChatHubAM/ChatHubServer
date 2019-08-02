using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbAccessLayer.Entities
{
    /// <summary>
    /// Model for Message Table interaction
    /// </summary>
    public class MessageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //attribute makes the Message Id auto generated in Db
        public int Id { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        public string MessageText { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }
}
