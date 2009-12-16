namespace Mavis.Core
{
    public interface IEntity: IEntity<int>
    {
        
    }

    public interface IEntity<TId>
    {
        TId ID { get; }
        bool IsTransient { get; }
    }
}