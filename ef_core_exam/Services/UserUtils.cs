using ef_core_exam.Models;
using ef_core_exam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core_exam.Utils
{
    public class UserUtils
    {
        static public User activeUser;
        static public void RegisterUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            while (!email.Contains("@")) 
            {
                Console.WriteLine("Invalid email address."); 
                Console.Write("Enter email: "); 
                email = Console.ReadLine(); 
            }
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };
            using (var service = new ServiceBookShop(new BookShop()))
            {
                service.UserRegistration(user);
                activeUser = service.GetUserByUsername(username);
            }
        }
        static public void AuthorizeUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            using (var service = new ServiceBookShop(new BookShop()))
            {
                bool isAuthorized = service.UserCheck(username, password);
                if (isAuthorized)
                {
                    activeUser = service.GetUserByUsername(username);
                    Console.WriteLine("Authorization successful!");
                }
                else Console.WriteLine("Authorization failed!");
            }


        }
        static public void PurchaseBook()
        {
            if (activeUser != null)
            {
                using (var service = new ServiceBookShop(new BookShop()))
                {
                    Console.Write("Enter book ID to purchase: ");
                    int bookId = int.Parse(Console.ReadLine());
                    service.RecordPurchase(bookId, activeUser.Id);
                }
            }
        }
        static public void ViewPurchaseHistory()
        {
            if (activeUser != null) { using (var service = new ServiceBookShop(new BookShop())) { var purchases = service.GetPurchaseHistory(activeUser.Id); foreach (var purchase in purchases) { Console.WriteLine(purchase); } } }
            else
            {
                Console.WriteLine("No active user. Please log in.");

            }
        }
    }
}
