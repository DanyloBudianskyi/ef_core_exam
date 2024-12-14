using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ef_core_exam.Models;

namespace ef_core_exam.Service
{
    public class ServiceBookShop : IDisposable
    {
        private readonly BookShop _db;
        public ServiceBookShop(BookShop db)
        {
            _db = db;
        }
        //------------Books------------
        public void CreateBook(Book book)
        {
            var _book = _db.Books.FirstOrDefault(b => b.Title == book.Title);
            if (_book == null)
            {
                _db.Books.Add(book);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("This book already exist");
            }
        }
        public void DeleteBookById(int id)
        {
            var book = _db.Books.Find(id);
            if (book != null)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Book not found");
            }
        }
        public void UpdateBook(int id, string title, string genre, int pages, int year, float costPrice, float price, int authorId, int publisherId)
        {
            var book = _db.Books.Find(id); 
            if (book != null)
            {
                book.Title = title; 
                book.Genre = genre; 
                book.Pages = pages; 
                book.Year = year; 
                book.CostPrice = costPrice; 
                book.Price = price;
                var author = _db.Authors.Find(authorId); 
                if (author != null) 
                { 
                    book.Author = author; 
                } 
                var publisher = _db.Publishers.Find(publisherId); 
                if (publisher != null) 
                { 
                    book.Publisher = publisher; 
                } 
                _db.SaveChanges(); 
                Console.WriteLine("Book updated successfully!"); 
            } 
            else 
            { 
                Console.WriteLine("Book not found"); 
            } 
        }
        public List<Book> ReadBooks() => _db.Books.Include(b => b.Author).Include(b => b.Publisher).ToList();
        
        public Book FindBookById(int id) => _db.Books.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefault(b => b.Id == id);
        public List<Book> SearchBooksByTitle(string title)
        {
            return _db.Books.Include(b => b.Author).Include(b => b.Publisher)
                            .Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();
        }
        public List<Book> SearchBooksByAuthor(string authorName)
        {
            return _db.Books.Include(b => b.Author).Include(b => b.Publisher)
                            .Where(b => b.Author.LastName.ToLower().Contains(authorName.ToLower()) || b.Author.FirstName.ToLower().Contains(authorName.ToLower())).ToList();
        }
        public List<Book> SearchBooksByGenre(string genre)
        {
            return _db.Books.Include(b => b.Author).Include(b => b.Publisher)
                            .Where(b => b.Genre.ToLower().Contains(genre.ToLower())).ToList();
        }
        public void ApplyDiscountToTheme(string genre, float discountPercentage)
        {
            var books = _db.Books.Where(b => b.Genre.ToLower() == genre);
            foreach (var book in books)
            {
                book.Price -= book.Price * (discountPercentage / 100);
                Console.WriteLine($"Book '{book.Title}' now costs {book.Price:C}");
            }
            _db.SaveChanges();
        }
        public List<Book> GetTop5PopularBooks()
        {
            return _db.Books.OrderByDescending(b => b.PopularityPoint).Take(5).ToList();
        }

        //------------Authors------------
        public void CreateAuthor(Author author)
        {
            var _author = _db.Authors.FirstOrDefault(a => a.LastName == author.LastName);
            if (_author == null)
            {
                _db.Authors.Add(author);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("This author already exist");
            }
        }
        public void DeleteAuthorById(int id)
        {
            var _author = _db.Authors.Find(id);
            if (_author != null)
            {
                _db.Authors.Remove(_author);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Author not found");
            }
        }
        public void UpdateAuthorById(int id, Author author)
        {
            var _author = _db.Authors.Find(id);
            if (_author != null)
            {
                _author.FirstName = author.FirstName;
                _author.LastName = author.LastName;
                _author.Books = author.Books;
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Author not found");
            }
        }
        public Author FindByLastName(string lastName)
        {
            var author = _db.Authors.FirstOrDefault(n => n.LastName == lastName);
            return author;
        }
        public List<Author> ReadAuthors()
        {
            return _db.Authors.ToList();
        }
        //------------Publisher------------
        public void CreatePublisher(Models.Publisher publisher)
        {
            var _publisher = _db.Publishers.FirstOrDefault(p => p.Name == publisher.Name);
            if (_publisher == null)
            {
                _db.Publishers.Add(publisher);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("This publisher already exist");
            }
        }
        public void DeletePublisherById(int id)
        {
            var _publisher = _db.Publishers.Find(id);
            if (_publisher != null)
            {
                _db.Publishers.Remove(_publisher);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Publisher not found");
            }
        }
        public void UpdateBookById(int id, Models.Publisher publisher)
        {
            var _publisher = _db.Publishers.Find(id);
            if (_publisher != null)
            {
                _publisher.Name = publisher.Name;
                _publisher.Books = _publisher.Books;
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Publisher not found");
            }
        }
        public Models.Publisher FindByPublisherName(string name)
        {
            var publisher = _db.Publishers.FirstOrDefault(p => p.Name == name);
            return publisher;
        }
        public List<Models.Publisher> ReadPublisher()
        {
            return _db.Publishers.ToList();
        }
        //------------User------------
        public void UserRegistration(User user)
        {
            var _user = _db.Users.FirstOrDefault(u => u.Username == user.Username);
            if(_user == null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("This user already exist");
            }
        }
        public bool UserCheck(string username, string password)
        {
            var _user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (_user != null)
            {
                _user.Password = password;
                return true;
            }

            else return false;
        }
        public User GetUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }

        //------------Purchase------------
        public void RecordPurchase(int bookId, int userId)
        {
            var book = _db.Books.Find(bookId);
            var user = _db.Users.Find(userId);
            if (book != null && user != null)
            {
                var purchase = new Purchase
                {
                    BookId = bookId,
                    Book = book,
                    UserId = userId,
                    User = user,
                    PurchaseDate = DateTime.Now
                };
                if (user.Purchases == null) 
                {
                    user.Purchases = new List<Purchase>(); 
                }
                user.Purchases.Add(purchase);
                _db.Purchases.Add(purchase);
                book.PopularityPoint++;
                _db.SaveChanges();
            }
        }
        public List<Purchase> GetPurchaseHistory(int userId)
        {
            return _db.Purchases.Include(p => p.Book).ToList();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
