using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ChatAPP.Services
{
    public class EncryptionS
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private static RSAParameters _privateKey;
        


        public EncryptionS()
        {
            _privateKey = csp.ExportParameters(true);
         

        }

    
        public RSAParameters GetPrivate()
        {
            return _privateKey;

        }

       



        public string Encrypt(string Text, RSAParameters privateKey)
        {



            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privateKey);
            var data = Encoding.Unicode.GetBytes(Text);
            var en = csp.Encrypt(data, false);

            return Convert.ToBase64String(en);
        }

        public string Decrypt(string Text, RSAParameters privateKey)
        {

            try
            {
                var data = Convert.FromBase64String(Text);
                csp.ImportParameters(privateKey);
                var en = csp.Decrypt(data, false);



                return Encoding.Unicode.GetString(en);
            }
            catch (Exception e)
            {
                string msg = "Encrypt";
                return msg;
            }
        }
    }
}

