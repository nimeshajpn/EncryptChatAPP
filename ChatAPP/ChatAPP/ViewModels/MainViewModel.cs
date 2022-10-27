using ChatAPP.Models;
using ChatAPP.Services;
using Home_Task.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace ChatAPP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SendMethod { get; set; }
        public ObservableCollection<Message> msgList { get; set; }
        public ObservableCollection<Message> ownmsgList { get; set; }

        public FirebaseService fr = new FirebaseService();
        public Encryption  en  = new Encryption();
        public MainViewModel()
        {
            SendMethod = new MvvmHelpers.Commands.AsyncCommand(SendMsg);
            msgList = new ObservableCollection<Message>();
            ownmsgList = new ObservableCollection<Message>();

            _ = Loading();

        }
        public string _msg;
        public DateTime date = DateTime.Now;
        public string Name;
        public string _Entry;
        public Boolean result;
        public Boolean own;

        public Boolean Own
        {

            get => own;
            set => own = value;



        }
        public string Msg
        {

            get => _msg;
            set => _msg = value;



        }
        public string Entry
        {

            get => _Entry;
            set => _Entry = value;



        }
        public async Task SendMsg()
        {
             
            string Date = DateTime.Now.ToString();
            this.Name = Preferences.Get("UserName",""); ;
            string enname = en.Encrypt(Name);
            string enmsg = en.Encrypt(Msg);
            string endate = en.Encrypt(Date) ;
            Message msg = new Message()
            {

                Name = enname,
                Msg = enmsg,
                

            };

           
            _ = fr.CreateMsg(msg);
           
            _ = Loading();

          


        }
        async Task Loading()
        {
            msgList.Clear();
            var ab = await fr.GetAlMsg();

            foreach (var msg in ab)
            {
                     msg.Name= en.Decrypt(msg.Name);
                     msg.Msg= en.Decrypt(msg.Msg);
                msgList.Add(msg);

                   
               
                

            }
        }
    }

}