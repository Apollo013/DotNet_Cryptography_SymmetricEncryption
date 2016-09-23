namespace DotNet_Cryptography_SymmetricEncryption.Models
{
    public class Message
    {
        public string PlainText { get; set; }
        public string IV { get; set; }
        public string Encrypted { get; set; }
    }
}
