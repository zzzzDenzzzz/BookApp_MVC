namespace WebApplication1.Models;

public static class BookDbInitializer
{
    public static void Seed(IApplicationBuilder app)
    {
        var result = app.ApplicationServices.CreateScope();
        var context = result.ServiceProvider.GetRequiredService<BookDbContext>();

        if (!context.Genres.Any())
        {
            context.Genres.Add(new Genre { Name = "Fantastic" });
            context.Genres.Add(new Genre { Name = "Mystic" });
            context.Genres.Add(new Genre { Name = "Fantasy" });
            context.Genres.Add(new Genre { Name = "Lyrics" });
            context.Genres.Add(new Genre { Name = "Education" });
            context.Genres.Add(new Genre { Name = "Epic" });
            context.Genres.Add(new Genre { Name = "Drama" });
            context.Genres.Add(new Genre { Name = "Detective" });
            context.SaveChanges();
        }

        if (context.Authors.Any()) return;
        context.Authors.Add(new Author { FullName = "J.R.R. Tolkien" });
        context.Authors.Add(new Author { FullName = "C.S. Lewis" });
        context.Authors.Add(new Author { FullName = "Ursula K. Le Guin" });
        context.Authors.Add(new Author { FullName = "Carlos Castaneda" });
        context.Authors.Add(new Author { FullName = "Hermann Hesse" });
        context.Authors.Add(new Author { FullName = "Paulo Coelho" });
        context.Authors.Add(new Author { FullName = "George R.R. Martin" });
        context.Authors.Add(new Author { FullName = "J.K. Rowling" });
        context.Authors.Add(new Author { FullName = "Terry Pratchett" });
        context.Authors.Add(new Author { FullName = "Bob Dylan" });
        context.Authors.Add(new Author { FullName = "Leonard Cohen" });
        context.Authors.Add(new Author { FullName = "Maya Angelou" });
        context.Authors.Add(new Author { FullName = "John Dewey" });
        context.Authors.Add(new Author { FullName = "Howard Gardner" });
        context.Authors.Add(new Author { FullName = "Paulo Freire" });
        context.Authors.Add(new Author { FullName = "Homer" });
        context.Authors.Add(new Author { FullName = "Virgil" });
        context.Authors.Add(new Author { FullName = "Dante Alighieri" });
        context.Authors.Add(new Author { FullName = "William Shakespeare" });
        context.Authors.Add(new Author { FullName = "Henrik Ibsen" });
        context.Authors.Add(new Author { FullName = "Anton Chekhov" });
        context.Authors.Add(new Author { FullName = "Agatha Christie" });
        context.Authors.Add(new Author { FullName = "Arthur Conan Doyle" });
        context.Authors.Add(new Author { FullName = "Raymond Chandler" });
        context.SaveChanges();
    }
}