using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyncMe.Models;
using SyncMe.Services; // Importante para usar o ContentService

namespace SyncMe.Controllers {
    public class ContentsController : Controller {
        // Trocamos o AppDbContext pelo nosso ContentService
        private readonly ContentService _service;

        public ContentsController(ContentService service) {
            _service = service;
        }

        // GET: Contents (Com Busca e Paginação)
        public async Task<IActionResult> Index(string searchString, int? pageNumber) {
            int pageSize = 3; // Cards por página
            int pageIndex = pageNumber ?? 1;

            // O Controller pede os dados prontos para o Serviço
            var result = await _service.GetAllAsync(searchString, pageIndex, pageSize);

            // Passa dados visuais para a View
            ViewBag.PageString = searchString;
            ViewBag.CurrentPage = result.CurrentPage;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Items);
        }

        // GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) return NotFound();

            var content = await _service.GetByIdAsync(id.Value);

            if (content == null) return NotFound();

            return View(content);
        }

        // GET: Contents/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Contents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Summary,MediaUrl,PublishDate,Difficulty,Category")] Content content) {
            if (ModelState.IsValid) {
                await _service.CreateAsync(content);
                return RedirectToAction(nameof(Index));
            }
            return View(content);
        }

        // GET: Contents/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return NotFound();

            var content = await _service.GetByIdAsync(id.Value);

            if (content == null) return NotFound();

            return View(content);
        }

        // POST: Contents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Summary,MediaUrl,PublishDate,Difficulty,Category")] Content content) {
            if (id != content.Id) return NotFound();

            if (ModelState.IsValid) {
                try {
                    await _service.UpdateAsync(content);
                }
                catch (DbUpdateConcurrencyException) {
                    if (await _service.GetByIdAsync(id) == null) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(content);
        }

        // GET: Contents/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return NotFound();

            var content = await _service.GetByIdAsync(id.Value);

            if (content == null) return NotFound();

            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}