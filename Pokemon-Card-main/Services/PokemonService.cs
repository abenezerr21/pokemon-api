using P.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        // Constructor that accepts MongoClient and settings
        public PokemonService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _pokemonCollection = database.GetCollection<Pokemon>("Poke");
        }

        // Get all Pokemon records
        public async Task<List<Pokemon>> GetAllAsync()
        {
            return await _pokemonCollection.Find(_ => true).ToListAsync();
        }

        // Get a specific Pokemon by ID
        public async Task<Pokemon> GetByIdAsync(int id)
        {
            return await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        // Add a new Pokemon record
        public async Task AddAsync(Pokemon pokemon)
        {
            await _pokemonCollection.InsertOneAsync(pokemon);
        }

        // Update an existing Pokemon record
        public async Task UpdateAsync(int id, Pokemon updatedPokemon)
        {
            await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);
        }

        // Delete a Pokemon record by ID
        public async Task DeleteAsync(int id)
        {
            await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}