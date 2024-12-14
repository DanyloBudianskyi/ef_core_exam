using ef_core_exam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core_exam.Utils
{
    public class PublisherUtils
    {
        static public void PrintPublishers()
        {
            Console.WriteLine("All publishers:\n");
            using (var service = new ServiceBookShop(new BookShop()))
            {
                var publishers = service.ReadPublisher();
                foreach (var publisher in publishers)
                {
                    Console.WriteLine($"Publisher: {publisher}\n" + $"Books:\n");
                    if(publisher.Books != null)
                    {
                        foreach (var book in publisher.Books) 
                        {
                            Console.WriteLine($"\n{book.Title}");
                        }
                    }
                    
                }
            }
        }
    }
}
