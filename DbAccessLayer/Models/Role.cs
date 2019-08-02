using System;
using System.Collections.Generic;
using System.Text;

namespace DbAccessLayer.Models
{
    /// <summary>
    /// Model for Role Table Interaction
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Permission { get; set; }
    }
}
