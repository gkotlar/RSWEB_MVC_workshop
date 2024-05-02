using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookstoreApp.Models
{
    public class UserBooks
    {
        public int Id { get; set; }

        public string AppUser { get; set; }


        public int BookId { get; set; }
    }
}
