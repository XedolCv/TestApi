namespace TestApi.Inter;

public interface IModified
{
    DateTime createTime { get; set; }
    DateTime? updateTime { get; set; }
    Guid createUserId { get; set; }
    Guid? updateUserId { get; set; }
}