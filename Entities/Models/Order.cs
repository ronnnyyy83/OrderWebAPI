using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("orderId")]
        public int OrderId { get; set; }

        [Column("orderNo")]
        [Required(ErrorMessage = "Order No is required")]
        [StringLength(100, ErrorMessage = "Order No can't be longer than 100 characters")]
        public string OrderNo { get; set; }

        [Column("price")]
        [Required(ErrorMessage = "Order No is required")]
        public decimal Price { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Column("lastName")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, ErrorMessage = "Last Name can't be longer than 100 characters")]
        public string LastName { get; set; }

        [Column("postCode")]
        [Required(ErrorMessage = "Post Code is required")]
        [StringLength(10, ErrorMessage = "Post Code can't be longer than 10 characters")]
        public string PostCode { get; set; }

        [Column("houseNumber")]
        [Required(ErrorMessage = "House Number is required")]
        [StringLength(10, ErrorMessage = "House Number can't be longer than 10 characters")]
        public string HouseNumber { get; set; }

        [Column("street")]
        [Required(ErrorMessage = "Street is required")]
        [StringLength(100, ErrorMessage = "Street can't be longer than 100 characters")]
        public string Street { get; set; }

        [Column("city")]
        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City can't be longer than 100 characters")]
        public string City { get; set; }
    }
}
