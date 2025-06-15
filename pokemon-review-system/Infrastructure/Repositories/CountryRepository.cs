using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;
using Domain.Models;

public class CountryRepository(DataContext context) : ReadOnlyBaseRepository<Country>(context), ICountryRepository
{
    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(c => c.Id == id);
    }
}