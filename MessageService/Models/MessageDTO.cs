using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.Models
{
    /// <summary>
    /// Model for Message Client interaction
    /// </summary>
    public class MessageDTO
    { 
        public string From { get; set; }
        public string To { get; set; }
        public string MessageText { get; set; }
    }
}
