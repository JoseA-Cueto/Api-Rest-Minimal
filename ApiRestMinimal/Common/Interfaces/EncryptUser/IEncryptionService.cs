namespace ApiRestMinimal.Common.Interfaces.EncryptUser
{
    public interface IEncryptionService
    {
        string EncryptPassword(string password);
    }
}
