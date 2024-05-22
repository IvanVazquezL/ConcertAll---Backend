using ConcertAll.Entities;

namespace ConcertAll.Repositories
{
    public class GenreRepository
    {
        private readonly List<Genre> genreList;
        public GenreRepository()
        {
            genreList = new();
            genreList.Add(new Genre() { Id = 1, Name = "Pop"});
            genreList.Add(new Genre() { Id = 2, Name = "K-Pop" });
            genreList.Add(new Genre() { Id = 3, Name = "Rock" });
        }

        public List<Genre> Get() 
        { 
            return genreList;
        }
        public Genre? Get(int id)
        {
            return genreList.FirstOrDefault(genre => genre.Id == id);
        }

        public void Add(Genre genre) 
        {
            var lastItem = genreList.MaxBy(genre => genre.Id);
            var newGenre = new Genre() { 
                Id = lastItem is null ? 1 : lastItem.Id + 1, 
                Name = genre.Name
            };
            genreList.Add(newGenre);
        }

        public void Update(int id, Genre genre)
        {
            var item = Get(id);

            if (item is not null)
            {
                item.Name = genre.Name;
                item.Status = genre.Status;
            }
        }

        public void Delete(int id)
        {
            var item = Get(id);

            if (item is not null)
            {
                genreList.Remove(item);
            }
        }

    }
}
