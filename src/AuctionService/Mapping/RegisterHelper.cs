using AuctionService.DTO;
using AuctionService.Entities;
using Mapster;

namespace AuctionService.Mapping;

public class RegisterHelper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Auction, AuctionDto>()
            .Map(dest => dest.Make, src => src.Item.Make)
            .Map(dest => dest.Model, src => src.Item.Model)
            .Map(dest => dest.Color, src => src.Item.Color)
            .Map(dest => dest.Mileage, src => src.Item.Mileage)
            .Map(dest => dest.Year, src => src.Item.Year)
            .Map(dest => dest.ImageUrl, src => src.Item.ImageUrl)
            .Map(dest => dest.Description, src => src.Item.Description);

        config.NewConfig<AuctionDto, Auction>()
            .Map(dest => dest.Item.Make, src => src.Make)
            .Map(dest => dest.Item.Model, src => src.Model)
            .Map(dest => dest.Item.Color, src => src.Color)
            .Map(dest => dest.Item.Mileage, src => src.Mileage)
            .Map(dest => dest.Item.Year, src => src.Year)
            .Map(dest => dest.Item.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.Item.Description, src => src.Description);

        config.NewConfig<UpdateAuctionDto, Item>()
            .IgnoreNullValues(true);

        config.NewConfig<UpdateAuctionDto, Auction>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Item!)
            .Map(dest => dest.UpdatedAt, _ => DateTime.UtcNow)
            .AfterMapping((src, dest) => src.Adapt(dest.Item));

        config.NewConfig<CreateAuctionDto, Auction>()
            .Map(dest => dest.Item.Make, src => src.Make)
            .Map(dest => dest.Item.Model, src => src.Model)
            .Map(dest => dest.Item.Color, src => src.Color)
            .Map(dest => dest.Item.Mileage, src => src.Mileage)
            .Map(dest => dest.Item.Year, src => src.Year)
            .Map(dest => dest.Item.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.Item.Description, src => src.Description);

    }
}