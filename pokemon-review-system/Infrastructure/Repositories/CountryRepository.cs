using Infrastructure.Data;
using Service.Interfaces;

namespace Infrastructure.Repositories;
using Domain.Models;

public class CountryRepository(DataContext context) : ReadOnlyBaseRepository<Country>(context), ICountryRepository
{
}