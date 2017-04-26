namespace NIP.Models
{
    public interface IBusinessRepository
    {
        Business FindByKRS(string KRS);
        Business FindByNIP(string NIP);
        Business FindByREGON(string REGON);
    }
}