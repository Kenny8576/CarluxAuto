using System;
using System.Text.Json;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class AuctionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config, ILogger<AuctionSvcHttpClient> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;
    }

    public async Task<List<Item>> GetItemsForSearchDb()
    {
    var lastUpdated = await DB.Find<Item, string>()
        .Sort(x => x.Descending(x => x.UpdatedAt))
        .Project(x => x.UpdatedAt.ToString())
        .ExecuteFirstAsync();

    return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdated);
    }


    //   public async Task<List<Item>> GetItemsForSearchDb()
    // {
    //     var lastUpdated = await DB.Find<Item, string>()
    //         .Sort(x => x.Descending(x => x.UpdatedAt))
    //         .Project(x => x.UpdatedAt.ToString())
    //         .ExecuteFirstAsync();

    //     return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] 
    //         + "/api/auctions?date=" + lastUpdated);
    // }

}
