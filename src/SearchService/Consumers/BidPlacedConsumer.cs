using System;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("----> Consuming Bid Placed");

        var bidAuction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);

        if(context.Message.BidStatus.Contains("Accepted") && context.Message.Amount > bidAuction.CurrentHighBid)
        {
            bidAuction.CurrentHighBid = context.Message.Amount;

            await bidAuction.SaveAsync();
        }
    }
}
