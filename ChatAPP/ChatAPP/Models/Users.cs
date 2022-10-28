using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatAPP.Models
{
    public class Users
    {
        public string Key { get; set; }
        public RSAParameters PublicKey { get; set; }
        public string Name { get; set; }
     
       
        public string Password { get; set; }

        public string Exponent { get; set; }
        public string Modulus { get; set; }
    }
}
