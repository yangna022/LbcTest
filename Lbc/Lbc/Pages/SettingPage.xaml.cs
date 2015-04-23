
using Lbc.WebApi.Modes;
using Xamarin.Forms;

namespace Lbc.Pages {
    public partial class SettingPage : ContentPage {

        public Token Token {
            get;
            set;
        }

        public SettingPage() {
            InitializeComponent();

            this.Token = PropertiesHelper.GetToken();

            this.BindingContext = this;
        }

        private async void SignOut(object sender, System.EventArgs e) {
            PropertiesHelper.Remove("Token");
            await PropertiesHelper.Save();
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
