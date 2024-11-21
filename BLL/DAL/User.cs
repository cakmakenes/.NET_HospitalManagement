using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string UserName { get; set; }
        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        public bool isActive { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
