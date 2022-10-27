using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ChatAPP.Services
{
    public class Encryption
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private static RSAParameters _privateKey;
        private static RSAParameters _publicKey;

        public Encryption()
        {
            _privateKey = csp.ExportParameters(false);
            _publicKey=csp.ExportParameters(true);
            
        }

        public string GetPublicKey() 
        {
            var a = new StringWriter();
            var b = new XmlSerializer(typeof(RSAParameters));
            b.Serialize(a, _publicKey);
        
            return a.ToString();
        }
        public string SetPublicKey()
        {




            var a = new StringWriter();
            var b = new XmlSerializer(typeof(RSAParameters));
            b.Serialize(a, _publicKey);

            //  return a.ToString();
            return _publicKey.ToString();
        }

        public string Encrypt(string Text) 
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_privateKey);
            var data = Encoding.Unicode.GetBytes(Text);
            var en = csp.Encrypt(data,false);
        
        return Convert.ToBase64String(en);
        }

        public string Decrypt(string Text)
        {

            try
            {
                var data = Convert.FromBase64String(Text);
                csp.ImportParameters(_publicKey);
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
