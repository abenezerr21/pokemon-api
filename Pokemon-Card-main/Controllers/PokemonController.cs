using Microsoft.AspNetCore.Mvc;
using P.Models;
using P.Services;

namespace P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        // Injected service
        private readonly IPokemonService _pokemonService;

        // Constructor injection of the service
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // Add a Pokemon
        [HttpPost("add")]
        public IActionResult AddPokemon(Pokemon pokemon)
        {
            _pokemonService.AddAsync(pokemon); // Delegates to service
            return Ok(new { Message = "Pokemon added successfully!" });
        }

        // Get all Pokemon
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPokemon()
        {
            var pokemons = await _pokemonService.GetAllAsync();
            return Ok(pokemons);
        }

        // Get Pokemon by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonById(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound(new { Message = "Pokemon not found." });
            }
            return Ok(pokemon);
        }

        // Update Pokemon
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon updatedPokemon)
        {
            var existingPokemon = await _pokemonService.GetByIdAsync(id);
            if (existingPokemon == null)
            {
                return NotFound(new { Message = "Pokemon not found." });
            }

            await _pokemonService.UpdateAsync(id, updatedPokemon);
            return Ok(new { Message = "Pokemon updated successfully!" });
        }

        // Delete Pokemon
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePokemonAsync(int id)
        {
            var existingPokemon = await _pokemonService.GetByIdAsync(id);
            if (existingPokemon == null)
            {
                return NotFound(new { Message = "Pokemon not found." });
            }

            await _pokemonService.DeleteAsync(id);
            return Ok(new { Message = "Pokemon deleted successfully!" });
        }
    }
}
