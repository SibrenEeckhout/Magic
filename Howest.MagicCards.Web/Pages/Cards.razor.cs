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



namespace Howest.MagicCards.Web.Pages
{
    public partial class Cards
    {
        private string Sort = string.Empty;
        private string message = string.Empty;
        private string button = "Add";
        private IEnumerable<CardReadDTO>? cards = null;
        private IEnumerable<ArtistReadDTO>? artists = null;
        private IEnumerable<RarityReadDTO>? rarites = null;
        private IEnumerable<TypeReadDTO>? types = null;
        private readonly JsonSerializerOptions JsonOptions;
        private int pageNumber = 1;
        private int pageSize = 12;
        private Nullable<int> id;
        private String name = string.Empty;
        private String setCode = string.Empty;
        private Nullable<int> artistId;
        public String raritieCode = string.Empty;
        private String rarityCode = string.Empty;
        private String type = string.Empty;
        private String text = string.Empty;
        private String apiVersion = string.Empty;
        private int selectedArtist;
        private TypeReadDTO selectedType = null;
        private RarityReadDTO selectedRarity = null;
        private EditContext editContext;
        private int counter = 0;


        public Cards()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        

    protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(this);
            counter= 0;
            HttpClient httpClient = httpClientFactory.CreateClient("CardsAPI");
            
            //HttpResponseMessage response = await httpClient.GetAsync($"cards");
            HttpResponseMessage response = await httpClient.GetAsync($"cards?PageNumber={pageNumber}&PageSize={pageSize}&Name={name}&SetCode={setCode}&ArtistId={artistId}&RarityCode={rarityCode}&type={type}&Text={text}&api-version={apiVersion}");

            string apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                PagedResponse<IEnumerable<CardReadDTO>>? result =
                        JsonSerializer.Deserialize<PagedResponse<IEnumerable<CardReadDTO>>>(apiResponse, JsonOptions);
                cards = result?.Data;
                StateHasChanged();
            }
            else
            {
                cards = new List<CardReadDTO>();
                message = $"Error: {response.ReasonPhrase}";
            }
            
            response = await httpClient.GetAsync($"rarity");
            apiResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<RarityReadDTO>? resultRarities = JsonSerializer.Deserialize<IEnumerable<RarityReadDTO>>(apiResponse, JsonOptions);
            rarites = resultRarities;

            response = await httpClient.GetAsync($"type");
            apiResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<TypeReadDTO>? resultType = JsonSerializer.Deserialize<IEnumerable<TypeReadDTO>>(apiResponse, JsonOptions);
            types = resultType;

            response = await httpClient.GetAsync($"artist");
            apiResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<ArtistReadDTO>? resultArtists =JsonSerializer.Deserialize<IEnumerable<ArtistReadDTO>>(apiResponse, JsonOptions);
            artists = resultArtists;
        }
        public async Task FilterCards(EditContext editContext, string rarityCode,string type,Nullable<int> artistId,Nullable<int> id,string name,int pageSize,Nullable<int> pageNumber,string text, string sort)
        {
            this.rarityCode = rarityCode;
            HttpClient httpClient = httpClientFactory.CreateClient("CardsAPI");
            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            HttpResponseMessage response = await httpClient.GetAsync($"cards?PageNumber={pageNumber}&PageSize={pageSize}&Name={name}&SetCode={setCode}&ArtistId={artistId}&RarityCode={rarityCode}&type={type}&Text={text}&api-version={apiVersion}&sortMethod={sort}");

            string apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                PagedResponse<IEnumerable<CardReadDTO>>? result =
                        JsonSerializer.Deserialize<PagedResponse<IEnumerable<CardReadDTO>>>(apiResponse, JsonOptions);
                this.cards = result?.Data;
                StateHasChanged();           
            }
            else
            {
                cards = new List<CardReadDTO>();
                message = $"Error: {response.ReasonPhrase}";
            }
            StateHasChanged();
        }

        public async Task AddCardToDatabase(CardReadDTO card)
        {
            using var client = httpClientFactory.CreateClient("DeckAPI");

            var request = new HttpRequestMessage(HttpMethod.Post, "deck");
            request.Content = new StringContent(JsonSerializer.Serialize(card), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add card to database. Status code: {response.StatusCode}");
            }
        }

        public void NavigateToCardDetailsPage(CardReadDTO card)
        {
            NavigationManager.NavigateTo($"/card/{card.Id}");
        }

    }
}