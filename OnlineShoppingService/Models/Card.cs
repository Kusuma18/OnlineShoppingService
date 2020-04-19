using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingService.Models
{
    [Table("MyCards")]
    public class MyCard
    {
        [Key]
        public int CardID { get; set; }
        public string CardName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
    public class MyCardDBContext : DbContext
    {
        public DbSet<MyCard> Cards { get; set; }
    }
}