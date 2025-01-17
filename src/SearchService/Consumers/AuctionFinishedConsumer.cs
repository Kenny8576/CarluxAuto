using System;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        var finishedAuction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);

        if(context.Message.ItemSold)
        {
            finishedAuction.Winner = context.Message.Winner;
            finishedAuction.SoldAmount = (int)context.Message.Amount;
        }

        finishedAuction.Status = "Finish";

        await finishedAuction.SaveAsync();
    }
}
