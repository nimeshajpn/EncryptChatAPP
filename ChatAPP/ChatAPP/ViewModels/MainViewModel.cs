using ChatAPP.Models;
using ChatAPP.Services;
using Home_Task.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace ChatAPP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SendMethod { get; set; }
        public ICommand RefreshCommand { get; set; }
       
        public ObservableCollection<Message> msgList { get; set; }
        public ObservableCollection<Message> ownmsgList { get; set; }

        public FirebaseService fr = new FirebaseService();
        public Encryption  en  = new Encryption();
        public EncryptionS  ens  = new EncryptionS();

        public Users user;
        public MainViewModel()
        {
            user = new Users();

            SendMethod = new MvvmHelpers.Commands.AsyncCommand(SendMsg);
            RefreshCommand = new MvvmHelpers.Commands.Command(refresh);


            msgList = new ObservableCollection<Message>();
            ownmsgList = new ObservableCollection<Message>();
            this.Name = Preferences.Get("UserName", ""); ;
            this.ReName = Preferences.Get("ChoiceName", ""); ;
            _ = Loading();
         
        }
        void refresh() 
        {
            if (IsBusy == true)
            {

                _ = Loading();

               IsBusy = false;

            }

        }


        public string _msg;
        public DateTime date = DateTime.Now;
        public string Name;
        public string ReName;
        public bool result;
        public string visibility;
        public string visibilityTwo;

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        public string   Visibility
        {

            get => visibility;
            set => visibility = value;



        }public string VisibilityTwo
        {

            get => visibilityTwo;
            set => visibilityTwo = value;



        }
        public string Msg
        {

            get => _msg;
            set => _msg = value;



        }
        
        public async Task SendMsg()
        {
            user = await fr.GetById(ReName);
            string Date = DateTime.Now.ToString();

            
          
            string enmsg = en.Encrypt(Msg,user.PublicKey);
            string endate = en.Encrypt(Date,user.PublicKey);

            // symmetric
            //  string enmsg = ens.Encrypt(Msg,ens.GetPrivate());
            //  string endate = ens.Encrypt(Date,ens.GetPrivate());

            Message msg = new Message()
            {

                Name = Name,
                Msg = enmsg,
                

            };

           
            _ = fr.CreateMsg(msg);
           
            _ = Loading();

          


        }
        async Task Loading()
        {



            string _key = en.GetPublicKey();

        


            user = await fr.GetById(Name);
            user.Name = Name;
            user.Key = _key;
            user.PublicKey = en.GetPublic();
            // symmetric
            //   user.PublicKey = ens.GetPrivate();

            _ = fr.UpdateUser(Name, user);














            msgList.Clear();
            var ab = await fr.GetAlMsg();

           

            
            
            foreach (var msg in ab)
            {
                if (msg.Name==Name || msg.Name==ReName ) 
                {
                    msg.Msg = en.Decrypt(msg.Msg);

                    // symmetric
                 //   user = await fr.GetById(ReName);
                 //   msg.Msg = ens.Decrypt(msg.Msg,user.PublicKey);



                    msgList.Add(msg);

                    if (msg.Name == Name)
                    {
                        msg.visibility = "true";
                        msg.Revisibility = "false";



                    }
                    else
                    {

                        msg.visibility = "false";
                        msg.Revisibility = "true";

                    }


                }







            }

        } 






         }

}