namespace Service.Interfaces;


public interface IUnitOfWork : IDisposable
{
    IPokemonRepository PokemonRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICountryRepository CountryRepository { get; }
    IOwnerRepository OwnerRepository { get; }
    IPokemonOwnerRepository PokemonOwnerRepository { get; }
    Task<int> SaveChangesAsync();
}
