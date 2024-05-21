using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Howest.MagicCards.Web;
using Howest.MagicCards.Web.Shared;
using Howest.MagicCards.DAL.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Microsoft.AspNetCore.Http.Json;
using static GraphQL.Validation.Rules.OverlappingFieldsCanBeMerged;
using static System.Reflection.Metadata.BlobBuilder;
using Howest.MagicCards.WebAPI.Wrappers;
using Howest.MagicCards.Shared.DTO;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Howest.MagicCards.MinimalAPI.Model;
using Howest.MagicCards.Web.Pages;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Howest.MagicCards.Web.Pages
{
    public partial class MyDeck
    {

        private IEnumerable<Deck>? cards = null;
        private String button = "Remove";

        private readonly JsonSerializerOptions JsonOptions;


        public MyDeck()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public void NavigateToCardDetailsPage(Deck card)
        {
            NavigationManager.NavigateTo($"/card/{card.Id}");
        }

        protected override async Task OnInitializedAsync()
        {
            updateCards();
        }

        public async Task UpdateAmountFromDeck(Deck card)
        {
            using var client = httpClientFactory.CreateClient("DeckAPI");

            int id = card.Id;

            var request = new HttpRequestMessage(HttpMethod.Put, $"deck/{id}");
            request.Content = new StringContent(JsonSerializer.Serialize(card), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to remove card from database. Status code: {response.StatusCode}");
            }
            updateCards();
        }

        public async Task OnAmountClicked(Deck card)
        {
            using var client = httpClientFactory.CreateClient("DeckAPI");

            var request = new HttpRequestMessage(HttpMethod.Post, "deck");
            request.Content = new StringContent(JsonSerializer.Serialize(card), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add card to database. Status code: {response.StatusCode}");
            }
            updateCards();
        }

        public async Task Clear()
        {
            using var client = httpClientFactory.CreateClient("DeckAPI");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"deck");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to remove all cards from database. Status code: {response.StatusCode}");
            }
            updateCards();
        }

        public async Task updateCards()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("DeckAPI");

            HttpResponseMessage response = await httpClient.GetAsync($"deck");

            string apiResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add card to database. Status code: {response.StatusCode}");
            }
            else
            {
                IEnumerable<Deck>? result = JsonSerializer.Deserialize<IEnumerable<Deck>>(apiResponse, JsonOptions);
                cards = result;
                StateHasChanged();
            }
        }
    }
}
