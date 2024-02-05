namespace CRUDdpto.WebAPI.Repositories
{
    public interface IDAO<T>
    {
        void Create(T entity);

        List<T> Read();

        T Update(T entity);

        void Delete(int id);

        T FindById(int id);
    }
}
