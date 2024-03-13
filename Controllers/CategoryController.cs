using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{

    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetListAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05xx - Falha ao buscar categorias"));
            }

        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>
                        ($"Categoria com id {id} não encontrada"));
                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<Category>(ex.Message));
            }


        }

        [HttpPost("v1/categories")]
        public async Task<ActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErros()));

            try
            {
                var category = new Category
                {
                    Name = model.Name,
                    Slug = model.Slug
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"ERXX09 - Error on saving Category");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERXX10 -  {ex.Message}");
            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new ResultViewModel<Category>($"Category with id {id} not found!"));

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound(new ResultViewModel<Category>($"Category with id {id} not found!"));

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
