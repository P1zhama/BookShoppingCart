using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Identity.Client;
using THEBOOKSTORE.Repositories;

namespace THEBOOKSTORE.Controllers
{
    [Authorize(Roles=nameof(Roles.Admin))]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetGenres();
            return View(genres);
        }
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreDTO genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            try
            {
                var genreToAdd = new Genre { GenreName = genre.GenreName, Id = genre.Id };
                await _genreRepository.AddGenre(genreToAdd);
                TempData["successMessage"] = "Жанр успешно добавлен";
                return RedirectToAction(nameof(AddGenre));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Жанр не добавлен!!";
                return View(genre);
                
            }            
        }
        public async Task<IActionResult> UpdateGenre(int id)
        {
            var genre = await _genreRepository.GetGenreById(id);
            if (genre is null)
                throw new InvalidOperationException($"Жанр с идентификатором: {id} не найден");

            var genreToUpdate = new GenreDTO
            {
                Id = genre.Id,
                GenreName = genre.GenreName 
            };

            return View(genreToUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGenre(GenreDTO genreToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(genreToUpdate);
            }
            try
            {
                var genre = new Genre { GenreName = genreToUpdate.GenreName, Id = genreToUpdate.Id };
                await _genreRepository.UpdateGenre(genre);
                TempData["successMessage"] = "Жанр успешно обновлён";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["erroeMessage"] = "Жанр не удалось обновить";
                return View(genreToUpdate);                
            }
        }
    }
}
