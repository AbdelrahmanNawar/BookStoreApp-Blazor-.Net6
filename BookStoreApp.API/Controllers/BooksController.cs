#nullable disable
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Constants;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(BookStoreDBContext context, IMapper mapper, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                var bookDtos = await _context.Books.Include(b => b.Author)
                                                .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync();
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a GET in {nameof(GetBooks)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBookById(int id)
        {
            try
            {
                var bookDto = await _context.Books.Include(b => b.Author)
                                                .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
                                                .FirstOrDefaultAsync(b => b.Id == id);

                if (bookDto is null)
                {
                    _logger.LogWarning($"Record not found: {nameof(GetBookById)} - ID: {id}");
                    return NotFound();
                }

                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a GET in {nameof(GetBookById)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateBook(int id, BookUpdateDto bookDto)
        {
            try
            {
                if (id != bookDto.Id)
                {
                    return BadRequest();
                }

                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

                if (book is null)
                {
                    _logger.LogWarning($"Record not found: {nameof(UpdateBook)} - ID: {id}");
                    return NotFound();
                }

                var oldURL = book.ImageURL;
                _mapper.Map(bookDto, book);
                book.ImageURL = CreateFile(bookDto.ImageData, bookDto.OriginalImageName, oldURL);
                _context.Entry(book).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExists(id))
                    return NotFound();
                else
                    return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a PUT in {nameof(UpdateBook)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // POST: api/Books
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                book.ImageURL = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a POST in {nameof(PostBook)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book is null)
                {
                    _logger.LogWarning($"Record not found: {nameof(DeleteBook)} - ID: {id}");
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        private async Task<bool> BookExists(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }

        private string CreateFile(string imageBase64, string imageName, string oldUrl = null)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = $"{_webHostEnvironment.WebRootPath}\\BookCoverimages\\{fileName}";
            var newURL = $"https://{url}/BookCoverImages/{fileName}";

            if (string.IsNullOrEmpty(oldUrl) || oldUrl != newURL)
            {
                byte[] image = Convert.FromBase64String(imageBase64);
                var fileStream = System.IO.File.Create(path);
                fileStream.Write(image, 0, image.Length);
                fileStream.Close();
            }
            return newURL;
        }
    }
}
