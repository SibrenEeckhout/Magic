using System.IO;
using Newtonsoft.Json;
using Howest.MagicCards.MinimalAPI.Model;

namespace Howest.MagicCards.MinimalAPI.Repositories
{
    public class DeckRepository
    {
        private readonly string _jsonFilePath;

        public DeckRepository()
        {
            _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON", "deck.json");
        }

        public void Add(Deck deck)
        {
            string json = File.ReadAllText(_jsonFilePath);

            var decks = JsonConvert.DeserializeObject<List<Deck>>(json) ?? new List<Deck>();
            var existingDeck = decks.FirstOrDefault(d => d.Id == deck.Id);
            int totalAmount = decks.Sum(d => d.Amount);
            if (totalAmount < 60) {
                if (existingDeck != null) { existingDeck.Amount++; }
                else
                {
                    deck.Amount = 1;
                    decks.Add(deck);
                }
            }
            
            json = JsonConvert.SerializeObject(decks);
            File.WriteAllText(_jsonFilePath, json);
        }

        public List<Deck> GetAll()
        {
            string json = File.ReadAllText(_jsonFilePath);
            var decks = JsonConvert.DeserializeObject<List<Deck>>(json) ?? new List<Deck>();

            return decks;
        }

        public void updateAmount(int id)
        {
            string json = File.ReadAllText(_jsonFilePath);
            var decks = JsonConvert.DeserializeObject<List<Deck>>(json) ?? new List<Deck>();

            var deckToRemove = decks.FirstOrDefault(d => d.Id == id);
            if (deckToRemove != null)
            {
                if (deckToRemove.Amount > 1) { deckToRemove.Amount--; }
                else{decks.Remove(deckToRemove);}

                json = JsonConvert.SerializeObject(decks);
                File.WriteAllText(_jsonFilePath, json);
            }
        }

        public void RemoveAll()
        {
            var decks = new List<Deck>();

            var json = JsonConvert.SerializeObject(decks);
            File.WriteAllText(_jsonFilePath, json);
        }

        public void UpdateField(int id, string fieldName, String newValue)
        {
            string json = File.ReadAllText(_jsonFilePath);
            var decks = JsonConvert.DeserializeObject<List<Deck>>(json) ?? new List<Deck>();

            var deckToUpdate = decks.FirstOrDefault(d => d.Id == id);
            switch (fieldName)
            {
                case "Name":
                    deckToUpdate.Name = (string)newValue;
                    break;
                case "SetCode":
                    deckToUpdate.SetCode = (string)newValue;
                    break;
                case "RarityCode":
                    deckToUpdate.RarityCode = (string)newValue;
                    break;
                case "Type":
                    deckToUpdate.Type = (string)newValue;
                    break;
                case "Text":
                    deckToUpdate.Text = (string)newValue;
                    break;
                default:
                    throw new ArgumentException("Invalid field name.");
            }

            json = JsonConvert.SerializeObject(decks);
            File.WriteAllText(_jsonFilePath, json);

        }

    }
}
