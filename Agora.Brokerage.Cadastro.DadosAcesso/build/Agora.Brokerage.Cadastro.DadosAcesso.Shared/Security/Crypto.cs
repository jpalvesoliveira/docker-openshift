using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Agni Campos
/// </summary>
namespace Agora.Brokerage.Cadastro.DadosAcesso.Shared.Security
{
    public class Crypto
    {

        //public string Encrypt(string PlainText)
        //{
        //    byte[] bytes = Encoding.Default.GetBytes(PlainText);
        //    SHA1 sha = new SHA1CryptoServiceProvider();
        //    return Convert.ToBase64String(sha.ComputeHash(bytes));
        //}

        private byte[] _IV;
        private byte[] _key;
        private TripleDESCryptoServiceProvider _TripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
        private readonly string OpenIV = "5FDC21B7E4F6D10B";
        private readonly string OpenKey = "2D66AEF18F7D816C79FC382DF75406B940B9F4DF5E4E85AB";

        public Crypto()
        {
            this._key = this.HexToByte(this.OpenKey);
            this._IV = this.HexToByte(this.OpenIV);
            this._TripleDESCryptoServiceProvider.Key = this._key;
            this._TripleDESCryptoServiceProvider.IV = this._IV;
            this._TripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
            this._TripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
        }

        public string Decrypt(string EncryptText)
        {
            byte[] buffer = Convert.FromBase64String(EncryptText);
            MemoryStream stream = new MemoryStream();
            ICryptoTransform transform = this._TripleDESCryptoServiceProvider.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] bytes = stream.ToArray();
            stream2.Close();
            return Encoding.Unicode.GetString(bytes);
        }

        public string Encrypt(string PlainText)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(PlainText);
            ICryptoTransform transform = this._TripleDESCryptoServiceProvider.CreateEncryptor();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }

        private byte[] HexToByte(string hexString)
        {
            byte[] buffer = new byte[hexString.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 0x10);
            }
            return buffer;
        }


    }
}
