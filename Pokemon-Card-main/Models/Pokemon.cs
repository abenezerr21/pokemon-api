namespace P.Models
{
    public class Pokemon
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Ability { get; set; }
        public required int Level { get; set; }
    }
}
