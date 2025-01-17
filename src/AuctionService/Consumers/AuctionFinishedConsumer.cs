using System;
using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuctionService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _context;

    public AuctionFinishedConsumer(AuctionDbContext context)
    {
        _context = context;
    }
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
         Console.WriteLine("---> Consuming Auction finished");

        var finishedAuction = await _context.Auctions.FindAsync(context.Message.AuctionId);

        if(context.Message.ItemSold)
        {
            finishedAuction.Winner = context.Message.Winner;
            finishedAuction.SoldAmount = context.Message.Amount;
        }

        finishedAuction.Status = finishedAuction.SoldAmount > finishedAuction.ReservePrice
            ? Status.Finished : Status.ReserveNotMet;

        await _context.SaveChangesAsync();
    }
}
