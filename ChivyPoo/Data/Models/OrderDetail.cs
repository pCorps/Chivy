using System.ComponentModel.DataAnnotations;
namespace ChivyPoo.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

//don't need forigne key when setup this way
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        //note do not delete products,  because they are required in the oder details.   instead make them inactive

        [Required]
        public int Count { get; set; }

        [Required]
        public double Price { get; set; }
//store purchase price,  this could change in future

        [Required]
        public string ProductName { get; set; }
    }
}