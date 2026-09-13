namespace AuctionService.DTO;

public class CreateAuctionDto
{
    public int ReservedPrice { get; set; }
    public required string Seller { get; set; }
    public DateTime AuctionEnd { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public required string Description { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public required string ImageUrl { get; set; }
}
