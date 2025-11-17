using LW4_task_3.Interface.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace LW4_task_3.Services
{
    public class PaswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool Verify(string password, string hash)
        {
            var hashPas = Hash(password);
            return hashPas == hash;
        }
    }
}
