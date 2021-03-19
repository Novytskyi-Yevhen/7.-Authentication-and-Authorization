using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAuthenticationAuthorization.Models.ModelsForAuthorization
{
    public enum BuyerType
    {
        None, Regular, Golden, Wholesale
    }
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public BuyerType BuyerType { get; set; } = BuyerType.None;
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
