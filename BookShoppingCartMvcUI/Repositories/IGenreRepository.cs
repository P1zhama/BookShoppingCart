namespace THEBOOKSTORE.Repositories
{
    public interface IGenreRepository
    {
        Task AddGenre(Genre genre);
        Task UpdateGenre(Genre genre);
        Task<Genre?> GetGenreById(int id);
        Task<IEnumerable<Genre>> GetGenres();
    }
}