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
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Howest.MagicCards.Web.Pages
{
    public partial class CardDetailed
    {
        private int? cardId;
        private CardReadDetailDTO? card;
        private readonly JsonSerializerOptions JsonOptions;

        [Parameter]
        public int Id { get; set; }

        public CardDetailed()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected override async Task OnInitializedAsync()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("CardsAPI");

            httpClient.DefaultRequestHeaders.Add("api-version", "1.5");

            HttpResponseMessage response = await httpClient.GetAsync($"Cards/{Id}/detailed");
            string apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                CardReadDetailDTO? result =
                        JsonSerializer.Deserialize<CardReadDetailDTO>(apiResponse, JsonOptions);
                card = result;
                StateHasChanged();
            }
            else
            {
                card = new CardReadDetailDTO();
            }
        }
    }
}