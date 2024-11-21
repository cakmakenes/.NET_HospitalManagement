using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }
        [DisplayName("User Name")]
        public string UserName => Record.UserName;
        public string Password => Record.Password;
        public string isActive => Record.isActive ? "Active" : "Inactive";
        public string Role => Record.Role?.Name;
    }
}
