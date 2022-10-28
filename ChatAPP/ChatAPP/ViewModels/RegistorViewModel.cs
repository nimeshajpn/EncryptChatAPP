using ChatAPP.Models;
using ChatAPP.Services;
using ChatAPP.Views;
using Home_Task.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatAPP.ViewModels
{
    public class RegistorViewModel : ViewModelBase
    {
        public ICommand RegistorCommandA { get; set; }
        public FirebaseService fr = new FirebaseService();
        public Encryption en = new Encryption();
        public Users model; 
        public RegistorViewModel()
        {
            RegistorCommandA = new MvvmHelpers.Commands.AsyncCommand(Registor);
            model = new Users();
        }
        public async Task Registor()
        {

            string _key= en.GetPublicKey();
            model = new Users {

                Name = UserName,
                Password = Password,
                Key = _key,
                PublicKey = en.GetPublic()


        };

            var result =fr.CreateUser(model);


            await Shell.Current.GoToAsync($"//{nameof(Login)}");


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
