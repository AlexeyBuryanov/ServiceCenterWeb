using ServiceCenterWeb.Models.Database;

namespace ServiceCenterWeb.Models
{
    public class PersonalCabinetViewModel
    {
        public User CurrentUser { get; set; } = new User();
        public Client CurrentClient { get; set; } = new Client();
        public Master CurrentMaster { get; set; } = new Master();
    }
}