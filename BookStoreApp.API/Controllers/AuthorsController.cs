#nullable disable
using AutoMapper;
using BookStoreApp.API.Constants;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BookStoreDBContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
                return Ok(_mapper.Map<IEnumerable<AuthorReadOnlyDto>>(authors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a GET in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthorById(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(GetAuthorById)} - ID: {id}");
                    return NotFound();
                }
                return Ok(_mapper.Map<AuthorReadOnlyDto>(author));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a GET in {nameof(GetAuthorById)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto authorDto)
        {
            try
            {
                if (id != authorDto.Id)
                {
                    return BadRequest();
                }

                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(UpdateAuthor)} - ID: {id}");
                    return NotFound();
                }

                _mapper.Map(authorDto, author);

                _context.Entry(author).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
                    return NotFound();
                else
                    return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a PUT in {nameof(UpdateAuthor)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> AddAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDto);
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a POST in {nameof(AddAuthor)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(DeleteAuthor)} - ID: {id}");
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured performing a DELETE in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.ERROR_500_MESSAGE);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
