
using ChatAPP.Models;
using Firebase.Database;
using Firebase.Database.Query;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Home_Task.Services
{
    public class FirebaseService
    {






        public Users userget;

        FirebaseClient fr;
       public List<Users> users = new List<Users>();
        public FirebaseService()
        {
            fr = new FirebaseClient("https://home-task-2ba21-default-rtdb.firebaseio.com/");
            userget = new Users();
        }


        public async Task<bool> CreateUser(Users user) {


            await fr.Child("ChUsers").PostAsync(JsonConvert.SerializeObject(user));
            




            return true;
        }  public async Task<bool> CreateMsg(Message msg) {


            await fr.Child("ChMsg").PostAsync(JsonConvert.SerializeObject(msg));
            




            return true;
        } 
        
        

       

        public async Task<bool> LoginUser(string Username, string Password) {

            var a = (await fr.Child("ChUser").OnceAsync<Users>()).Where(u => u.Object.Name == Username).Where(u => u.Object.Password == Password).FirstOrDefault();

            if (a == null)
            {
                return false;
                    
            }

            return true ;
        }
        public async Task<List<Users>> GetAll() {

            return (await fr.Child("ChMsg").OnceAsync<Users>()).Select(item => new Users
            {
               Name = item.Object.Name,
           
             
               Password = item.Object.Password,
                




            }).ToList();
        
        
        }
        public async Task<List<Message>> GetAlMsg() {

            return (await fr.Child("ChMsg").OnceAsync<Message>()).Select(item => new Message
            {
               Name = item.Object.Name,
           
               Date = item.Object.Date,
               Msg = item.Object.Msg,

                




            }).ToList();
        
        
        }



        public async Task<Users> GetById(string UserName)
        {


            try
            {

                userget = (await fr.Child("ChUsers").OnceAsync<Users>()).Where(u => u.Object.Name == UserName).Select(item => new Users
                {
                   Name=item.Object.Name,
                   Key = item.Object.Key,
                   Password = item.Object.Password,

                }).First();




                return userget;
            }
            catch (Exception e)
            {

                _ = Application.Current.MainPage.DisplayAlert("Error", e.ToString(), "Ok");
            }

            return null;

        }
        public async Task<Message> GetByMsg(string UserName) {


            try
            {
              
               Message us = (await fr.Child("ChUser").OnceAsync<Message>()).Where(u => u.Object.Name == UserName).Select(item => new Message
               {
                   Name = item.Object.Name,
                   Date = item.Object.Date,
                   Msg = item.Object.Msg,
                  

                   


               }).First();




                return us;
            }
            catch (Exception e )
            {

                _ = Application.Current.MainPage.DisplayAlert("Error", e.ToString(), "Ok");
            }
           
            return null;
           
        }

        public async Task<bool> Update(string UserName,Users user)
        {


            var a = (await fr.Child("ChUser").OnceAsync<Users>()).Where(u => u.Object.Name == UserName).FirstOrDefault();

            await fr.Child("ChUser").Child(a.Key).PutAsync(JsonConvert.SerializeObject(user));

            return true;
        
        }

    }
}
