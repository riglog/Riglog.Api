using System;
using System.ComponentModel.DataAnnotations;

namespace Riglog.Api.Models
{
    public class UserModel
    {
        private string _username;
        private string _email;
        private string _phone;
        private string _firstName;
        private string _lastName;

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username
        {
            get => _username;
            set => _username = value?.Trim().ToLower();
        }

        [Required]
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value?.Trim();
        }

        [Required]
        public string LastName
        {
            get => _lastName;
            set => _lastName = value?.Trim();
        }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim().ToLower();
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value?.Trim().Replace(" ", "");
        }

        public bool IsSuperAdmin { get; set; }

    }
}