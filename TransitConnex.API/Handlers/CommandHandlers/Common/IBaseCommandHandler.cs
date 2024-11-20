namespace TransitConnex.API.Handlers.CommandHandlers.Common
{
    public interface IBaseCommandHandler<T>
    {
        public Task HandleCreate(T command);
        public Task HandleUpdate(T command);
        public Task HandleDelete(T command);
    }
}
