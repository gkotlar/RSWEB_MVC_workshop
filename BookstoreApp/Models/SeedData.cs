using Microsoft.EntityFrameworkCore;
using System.IO;


namespace BookstoreApp.Models;
public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookstoreAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BookstoreAppContext>>()))
        {
            // Look for any books, authors, genres etc.
            if (context.Author.Any() || context.Book.Any() || context.BookGenre.Any() || context.Genre.Any() || context.Review.Any() || context.UserBooks.Any())
            {
                return;   // DB has been seeded
            }

            context.Author.AddRange(
                new Author 
                { /*Id = 1, */
                    FirstName = "Rob",
                    LastName = "Reiner",
                    BirthDate = DateTime.Parse("1947-3-6"),
                    Nationality = "American",
                    Gender = "Male" 
                },
                new Author
                {   /*Id = 2, */
                    FirstName = "Ahmed",
                    LastName = "Dennis",
                    BirthDate = DateTime.Parse("1969-9-8"),
                    Nationality = "South Korea",
                    Gender = "Male"
                },
                new Author
                { /*Id = 3, */
                    FirstName = "Brynn",
                    LastName = "Farmer",
                    BirthDate = DateTime.Parse("1988-6-26"),
                    Nationality = "Australia",
                    Gender = "Male"
                },
                new Author
                { /*Id = 4, */
                    FirstName = "Ferris",
                    LastName = "Farrell",
                    BirthDate = DateTime.Parse("1974-2-4"),
                    Nationality = "Pakistan",
                    Gender = "Female"
                },
                new Author
                { /*Id = 5, */
                    FirstName = "Philip",
                    LastName = "Cantrell",
                    BirthDate = DateTime.Parse("1929-2-17"),
                    Nationality = "United Kingdom",
                    Gender = "Male"
                },
                new Author
                { /*Id = 6, */
                    FirstName = "Stacey",
                    LastName = "Burton",
                    BirthDate = DateTime.Parse("1921-1-3"),
                    Nationality = "Spain",
                    Gender = "Female"
                },
                new Author
                { /*Id = 7, */
                    FirstName = "Kameko",
                    LastName = "Shaffer",
                    BirthDate = DateTime.Parse("2005-1-24"),
                    Nationality = "Poland",
                    Gender = "Female"
                },
                new Author
                { /*Id = 8, */
                    FirstName = "Chaim",
                    LastName = "Langley",
                    BirthDate = DateTime.Parse("1942-3-19"),
                    Nationality = "Russian Federation",
                    Gender = "Male"
                }

            );
            context.SaveChanges();

            context.Book.AddRange(
                new Book { /*Id = 1, */ 
                    Title = "Song Birds Tale", 
                    YearPublished = 2012, 
                    NumPages = 410, 
                    Description = "Lacking", 
                    Publisher = "Store House Media",
                    FrontPage = "",
                    DownloadUrl = "",
                    AuthorId = 1 
                },
                new Book
                {/*Id = 2, */
                    Title = "quis diam. Pellentesque",
                    YearPublished = 2022,
                    NumPages = 650,
                    Description = "mauris. Suspendisse aliquet molestie tellus. Aenean egestas hendrerit neque. In ornare sagittis felis. Donec tempor, est ac mattis semper, dui lectus rutrum urna, nec luctus felis purus ac tellus. Suspendisse sed dolor. Fusce mi lorem, vehicula et, rutrum eu, ultrices sit amet, risus. Donec nibh enim, gravida sit amet, dapibus id, blandit at, nisi. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin vel nisl. Quisque fringilla euismod enim. Etiam gravida molestie arcu. Sed eu nibh vulputate mauris sagittis placerat.",
                    Publisher = "In Tempus Corporation",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 1
                },
                new Book
                {/*Id = 3, */
                    Title = "nunc.",
                    YearPublished = 2015,
                    NumPages = 161,
                    Description = "vulputate mauris sagittis placerat. Cras dictum ultricies ligula. Nullam enim. Sed nulla ante, iaculis nec, eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc sed libero. Proin sed turpis nec mauris blandit mattis. Cras eget nisi dictum augue malesuada malesuada. Integer id magna et ipsum cursus vestibulum. Mauris magna. Duis dignissim tempor arcu. Vestibulum ut eros non enim commodo hendrerit. Donec porttitor tellus non magna. Nam ligula elit, pretium et, rutrum non, hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit consectetuer, cursus et, magna. Praesent interdum ligula eu enim. Etiam imperdiet dictum magna. Ut tincidunt orci quis lectus. Nullam suscipit, est ac",
                    Publisher = "Mi Ac Corp.",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 2
                },
                new Book
                {/*Id = 4, */
                    Title = "feugiat metus sit",
                    YearPublished = 2012,
                    NumPages = 173,
                    Description = "Sed eu eros. Nam consequat dolor vitae dolor. Donec fringilla. Donec feugiat metus sit amet ante. Vivamus non lorem vitae odio sagittis semper. Nam tempor diam dictum sapien. Aenean massa. Integer vitae nibh. Donec est mauris, rhoncus id, mollis nec, cursus a, enim. Suspendisse aliquet, sem ut cursus luctus, ipsum leo elementum sem, vitae aliquam eros turpis non enim. Mauris quis turpis vitae purus gravida sagittis. Duis gravida. Praesent eu nulla at sem molestie sodales. Mauris blandit enim consequat purus. Maecenas libero est, congue a, aliquet vel, vulputate eu, odio. Phasellus at augue id ante dictum cursus. Nunc mauris elit, dictum eu, eleifend nec, malesuada ut, sem. Nulla interdum. Curabitur dictum. Phasellus in felis. Nulla tempor augue ac ipsum.",
                    Publisher = "Gravida Sagittis Corp.",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 6
                },
                new Book
                {/*Id = 5, */
                    Title = "ipsum. Suspendisse sagittis. Nullam",
                    YearPublished = 2016,
                    NumPages = 154,
                    Description = "id risus quis diam luctus lobortis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Mauris ut quam vel sapien imperdiet ornare. In faucibus. Morbi vehicula. Pellentesque tincidunt tempus risus. Donec egestas. Duis ac arcu. Nunc mauris. Morbi non sapien molestie orci tincidunt adipiscing. Mauris molestie pharetra nibh. Aliquam ornare, libero at auctor ullamcorper, nisl arcu iaculis enim, sit amet ornare lectus justo eu arcu. Morbi sit amet massa. Quisque porttitor eros nec tellus. Nunc lectus pede, ultrices a, auctor non, feugiat nec, diam. Duis mi enim, condimentum",
                    Publisher = "In Corporation",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 8
                },
                new Book
                {/*Id = 6, */
                    Title = "dui augue",
                    YearPublished = 2013,
                    NumPages = 205,
                    Description = "nisi dictum augue malesuada malesuada. Integer id magna et ipsum cursus vestibulum. Mauris magna. Duis dignissim tempor arcu. Vestibulum ut eros non enim commodo hendrerit. Donec porttitor tellus non magna. Nam ligula elit, pretium et, rutrum non, hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit consectetuer, cursus et, magna. Praesent interdum ligula eu enim. Etiam imperdiet dictum magna. Ut tincidunt orci quis lectus. Nullam suscipit, est ac facilisis facilisis, magna tellus faucibus leo, in lobortis tellus justo sit amet nulla. Donec non justo. Proin non massa non ante bibendum ullamcorper. Duis cursus, diam at pretium aliquet, metus urna convallis erat, eget tincidunt dui augue eu tellus. Phasellus elit pede, malesuada vel, venenatis vel, faucibus id, libero. Donec consectetuer mauris id sapien. Cras dolor dolor, tempus non, lacinia at, iaculis quis, pede. Praesent eu dui. Cum sociis",
                    Publisher = "Morbi Institute",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 2
                },
                new Book
                {/*Id = 7, */
                    Title = "Donec vitae erat vel",
                    YearPublished = 2015,
                    NumPages = 560,
                    Description = "sed, facilisis vitae, orci. Phasellus dapibus quam quis diam. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Fusce aliquet magna a neque. Nullam ut nisi a odio semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam ultrices iaculis odio. Nam interdum enim non nisi. Aenean eget metus. In nec orci. Donec nibh. Quisque nonummy ipsum non arcu. Vivamus sit amet risus. Donec egestas. Aliquam nec enim. Nunc ut erat. Sed nunc est, mollis non, cursus non, egestas a, dui. Cras pellentesque.",
                    Publisher = "Volutpat Nunc LLP",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 5
                },
                new Book
                {/*Id = 8, */
                    Title = "facilisis eget, ipsum.",
                    YearPublished = 2017,
                    NumPages = 102,
                    Description = "et, euismod et, commodo at, libero. Morbi accumsan laoreet ipsum. Curabitur consequat, lectus sit amet luctus vulputate, nisi sem semper erat, in consectetuer ipsum nunc id enim. Curabitur massa. Vestibulum accumsan neque et nunc. Quisque ornare tortor at risus. Nunc ac sem ut dolor dapibus gravida. Aliquam tincidunt, nunc ac mattis ornare, lectus ante dictum mi, ac mattis velit justo nec ante. Maecenas mi felis, adipiscing fringilla, porttitor vulputate, posuere vulputate, lacus. Cras interdum. Nunc sollicitudin commodo ipsum. Suspendisse non leo. Vivamus nibh dolor, nonummy ac, feugiat non, lobortis quis, pede. Suspendisse dui.",
                    Publisher = "Cras Convallis Institute",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 2
                },
                new Book
                {/*Id = 9, */
                    Title = "ullamcorper.",
                    YearPublished = 2020,
                    NumPages = 466,
                    Description = "pede sagittis augue, eu tempor erat neque non quam. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aliquam fringilla cursus purus. Nullam scelerisque neque sed sem egestas blandit. Nam nulla magna, malesuada vel, convallis in, cursus et, eros. Proin ultrices. Duis volutpat nunc sit amet metus. Aliquam erat volutpat. Nulla facilisis. Suspendisse commodo tincidunt nibh. Phasellus nulla. Integer vulputate, risus a ultricies adipiscing, enim mi tempor lorem, eget mollis lectus pede et risus. Quisque libero lacus, varius et, euismod et, commodo at, libero. Morbi accumsan laoreet ipsum. Curabitur consequat, lectus sit amet luctus vulputate, nisi sem semper erat, in consectetuer ipsum nunc id enim. Curabitur massa. Vestibulum accumsan neque et nunc. Quisque ornare tortor at risus. Nunc ac sem ut dolor dapibus gravida. Aliquam tincidunt, nunc ac mattis ornare, lectus ante dictum mi, ac mattis velit justo nec ante. Maecenas mi felis, adipiscing fringilla, porttitor vulputate, posuere vulputate, lacus. Cras interdum. Nunc sollicitudin commodo ipsum. Suspendisse non leo. Vivamus nibh dolor, nonummy ac, feugiat non, lobortis quis, pede. Suspendisse dui. Fusce diam nunc, ullamcorper eu, euismod ac, fermentum vel, mauris. Integer sem elit, pharetra ut, pharetra sed,",
                    Publisher = "Mauris Eu Turpis Foundation",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 3
                },
                new Book
                {/*Id = 10, */
                    Title = "eget metus. In",
                    YearPublished = 2012,
                    NumPages = 451,
                    Description = "neque venenatis lacus. Etiam bibendum fermentum metus. Aenean sed pede nec ante blandit viverra. Donec tempus, lorem fringilla ornare placerat, orci lacus vestibulum lorem, sit amet ultricies sem magna nec quam. Curabitur vel lectus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec dignissim magna a tortor. Nunc commodo auctor velit. Aliquam nisl. Nulla eu neque pellentesque massa lobortis ultrices. Vivamus rhoncus. Donec est. Nunc ullamcorper, velit in aliquet lobortis, nisi nibh lacinia orci, consectetuer euismod est arcu ac orci. Ut semper pretium neque. Morbi quis urna. Nunc quis arcu vel quam dignissim pharetra. Nam ac nulla. In tincidunt congue turpis. In condimentum. Donec at arcu. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae Donec tincidunt. Donec vitae erat vel pede blandit congue. In scelerisque scelerisque dui. Suspendisse ac metus vitae velit egestas lacinia. Sed congue, elit",
                    Publisher = "Faucibus Orci Luctus Company",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 3
                },
                new Book
                {/*Id = 11, */
                    Title = "lorem, luctus",
                    YearPublished = 2023,
                    NumPages = 481,
                    Description = "sed dolor. Fusce mi lorem, vehicula et, rutrum eu, ultrices sit amet, risus. Donec nibh enim, gravida sit amet, dapibus id, blandit at, nisi. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin vel nisl. Quisque fringilla euismod enim. Etiam gravida molestie arcu. Sed eu nibh vulputate mauris sagittis placerat. Cras dictum ultricies ligula. Nullam enim. Sed nulla ante, iaculis nec, eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc sed libero. Proin sed turpis nec mauris blandit mattis. Cras eget nisi dictum augue malesuada malesuada. Integer id magna et ipsum cursus vestibulum. Mauris magna. Duis dignissim tempor arcu. Vestibulum ut eros non enim commodo hendrerit. Donec porttitor tellus non magna. Nam",
                    Publisher = "Consectetuer Ipsum Nunc Corp.",
                    FrontPage = "/",
                    DownloadUrl = "/",
                    AuthorId = 1
                }
            );
            context.SaveChanges();

            context.Genre.AddRange(
                new Genre {/*Id = 1 */ GenreName = "Drama" },
                new Genre {/*Id = 2 */  GenreName = "Action"},
                new Genre {/*Id = 3 */ GenreName = "Fantasy" },
                new Genre {/*Id = 4 */  GenreName = "Adventure" },
                new Genre {/*Id = 5 */ GenreName = "Horror" },
                new Genre {/*Id = 6 */  GenreName = "Fiction" },
                new Genre {/*Id = 7 */ GenreName = "Triler" },
                new Genre {/*Id = 8 */  GenreName = "Comedy" }
                );
            context.SaveChanges();

            context.BookGenre.AddRange(
                new BookGenre {/*Id = 1 */ BookId = 1, GenreId = 1 },
                new BookGenre {/*Id = 2 */ BookId = 2, GenreId = 2 },
                new BookGenre {/*Id = 3 */ BookId = 3, GenreId = 3 },
                new BookGenre {/*Id = 4 */ BookId = 4, GenreId = 4 },
                new BookGenre {/*Id = 5 */ BookId = 5, GenreId = 5 },
                new BookGenre {/*Id = 6 */ BookId = 6, GenreId = 6 },
                new BookGenre {/*Id = 7 */ BookId = 7, GenreId = 7 },
                new BookGenre {/*Id = 8 */ BookId = 8, GenreId = 8 },
                new BookGenre {/*Id = 9 */ BookId = 9, GenreId = 1},
                new BookGenre {/*Id = 10 */ BookId = 10, GenreId = 4 },
                new BookGenre {/*Id = 11 */ BookId = 11, GenreId = 3 },
                new BookGenre {/*Id = 12 */ BookId = 2, GenreId = 2 },
                new BookGenre {/*Id = 13 */ BookId = 7, GenreId = 7 },
                new BookGenre {/*Id = 14 */ BookId = 2, GenreId = 8 },
                new BookGenre {/*Id = 15 */ BookId = 11, GenreId = 4 },
                new BookGenre {/*Id = 16 */ BookId = 8, GenreId = 5 },
                new BookGenre {/*Id = 17 */ BookId = 9, GenreId = 2 },
                new BookGenre {/*Id = 18 */ BookId = 3, GenreId = 3 },
                new BookGenre {/*Id = 19 */ BookId = 8, GenreId = 5 },
                new BookGenre {/*Id = 20 */ BookId = 9, GenreId = 6 }
                );
            context.SaveChanges();

            context.Review.AddRange(
                new Review 
                {/*Id = 1 */  
                    BookId = 1, 
                    AppUser = "",
                    Comment = "",
                    Rating = 2
                },
                new Review
                {
                    BookId = 8,
                    AppUser = "/",
                    Comment = "conubia nostra, per inceptos hymenaeos. Mauris ut quam vel sapien imperdiet ornare. In faucibus. Morbi vehicula. Pellentesque tincidunt tempus risus. Donec egestas. Duis ac arcu. Nunc mauris. Morbi non sapien molestie orci tincidunt adipiscing. Mauris molestie pharetra nibh. Aliquam",
                    Rating = 2
                },
                new Review
                {
                    BookId = 2,
                    AppUser = "/",
                    Comment = "sagittis placerat. Cras dictum ultricies ligula. Nullam enim. Sed nulla ante, iaculis nec, eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc sed libero. Proin sed turpis nec mauris blandit mattis. Cras eget nisi dictum augue malesuada malesuada.",
                    Rating = 3
                },
                new Review
                {
                    BookId = 6,
                    AppUser = "/",
                    Comment = "nunc sit amet metus. Aliquam erat volutpat. Nulla facilisis. Suspendisse commodo tincidunt nibh. Phasellus nulla. Integer vulputate, risus a ultricies adipiscing, enim",
                    Rating = 2
                },
                new Review
                {
                    BookId = 10,
                    AppUser = "/",
                    Comment = "eleifend egestas. Sed pharetra, felis eget varius ultrices, mauris ipsum porta elit, a feugiat tellus lorem eu metus. In lorem. Donec elementum, lorem ut aliquam iaculis, lacus pede sagittis augue, eu tempor erat neque non quam. Pellentesque habitant morbi tristique senectus",
                    Rating = 3
                },
                new Review
                {
                    BookId = 3,
                    AppUser = "/",
                    Comment = "ultricies ligula. Nullam enim. Sed nulla ante, iaculis nec, eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc sed libero.",
                    Rating = 3
                },
                new Review
                {
                    BookId = 5,
                    AppUser = "/",
                    Comment = "eu neque pellentesque massa lobortis ultrices. Vivamus rhoncus. Donec est. Nunc ullamcorper, velit in aliquet lobortis, nisi nibh lacinia orci, consectetuer euismod est arcu ac orci. Ut semper pretium",
                    Rating = 1
                },
                new Review
                {
                    BookId = 2,
                    AppUser = "/",
                    Comment = "elit pede, malesuada vel, venenatis vel, faucibus id, libero. Donec consectetuer mauris id sapien. Cras dolor dolor, tempus non, lacinia at, iaculis quis, pede. Praesent eu dui. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean eget",
                    Rating = 3
                },
                new Review
                {
                    BookId = 5,
                    AppUser = "/",
                    Comment = "Sed auctor odio a purus. Duis elementum, dui quis accumsan convallis, ante lectus convallis est, vitae sodales nisi magna sed dui. Fusce aliquam, enim nec",
                    Rating = 1
                },
                new Review
                {
                    BookId = 5,
                    AppUser = "/",
                    Comment = "Fusce aliquet magna a neque. Nullam ut nisi a odio semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam ultrices iaculis odio. Nam interdum enim non nisi. Aenean eget metus. In nec orci. Donec nibh. Quisque nonummy ipsum non arcu. Vivamus sit amet risus. Donec",
                    Rating = 2
                },
                new Review
                {
                    BookId = 5,
                    AppUser = "/",
                    Comment = "Vivamus non lorem vitae odio sagittis semper. Nam tempor diam dictum sapien. Aenean massa. Integer vitae nibh. Donec est mauris, rhoncus id, mollis nec, cursus a,",
                    Rating = 2
                }
             );
            context.SaveChanges();

            context.UserBooks.AddRange(
                new UserBooks { AppUser = "", BookId = 1}

                );
            context.SaveChanges();
        }
    }
}

