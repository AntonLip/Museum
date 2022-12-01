namespace Museum.Models.Interfaces.Service
{

    public interface IService<TModel, TModelDto, TModelAddDto, TModelUpdateDto, TId>
            where TModel : IEntity<TId>
            where TModelDto : IEntity<TId>
            where TModelAddDto : IEntity<TId>
            where TModelUpdateDto : IEntity<TId>
    {
        Task<TModelDto> AddAsync(TModelAddDto obj, CancellationToken cancellationToken = default);
        Task<IEnumerable<TModelDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TModelDto> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<TModelDto> UpdateAsync(TId id, TModelUpdateDto obj, CancellationToken cancellationToken = default);
        int GetCountEntity();
        Task<TModelDto> RemoveAsync(TId id, CancellationToken cancellationToken = default);
    }
}
