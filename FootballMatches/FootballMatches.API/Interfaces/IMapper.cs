using FootballMatches.API.Models;

public interface IMapper<TEntity, TDto>
{
    TEntity MapToEntity(TDto dto);
    TDto MapToDto(TEntity entity);
    IEnumerable<TEntity> MapToEntities(IEnumerable<TDto> dtos);
    IEnumerable<TDto> MapToDtos(IEnumerable<TEntity> entities);
    
}