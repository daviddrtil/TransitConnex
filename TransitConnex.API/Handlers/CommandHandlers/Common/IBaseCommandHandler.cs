namespace TransitConnex.API.Handlers.CommandHandlers.Common;

public interface IBaseCommandHandler<T>
{
    public Task<Guid> HandleCreate(T command);
    public Task HandleUpdate(T command);
    public Task HandleDelete(Guid id);
}
