using System;
using System.ComponentModel.DataAnnotations;

namespace Challange.Data.DTO
{
    public class UserDTO
    {
        [Key]
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Employment { get; set; }
        public string? KeySkill { get; set; }
        public string? AddressId { get; set; }
        public DateTime CreationDate { get; set; }
        public AddressDTO? Address { get; set; }
    }

    public class AddressDTO
    {
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public string? Zipcode { get; set; }
    }
}
