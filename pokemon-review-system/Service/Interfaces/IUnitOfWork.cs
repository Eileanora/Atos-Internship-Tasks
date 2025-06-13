namespace Service.Interfaces;


public interface IUnitOfWork : IDisposable
{
    IPokemonRepository PokemonRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICountryRepository CountryRepository { get; }

    Task<int> SaveChangesAsync();
}
