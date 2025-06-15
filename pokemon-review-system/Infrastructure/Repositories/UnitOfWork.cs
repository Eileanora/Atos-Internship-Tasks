using Infrastructure.Data;
using Service.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;
    public IPokemonRepository PokemonRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ICountryRepository CountryRepository { get; }
    public IOwnerRepository OwnerRepository { get; }
public IPokemonOwnerRepository PokemonOwnerRepository { get; }

    public UnitOfWork(
        DataContext dbContext,
        IPokemonRepository pokemonRepository,
        ICategoryRepository categoryRepository,
        ICountryRepository countryRepository,
        IOwnerRepository ownerRepository,
        IPokemonOwnerRepository pokemonOwnerRepository)
    {
        _dbContext = dbContext;
        PokemonRepository = pokemonRepository;
        CategoryRepository = categoryRepository;
        CountryRepository = countryRepository;
        OwnerRepository = ownerRepository;
        PokemonOwnerRepository = pokemonOwnerRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async void Dispose(bool disposing)
    {
        if (disposing)
        {
            await _dbContext.DisposeAsync();
        }
    }
}
