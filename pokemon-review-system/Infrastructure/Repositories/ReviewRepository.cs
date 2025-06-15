using Infrastructure.Data;
using Service.Interfaces;

namespace Infrastructure.Repositories;
using Domain.Models;

public class ReviewRepository(DataContext context) : BaseRepository<Review>(context), IReviewRepository
{
}