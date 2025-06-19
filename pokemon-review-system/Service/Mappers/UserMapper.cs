using Domain.Models;
using Service.DTOs;

namespace Service.Mappers;

public static class UserMapper
{
    public static User ToEntity(this RegisterDto registerDto)
    {
        return new User
        {
            Email = registerDto.Email,
            UserName = Guid.NewGuid().ToString(),
        };
    }

    public static RegisterDto OwnerToRegisterDto(this CreateOwnerDto owner)
    {
        return new RegisterDto
        {
            Email = owner.Email,
            Password = owner.Password,
            ConfirmPassword = owner.ConfirmPassword,
        };
    }
}
