using System;
using AuctionService.Data;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly AuctionDbContext _context;

    public BidPlacedConsumer(AuctionDbContext context)
    {
        _context = context;
    }
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("---> Consuming Bid Placed");
        var bidAuction = await _context.Auctions.FindAsync(Guid.Parse( context.Message.AuctionId));

        if(bidAuction.CurrentHighBid == null || context.Message.BidStatus.Contains("Accepted")
            && context.Message.Amount > bidAuction.CurrentHighBid )
        {
            bidAuction.CurrentHighBid = context.Message.Amount;
            await _context.SaveChangesAsync();
        }
    }
}
