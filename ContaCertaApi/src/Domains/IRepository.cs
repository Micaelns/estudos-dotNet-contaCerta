namespace ContaCertaApi.Domains.Interfaces
{
    public interface IRepository<T>
    {
        public T Find(int Id);
        public T Save(T entity);
    }
}