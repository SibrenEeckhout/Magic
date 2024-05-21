using Newtonsoft.Json;

namespace Howest.MagicCards.MinimalAPI.Model
{
    [JsonObject]
    public class Deck
    {
        public int Id { get; set; }
        public int Amount { get; set; } = 0;
        public string Name { get; set; }
        public string SetCode { get; set; }
        public long ArtistId { get; set; }
        public string RarityCode { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
    }
}
