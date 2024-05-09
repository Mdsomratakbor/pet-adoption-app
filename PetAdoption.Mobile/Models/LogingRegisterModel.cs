namespace PetAdoption.Mobile.Models
{
    public partial class LogingRegisterModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public bool isNewUser => !string.IsNullOrWhiteSpace(Name);

        public bool Validate(bool isRegistrationMode)
        {


            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                return false;

            if (isRegistrationMode && string.IsNullOrWhiteSpace(Name))
                return false;

            return true;

        }

    }
}
