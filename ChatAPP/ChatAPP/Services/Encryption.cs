using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
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
        public static RSAParameters _publicKey;
        

        public Encryption()
        {
            _privateKey = csp.ExportParameters(true);
            _publicKey=csp.ExportParameters(false);
            
        }

        public string GetPublicKey() 
        {

            var a = new StringWriter();
            var b = new XmlSerializer(typeof(RSAParameters));
            b.Serialize(a, _publicKey);
             return a.ToString();


        }
        public RSAParameters GetPublic() {
            return _publicKey;
        
        }

        public string GetModulus()
        {
            return _publicKey.Modulus.ToString(); ;

        }
        public string GetExponent()
        {
            return _publicKey.Exponent.ToString(); ;

        }



        public string Encrypt(string Text,RSAParameters publicKey) 
        {
            
           

            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(publicKey);
            var data = Encoding.Unicode.GetBytes(Text);
            var en = csp.Encrypt(data,false);
        
        return Convert.ToBase64String(en);
        }

        public string Decrypt(string Text )
        {

            try
            {
                var data = Convert.FromBase64String(Text);
                csp.ImportParameters(_privateKey);
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
