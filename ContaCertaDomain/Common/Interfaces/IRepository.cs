namespace ContaCerta.Domain.Common.Interfaces;

public interface IRepository<T>
{
    public T? Find(int Id);
    public T Save(T entity);
    public Task Delete(int Id);
}