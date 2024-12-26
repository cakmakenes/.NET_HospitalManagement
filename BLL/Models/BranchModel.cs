using BLL.DAL;

namespace BLL.Models
{
    public class BranchModel
    {
        public Branch Record { get; set; }
        public string Name => Record.Name;
    }
}
