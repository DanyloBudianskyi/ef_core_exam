using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ef_core_exam.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public string Genre { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }
        public float CostPrice { get; set; }
        public float Price {  get; set; }
        public int PopularityPoint { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Author: {(Author != null ? Author.ToString() : "Unknown")}\n" +
                $"Publisher: {(Publisher != null ? Publisher.ToString() : "Unknown")}\n" +
                $"Genre: {Genre}, Pages: {Pages}, Year: {Year}, Price: {Price}";
        }
    }
}
