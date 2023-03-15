namespace TestApi.Inter;

public interface IEntity
{
    Guid id { get; set; }
}
public interface IEntity<T> where T : struct
{
    T id { get; set; }
}