using Domain.Models;
using System; // Make sure this using directive is present for DateTime

namespace Infrastructure.Data;

public class Seed(DataContext context)
{
    public void SeedDataContext()
    {
        if (!context.PokemonOwners.Any())
        {
            var currentDate = DateTimeOffset.UtcNow.DateTime;
            var pokemonOwners = new List<PokemonOwner>()
            {
                new PokemonOwner()
                {
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    Pokemon = new Pokemon()
                    {
                        Name = "Pikachu",
                        BirthDate = new DateTime(1903,1,1),
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        PokemonCategories = new List<PokemonCategory>()
                        {
                            new PokemonCategory { 
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Category = new Category() { 
                                    Name = "Electric",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                }
                            }
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { 
                                Title="Pikachu", 
                                Content = "Pickahu is the best pokemon, because it is electric", 
                                Rating = 5,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Teddy", 
                                    LastName = "Smith",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                            new Review { 
                                Title="Pikachu", 
                                Content = "Pickachu is the best a killing rocks", 
                                Rating = 5,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Taylor", 
                                    LastName = "Jones",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                            new Review { 
                                Title="Pikachu",
                                Content = "Pickchu, pickachu, pikachu", 
                                Rating = 1,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Jessica", 
                                    LastName = "McGregor",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                        }
                    },
                    Owner = new Owner()
                    {
                        FirstName = "Jack",
                        LastName = "London",
                        Gym = "Brocks Gym",
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        Country = new Country()
                        {
                            Name = "Kanto",
                            CreatedDate = currentDate,
                            ModifiedDate = currentDate
                        }
                    }
                },
                new PokemonOwner()
                {
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    Pokemon = new Pokemon()
                    {
                        Name = "Squirtle",
                        BirthDate = new DateTime(1903,1,1),
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        PokemonCategories = new List<PokemonCategory>()
                        {
                            new PokemonCategory { 
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Category = new Category() { 
                                    Name = "Water",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                }
                            }
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { 
                                Title= "Squirtle", 
                                Content = "squirtle is the best pokemon, because it is electric", 
                                Rating = 5,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Teddy", 
                                    LastName = "Smith",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                            new Review { 
                                Title= "Squirtle",
                                Content = "Squirtle is the best a killing rocks", 
                                Rating = 5,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Taylor", 
                                    LastName = "Jones",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                            new Review { 
                                Title= "Squirtle", 
                                Content = "squirtle, squirtle, squirtle", 
                                Rating = 1,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Jessica", 
                                    LastName = "McGregor",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                        }
                    },
                    Owner = new Owner()
                    {
                        FirstName = "Harry",
                        LastName = "Potter",
                        Gym = "Mistys Gym",
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        Country = new Country()
                        {
                            Name = "Saffron City",
                            CreatedDate = currentDate,
                            ModifiedDate = currentDate
                        }
                    }
                },
                new PokemonOwner()
                {
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    Pokemon = new Pokemon()
                    {
                        Name = "Venasuar",
                        BirthDate = new DateTime(1903,1,1),
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        PokemonCategories = new List<PokemonCategory>()
                        {
                            new PokemonCategory { 
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Category = new Category() { 
                                    Name = "Leaf",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                }
                            }
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { 
                                Title="Veasaur",
                                Content = "Venasuar is the best pokemon, because it is electric", 
                                Rating = 5,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Teddy", 
                                    LastName = "Smith",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                            new Review { 
                                Title="Veasaur",
                                Content = "Venasuar is the best a killing rocks", 
                                Rating = 5,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Taylor", 
                                    LastName = "Jones",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                            new Review { 
                                Title="Veasaur",
                                Content = "Venasuar, Venasuar, Venasuar", 
                                Rating = 1,
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                Reviewer = new Reviewer(){ 
                                    FirstName = "Jessica", 
                                    LastName = "McGregor",
                                    CreatedDate = currentDate,
                                    ModifiedDate = currentDate
                                } 
                            },
                        }
                    },
                    Owner = new Owner()
                    {
                        FirstName = "Ash",
                        LastName = "Ketchum",
                        Gym = "Ashs Gym",
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        Country = new Country()
                        {
                            Name = "Millet Town",
                            CreatedDate = currentDate,
                            ModifiedDate = currentDate
                        }
                    }
                }
            };
            context.PokemonOwners.AddRange(pokemonOwners);
            context.SaveChanges();
        }
    }
}
