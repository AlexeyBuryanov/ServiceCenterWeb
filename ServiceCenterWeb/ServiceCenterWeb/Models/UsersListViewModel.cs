using System.Collections.Generic;
using System.Linq;

namespace ServiceCenterWeb.Models
{
    public class UsersListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterUserViewModel FilterUserViewModel { get; set; }
        public SortUserViewModel SortViewModel { get; set; }
    }
}
