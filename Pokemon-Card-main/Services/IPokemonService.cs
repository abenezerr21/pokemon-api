using P.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<Pokemon> GetByIdAsync(int id);
        Task AddAsync(Pokemon pokemon);
        Task UpdateAsync(int id, Pokemon updatedPokemon);
        Task DeleteAsync(int id);
    }
}
