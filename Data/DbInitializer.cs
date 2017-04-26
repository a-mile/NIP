using System.Linq;
using NIP.Models;

namespace NIP.Data
{
    public class DbInitializer
    {
        public static void Initialize(NIPDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Businesses.Any())
            {
                return; 
            }

            var businesses = new Business[]
            {
                new Business
                {
                    Name = "Przedsiębiorstwo handlowe", 
                    Street = "Klonowa",
                    Number = 8,
                    ZIPCode = "13-343",
                    City = "Warszawa",
                    KRS = "4566783475",
                    NIP = "3453234258",
                    REGON = "343567897"        
                },
                new Business
                {
                    Name = "Przedsiębiorstwo usługowe", 
                    Street = "Brzozowa",
                    Number = 2,
                    ZIPCode = "55-333",
                    City = "Poznań",  
                    KRS = "7111783458",                  
                    NIP = "1257233258",
                    REGON = "335439976"        
                },
                new Business
                {
                    Name = "Przedsiębiorstwo produkcyjne", 
                    Street = "Dąbrowskiego",
                    Number = 77,
                    ZIPCode = "11-333",
                    City = "Wrocław",
                    KRS = "7222847566",
                    NIP = "2039487765",
                    REGON = "960374958"        
                },
            };

            foreach(var business in businesses)
            {
                context.Businesses.Add(business);
            }

            context.SaveChanges();
        }
    }
}