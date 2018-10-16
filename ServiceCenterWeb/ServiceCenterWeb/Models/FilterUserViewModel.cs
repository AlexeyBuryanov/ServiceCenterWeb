namespace ServiceCenterWeb.Models
{
    public class FilterUserViewModel
    {
        public string SelectedEmail { get; }

        public FilterUserViewModel(string email)
        {
            SelectedEmail = email;
        }
    }
}
