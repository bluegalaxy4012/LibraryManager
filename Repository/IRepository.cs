namespace LibraryManager.Repository;

public interface IRepository<T> where T: class
{
    T? Add(T entity);

    T? FindById(int id);

    T? Update(int id, T entity);

    T? Delete(int id);

    IEnumerable<T> GetAll();
}