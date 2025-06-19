using Domain.Models;
using Service.DTOs;

namespace Service.Mappers;

public interface IBaseMapper<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    TDto ToListDto(TEntity entity);
    TDto ToDetailDto(TEntity entity);
    TEntity ToEntity(TDto dto);
}
