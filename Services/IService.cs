namespace apbd_cw12.Services;

public interface IService<T, TDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> InsertAsync(TDto obj);
    Task DeleteAsync(params object[] keys);

    TDto MapToDto(T obj);
    T MapFromDto(TDto dto);
}
