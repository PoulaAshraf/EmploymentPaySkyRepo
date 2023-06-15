using System.Linq.Expressions;

namespace EmploymentApi.Contracts
{
    public interface IBase<T> where T : class
    {
        T GetById(Guid id);

        T GetById(int id);

        List<T> GetAll();

        List<T> GetAll(string[] includes = null);

        T Insert(T entity);

        T Update(T entity);
       

        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        List<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);

        //Soft Delete
        T SoftDelete(int iD, bool isActive, string columnName);


    }
}
