using ef_core_exam.Models;
using ef_core_exam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core_exam.Utils
{
    public class BookUtils
    {
        static public void PrintBooks()
        {
            using (var service = new ServiceBookShop(new BookShop()))
            {
                var Books = service.ReadBooks();
                foreach (var book in Books)
                {
                    Console.WriteLine(book + "\n");
                }
            }
        }
        static public void CreateBook()
        {
            Console.Write("Input title: ");
            string _Title = Console.ReadLine().ToLower();
            Console.Write("Input genre: ");
            string _Genre = Console.ReadLine().ToLower();
            Console.Write("Input pages: ");
            int _Pages = Int32.Parse(Console.ReadLine());
            Console.Write("Input year: ");
            int _Year = Int32.Parse(Console.ReadLine());
            Console.Write("Input cost price: ");
            float _CostPrice = float.Parse(Console.ReadLine());
            Console.Write("Input price in store: ");
            float _Price = float.Parse(Console.ReadLine());
            using (var service = new ServiceBookShop(new BookShop()))
            {
                Console.Write("Input author last name: ");
                string lastName = Console.ReadLine().ToLower();
                Console.Write("Input author first name: ");
                string firstName = Console.ReadLine().ToLower();
                var _author = service.FindByLastName(lastName);
                if (_author == null)
                {
                    _author = new Author { LastName = lastName, FirstName = firstName };
                    service.CreateAuthor(_author);
                }

                Console.Write("Input publisher name: ");
                string publisherName = Console.ReadLine().ToLower();
                var _publisher = service.FindByPublisherName(publisherName);
                if (_publisher == null)
                {
                    _publisher = new Publisher { Name = publisherName };
                    service.CreatePublisher(_publisher);
                }
                var book = new Book
                {
                    Title = _Title,
                    Author = _author,
                    Publisher = _publisher,
                    Genre = _Genre,
                    Pages = _Pages,
                    Year = _Year,
                    CostPrice = _CostPrice,
                    Price = _Price,
                };
                service.CreateBook(book);
                _author.Books.Add(book);
                _publisher.Books.Add(book);
            }
        }
        static public void DeleteBook()
        {
            Console.Write("Input book id:");
            int bookId = Int32.Parse(Console.ReadLine());
            using (var service = new ServiceBookShop(new BookShop())) 
            { 
                var book = service.FindBookById(bookId); 
                if (book != null) 
                { 
                    var author = book.Author; 
                    var publisher = book.Publisher; 
                    if (author != null && author.Books.Contains(book)) 
                    { 
                        author.Books.Remove(book); 
                    } 
                    if (publisher != null && publisher.Books.Contains(book))
                    { 
                        publisher.Books.Remove(book); 
                    } 
                    service.DeleteBookById(bookId); 
                    Console.WriteLine($"Book '{book.Title}' has been deleted."); 
                } 
                else  Console.WriteLine("Book not found");
            }
        }
        static public void UpdateBook() 
        {
            using (var service = new ServiceBookShop(new BookShop())) 
            { 
                Console.Write("Enter book ID to update: "); 
                int bookId = int.Parse(Console.ReadLine()); 
                Console.Write("Enter new title: "); 
                string title = Console.ReadLine(); 
                Console.Write("Enter new genre: "); 
                string genre = Console.ReadLine(); 
                Console.Write("Enter new pages: "); 
                int pages = int.Parse(Console.ReadLine()); 
                Console.Write("Enter new year: "); 
                int year = int.Parse(Console.ReadLine()); 
                Console.Write("Enter new cost price: "); 
                float costPrice = float.Parse(Console.ReadLine()); 
                Console.Write("Enter new price in store: "); 
                float price = float.Parse(Console.ReadLine()); 
                Console.Write("Enter new author ID: "); 
                int authorId = int.Parse(Console.ReadLine()); 
                Console.Write("Enter new publisher ID: "); 
                int publisherId = int.Parse(Console.ReadLine()); 
                service.UpdateBook(bookId, title, genre, pages, year, costPrice, price, authorId, publisherId); 
            } 
        }
        static public void SearchBooksByTitle() 
        { 
            Console.Write("Enter title to search: "); 
            string title = Console.ReadLine(); 
            using (var service = new ServiceBookShop(new BookShop())) 
            { 
                var books = service.SearchBooksByTitle(title); 
                foreach (var book in books) 
                { 
                    Console.WriteLine(book); 
                }
            }
        }
        static public void SearchBooksByAuthor() 
        { 
            Console.Write("Enter author name to search: "); 
            string authorName = Console.ReadLine(); 
            using (var service = new ServiceBookShop(new BookShop())) 
            { 
                var books = service.SearchBooksByAuthor(authorName); 
                foreach (var book in books) 
                { 
                    Console.WriteLine(book); 
                }
            }
        }
        static public void SearchBooksByGenre()
        {
            Console.Write("Enter genre to search: "); 
            string genre = Console.ReadLine(); 
            using (var service = new ServiceBookShop(new BookShop())) 
            {
                var books = service.SearchBooksByGenre(genre);
                foreach (var book in books) 
                {
                    Console.WriteLine(book); 
                }
            }
        }
        static public void ApplyDiscount()
        {
            using (var service = new ServiceBookShop(new BookShop()))
            {
                Console.Write("Enter genre for discount (Horror): ");
                string genre = Console.ReadLine().ToLower();
                Console.Write("Enter discount percentage: ");
                float discount = float.Parse(Console.ReadLine());
                service.ApplyDiscountToTheme(genre, discount);
            }
        }
        static public void ShowTop5() { 
            using (var service = new ServiceBookShop(new BookShop())) 
            {
                var popularBooks = service.GetTop5PopularBooks(); 
                Console.WriteLine("Top 5 Popular Books:"); 
                foreach (var book in popularBooks) 
                {
                    Console.WriteLine(book + "\n"); 
                }
            }
        }
    }
}
