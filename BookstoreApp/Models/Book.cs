using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApp.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "Year Published")]
        public int? YearPublished { get; set; }

        [Display(Name = "Number of Pages")]
        public int? NumPages { get; set; }

        public string? Description { get; set; }

        [StringLength(50)]
        public string? Publisher { get; set; }


        [Display(Name = "Front Page")]
        public string? FrontPage { get; set; }

        [Display(Name = "Download Url")]
        public string? DownloadUrl { get; set; }

        [Required]
        [Display (Name ="Author Id")]
        public int AuthorId { get; set; }

        public Author? Authors { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        [NotMapped]
        public int? getReviewsCount {
            get
            {
                if (Reviews != null && Reviews.Any())
                {
                    return Reviews.Count();
                }
                else
                {
                    return null;
                }
            }
        }
        [NotMapped]
        public double? getReviewsAverage
        {
            get
            {              
                double averageRating = 0;
                if (Reviews != null && Reviews.Any())
                {             
                    int totalSum = 0;
                    int numValidReviews = 0;

                    foreach (var item in Reviews)
                    {
                        if (item.Rating.HasValue)
                        {
                            totalSum += item.Rating.Value;
                            numValidReviews++;
                        }
                    }

                    if (numValidReviews != 0)
                    {
                        averageRating = (double)totalSum / numValidReviews;
                    }

                    return Math.Round(averageRating, 2);
                }
                else
                {
                    return null;
                }
               
            }
        }

        [Display(Name = "Genres")]
        public ICollection<BookGenre>? bookGenres { get; set; }

        public ICollection<UserBooks>? UserBooks { get;}
    }
}

