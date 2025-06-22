using Domain.Models;
using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class Seed(DataContext context)
{
    public void SeedDataContext(UserManager<User> userManager)
    {
        if (!context.PokemonOwners.Any())
        {
            var currentDate = DateTimeOffset.UtcNow.DateTime;

            // 1. Create Users First
            var jackUser = new User { UserName = "jack.london@pokemon.com", Email = "jack.london@pokemon.com" };
            var harryUser = new User { UserName = "harry.potter@pokemon.com", Email = "harry.potter@pokemon.com" };
            var ashUser = new User { UserName = "ash.ketchum@pokemon.com", Email = "ash.ketchum@pokemon.com" };
            
             userManager.CreateAsync(jackUser, "Password123!");
             userManager.CreateAsync(harryUser, "Password123!");
             userManager.CreateAsync(ashUser, "Password123!");

            // 2. Create Categories
            var electricCategory = new Category 
            { 
                Name = "Electric",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var waterCategory = new Category 
            { 
                Name = "Water",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var leafCategory = new Category 
            { 
                Name = "Leaf",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            context.Categories.AddRange(electricCategory, waterCategory, leafCategory);
            context.SaveChanges();

            // 3. Create Countries
            var kanto = new Country 
            { 
                Name = "Kanto",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var saffron = new Country 
            { 
                Name = "Saffron City",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var millet = new Country 
            { 
                Name = "Millet Town",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            context.Countries.AddRange(kanto, saffron, millet);
            context.SaveChanges();

            // 4. Create Owners with User IDs
            var jack = new Owner 
            { 
                FirstName = "Jack", 
                LastName = "London", 
                Gym = "Brocks Gym", 
                CountryId = kanto.Id, 
                UserId = jackUser.Id,
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var harry = new Owner 
            { 
                FirstName = "Harry", 
                LastName = "Potter", 
                Gym = "Mistys Gym", 
                CountryId = saffron.Id, 
                UserId = harryUser.Id,
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var ash = new Owner 
            { 
                FirstName = "Ash", 
                LastName = "Ketchum", 
                Gym = "Ashs Gym", 
                CountryId = millet.Id, 
                UserId = ashUser.Id,
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            context.Owners.AddRange(jack, harry, ash);
            context.SaveChanges();

            // 5. Create Pokemons
            var pikachu = new Pokemon 
            { 
                Name = "Pikachu", 
                BirthDate = new DateTime(1903,1,1),
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var squirtle = new Pokemon 
            { 
                Name = "Squirtle", 
                BirthDate = new DateTime(1903,1,1),
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var venasaur = new Pokemon 
            { 
                Name = "Venasuar", 
                BirthDate = new DateTime(1903,1,1),
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            context.Pokemons.AddRange(pikachu, squirtle, venasaur);
            context.SaveChanges();
            
            // 6. Create Reviewers
            var teddy = new Reviewer 
            { 
                FirstName = "Teddy", 
                LastName = "Smith",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var taylor = new Reviewer 
            { 
                FirstName = "Taylor", 
                LastName = "Jones",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            var jessica = new Reviewer 
            { 
                FirstName = "Jessica", 
                LastName = "McGregor",
                CreatedDate = currentDate,
                ModifiedDate = currentDate
            };
            context.Reviewers.AddRange(teddy, taylor, jessica);
            context.SaveChanges();

            // 7. Create Reviews
            var reviews = new List<Review>
            {
                new Review { 
                    Title = "Pikachu", 
                    Content = "Pickahu is the best pokemon, because it is electric", 
                    Rating = 5, 
                    ReviewerId = teddy.Id, 
                    PokemonId = pikachu.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Pikachu", 
                    Content = "Pickachu is the best a killing rocks", 
                    Rating = 5, 
                    ReviewerId = taylor.Id, 
                    PokemonId = pikachu.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Pikachu", 
                    Content = "Pickchu, pickachu, pikachu", 
                    Rating = 1, 
                    ReviewerId = jessica.Id, 
                    PokemonId = pikachu.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Squirtle", 
                    Content = "squirtle is the best pokemon, because it is electric", 
                    Rating = 5, 
                    ReviewerId = teddy.Id, 
                    PokemonId = squirtle.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Squirtle", 
                    Content = "Squirtle is the best a killing rocks", 
                    Rating = 5, 
                    ReviewerId = taylor.Id, 
                    PokemonId = squirtle.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Squirtle", 
                    Content = "squirtle, squirtle, squirtle", 
                    Rating = 1, 
                    ReviewerId = jessica.Id, 
                    PokemonId = squirtle.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Veasaur", 
                    Content = "Venasuar is the best pokemon, because it is electric", 
                    Rating = 5, 
                    ReviewerId = teddy.Id, 
                    PokemonId = venasaur.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Veasaur", 
                    Content = "Venasuar is the best a killing rocks", 
                    Rating = 5, 
                    ReviewerId = taylor.Id, 
                    PokemonId = venasaur.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new Review { 
                    Title = "Veasaur", 
                    Content = "Venasuar, Venasuar, Venasuar", 
                    Rating = 1, 
                    ReviewerId = jessica.Id, 
                    PokemonId = venasaur.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                }
            };
            context.Reviews.AddRange(reviews);
            context.SaveChanges();

            // 8. Create PokemonCategories
            context.PokemonCategories.AddRange(
                new PokemonCategory { 
                    PokemonId = pikachu.Id, 
                    CategoryId = electricCategory.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new PokemonCategory { 
                    PokemonId = squirtle.Id, 
                    CategoryId = waterCategory.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new PokemonCategory { 
                    PokemonId = venasaur.Id, 
                    CategoryId = leafCategory.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                }
            );
            context.SaveChanges();

            // 9. Create PokemonOwners
            context.PokemonOwners.AddRange(
                new PokemonOwner { 
                    PokemonId = pikachu.Id, 
                    OwnerId = jack.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new PokemonOwner { 
                    PokemonId = squirtle.Id, 
                    OwnerId = harry.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                },
                new PokemonOwner { 
                    PokemonId = venasaur.Id, 
                    OwnerId = ash.Id,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate
                }
            );
            context.SaveChanges();
        }
    }
}
