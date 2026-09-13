using AuctionService.Data;
using AuctionService.DTO;
using AuctionService.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
//[Authorize]
public class AuctionsController(AuctionDbContext ctx) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = await ctx.Auctions
            .OrderBy(x => x.Item.Make)
            .ThenBy(x=>x.Item.Model)
            .ProjectToType<AuctionDto>()
            .ToListAsync(cancellationToken);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var response = await ctx
            .Auctions
            .ProjectToType<AuctionDto>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return response!=null ?
            Ok(response) : 
            NotFound();
    }

    [HttpPut]
	[Route("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateAuctionDto request,  string id, CancellationToken cancellationToken)
    {
        var auction = await ctx
            .Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (auction == null)
        {
            return NotFound();
        }

        request.Adapt(auction);
        auction.UpdatedAt = DateTime.UtcNow;

        await ctx.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var auctionToDelete = await ctx.Auctions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (auctionToDelete == null)
        {
            return NotFound();
        }
        
        ctx.Auctions.Remove(auctionToDelete);
        await ctx.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuctionDto request, CancellationToken cancellationToken)
    {
        var auction = request.Adapt<Auction>();
        ctx.Auctions.Add(auction);
        await ctx.SaveChangesAsync(cancellationToken);
        var response = auction.Adapt<AuctionDto>();
        return Created($"api/auctions/{response.Id}", response);
    }
}