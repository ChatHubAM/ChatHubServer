using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccessLayer.Entities
{
    /// <summary>
    /// Model for User Table interaction
    /// </summary>
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [EmailAddress]
		public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string IPLocal { get; set; }
        

        public UserEntity()
        {
        }

        public UserEntity(int id, string fullName, string email, string username, string password)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
	}
}
