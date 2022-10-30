using ChatAPP.Models;
using ChatAPP.Views;
using Home_Task.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChatAPP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommandA { get; set; }
        public ICommand RegistorCommandA { get; set; }
        public FirebaseService fr = new FirebaseService();
        public Users model;
        public LoginViewModel()
        {
            LoginCommandA = new MvvmHelpers.Commands.AsyncCommand(Login);
            RegistorCommandA = new MvvmHelpers.Commands.AsyncCommand(Registor);
            model = new Users();
        }



        public async Task Login() {

          model = await fr.GetById(UserName);

            if(model.Password!= null)
            {
                if (model.Password == Password)
                {
                    Preferences.Set("UserName", UserName);
                    await Shell.Current.GoToAsync($"//{nameof(User)}");


                }
                else {

                    _ = Application.Current.MainPage.DisplayAlert("Error","Can not Login", "Ok");


                }

            }

           



           


            

            
        
        }

        public async Task Registor() {


            await Shell.Current.GoToAsync($"//{nameof(RegistorPage)}");


        }
        public string username;
        public string password;
        public string result;
        public string UserName
        {

            get => username;
            set => username = value;


        }
        public string Password
        {

            get => password;
            set => password = value;


        }
    }
}
