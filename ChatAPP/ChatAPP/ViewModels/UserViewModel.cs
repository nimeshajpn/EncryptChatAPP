using ChatAPP.Models;
using ChatAPP.Services;
using ChatAPP.Views;
using Home_Task.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ChatAPP.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public ICommand Selected { get; set; }
        public ObservableCollection<Users> users { get; set; }

        
        public FirebaseService fr = new FirebaseService();
        public Encryption en = new Encryption();

        public Users Choice { get; set; }
        public UserViewModel()
        {

            Selected = new MvvmHelpers.Commands.Command(Select);
            users = new ObservableCollection<Users>();



            _ = Loading();
        }

        public void Select() {



            Preferences.Set("ChoiceName",Choice.Name);

             Shell.Current.GoToAsync($"//{nameof(MainPage)}");


        }
        public string choice;
       
        public string name;
        public string Name
        {

            get => name;
            set => Name = value;



        }

        async Task Loading()
        {

            string b =Preferences.Get("UserName","");


            users.Clear();
            var ab = await fr.GetAll();

            foreach (var msg in ab)
            {
                if(msg.Name != b)
                {

                    users.Add(msg);


                }






            }
        }
    }
}
