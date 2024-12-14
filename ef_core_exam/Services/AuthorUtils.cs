using ef_core_exam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core_exam.Utils
{
    public class AuthorUtils
    {
        static public void PrintAuthors()
        {
            Console.WriteLine("All authors:\n");
            using (var service = new ServiceBookShop(new BookShop()))
            {
                var Authors = service.ReadAuthors();
                foreach (var auth in Authors)
                {
                    Console.WriteLine(auth + "\n");
                    if (auth.Books != null)
                    {
                        foreach (var book in auth.Books)
                        {
                            Console.WriteLine($"\n{book.Title}");
                        }
                    }
                }
            }
        }
    }
}
