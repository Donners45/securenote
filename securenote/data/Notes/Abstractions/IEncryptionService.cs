using domain;

namespace data.Notes.Abstractions
{
    public interface IEncryptionService
    {
        string Encrypt(string message);
        string Decrypt(string cipher);
    }
}