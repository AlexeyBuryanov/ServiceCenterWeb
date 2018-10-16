namespace ServiceCenterWeb.Models
{
    public class SortUserViewModel
    {
        public SortUserState UserNameSort { get; }
        public SortUserState EmailSort { get; }
        public SortUserState ConfirmEmailSort { get; }
        public SortUserState PhoneNumberSort { get; }
        public SortUserState ConfirmPhoneNumberSort { get; }
        public SortUserState TwoFaSort { get; }
        public SortUserState LockoutEndSort { get; }
        public SortUserState LockoutEnabledSort { get; }
        public SortUserState AccessFailedCountSort { get; }

        // Текущее значение сортировки
        public SortUserState Current { get; }

        public SortUserViewModel(SortUserState sortOrder)
        {
            UserNameSort = sortOrder == SortUserState.UserNameAsc ? SortUserState.UserNameDesc : SortUserState.UserNameAsc;
            EmailSort = sortOrder == SortUserState.EmailAsc ? SortUserState.EmailDesc : SortUserState.EmailAsc;
            ConfirmEmailSort = sortOrder == SortUserState.ConfirmEmailAsc ? SortUserState.ConfirmEmailDesc : SortUserState.ConfirmEmailAsc;
            PhoneNumberSort = sortOrder == SortUserState.PhoneNumberAsc ? SortUserState.PhoneNumberDesc : SortUserState.PhoneNumberAsc;
            ConfirmPhoneNumberSort = sortOrder == SortUserState.ConfirmPhoneNumberAsc ? SortUserState.ConfirmPhoneNumberDesc : SortUserState.ConfirmPhoneNumberAsc;
            TwoFaSort = sortOrder == SortUserState.TwoFaAsc ? SortUserState.TwoFaDesc : SortUserState.TwoFaAsc;
            LockoutEndSort = sortOrder == SortUserState.LockoutEndAsc ? SortUserState.LockoutEndDesc : SortUserState.LockoutEndAsc;
            LockoutEnabledSort = sortOrder == SortUserState.LockoutEnabledAsc ? SortUserState.LockoutEnabledDesc : SortUserState.LockoutEnabledAsc;
            AccessFailedCountSort = sortOrder == SortUserState.AccessFailedCountAsc ? SortUserState.AccessFailedCountDesc : SortUserState.AccessFailedCountAsc;

            Current = sortOrder;
        }
    }
}
