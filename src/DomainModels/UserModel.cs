using System;
using System.ComponentModel.DataAnnotations;
using ViewModels.Enums;

namespace DomainModels
{
    public class User
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string RealName { get; set; }

        public Genders? Gender { get; set; }

        [MaxLength(10)]
        public DateTime? BirthDay { get; set; }

        [MaxLength(20)]
        public string Education { get; set; }

        [MaxLength(100)]
        public string SchoolName { get; set; }

        public DateTime? RegisterTime { get; set; }
    }
}
