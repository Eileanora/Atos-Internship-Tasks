namespace Service.Interfaces;


public interface IUnitOfWork : IDisposable
{
    IPokemonRepository PokemonRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICountryRepository CountryRepository { get; }
    IOwnerRepository OwnerRepository { get; }
    IPokemonOwnerRepository PokemonOwnerRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IReviewerRepository ReviewerRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    Task<int> SaveChangesAsync();
}
