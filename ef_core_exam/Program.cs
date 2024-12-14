using ef_core_exam.Models;
using ef_core_exam.Service;
using ef_core_exam.Utils;
using System;

namespace ef_core_exam
{
    
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!\nDo you have an account? (yes/no)");
            string response = "";
            while (response != "yes" && response != "no")
            {
                response = Console.ReadLine();
                if (response == "yes")
                {
                    UserUtils.AuthorizeUser();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else if (response == "no") 
                { 
                    UserUtils.RegisterUser();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Wrong choice");
                }
            }
            if(UserUtils.activeUser != null) 
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Print info.\n2. Search\n3. Create new book.\n4. Update.\n5. Delete book from store.\n6. Buy book.\n7. Print all user purchases\n0. Exit");
                    response = Console.ReadLine().ToLower();
                    switch (response)
                    {
                        case "1":
                            Console.WriteLine("1. Books\n2. Authors\n3. Publishers\n4. Show top 5\n5. Go back");
                            response = Console.ReadLine().ToLower();
                            Console.Clear();
                            if (response == "1") BookUtils.PrintBooks();
                            else if (response == "2") AuthorUtils.PrintAuthors();
                            else if (response == "3") PublisherUtils.PrintPublishers();
                            else if (response == "4") BookUtils.ShowTop5();
                            else if (response == "5") break;
                            else Console.WriteLine("Wrong choice");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.WriteLine("1. By title\n2. By author\n3. By genre\n4. Go back");
                            response = Console.ReadLine().ToLower();
                            Console.Clear();
                            if (response == "1") BookUtils.SearchBooksByTitle();
                            else if (response == "2") BookUtils.SearchBooksByAuthor();
                            else if (response == "3") BookUtils.SearchBooksByGenre();
                            else if (response == "4") break;
                            else Console.WriteLine("Wrong choice");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "3":
                            BookUtils.CreateBook();
                            Console.WriteLine("Book created");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "4":
                            Console.WriteLine("1.Upate book.\n2.Set discount for books by genre\n3. Go back");
                            response = Console.ReadLine().ToLower();
                            Console.Clear();
                            if (response == "1") BookUtils.UpdateBook();
                            else if (response == "2") BookUtils.ApplyDiscount();
                            else if (response == "3") break;
                            else Console.WriteLine("Wrong choice");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "5":
                            BookUtils.DeleteBook();
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "6":
                            UserUtils.PurchaseBook();
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "7":
                            UserUtils.ViewPurchaseHistory();
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
            else { 
                return; 
            }
            Console.ReadKey();
        }
    }
}
