using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Inter;

public interface IRepository: IDisposable 
{
    IEnumerable<T> GetList<T>() where T :class,IEntity,new ();
    IEnumerable<T> GetFiltredList<T>(List<T> ids) where T :class,IEntity,new();
    IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T :class,IEntity,new(); // получение одного объекта по id
    T GetOne<T>(Expression<Func<T, bool>> selector) where T :class,IEntity,new(); // получение одного объекта по id
    void Create<T>(T item) where T :class,IEntity,new(); // создание объекта
    void Update<T>(T item) where T :class,IEntity,new(); // обновление объекта
    void Delete<T>(T id) where T :class,IEntity,new(); // удаление объекта по id
    void Save();  // сохранение изменений
}

public class Repository: IRepository 
{
    private readonly MyContext _context;
    
    public Repository(MyContext context)
    {
        _context = context; 
        _context.Database.Migrate();
        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();
    }
    public void Dispose()
    {
        _context?.Dispose();
    }
    public IEnumerable<T> GetList<T>() where T :class,IEntity,new()
    {
        return _context.Set<T>().AsEnumerable();
    }
    public IEnumerable<T> GetFiltredList<T>(List<T> ids)  where T :class,IEntity,new()
    {
        return _context.Set<T>().Where(it => ids.Exists(j => j.id == it.id)).AsEnumerable();
    }
    public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector)   where T :class,IEntity,new()
    {
        if (typeof(T).GetInterfaces().Contains(typeof(IEntityGetting<T>)))
        {
            IEntityGetting<T> obj = (IEntityGetting<T>)new T();
            return obj.Get(_context, selector);
        }
        else
        {
            return _context.Set<T>().Where(selector).AsQueryable();
        }
    }

    public T GetOne<T>(Expression<Func<T, bool>> selector) where T : class, IEntity, new()
    {
        return _context.Set<T>().Where(selector).FirstOrDefault();
    }

    public void Create<T>(T item)where T :class,IEntity,new()
    {
        _context.Set<T>().Add(item);
    }
    public void Update<T>(T item)where T :class,IEntity,new()
    {
        _context.Set<T>().Entry(item).State = EntityState.Modified;
    }
    public void Delete<T>(T id) where T :class,IEntity,new()
    {
        T item = _context.Set<T>().Find(id);
        if (item != null) _context.Set<T>().Remove(item);
    }
    public void Save()
    {
        _context.SaveChanges();
    }
    
}