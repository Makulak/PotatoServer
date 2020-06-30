namespace PotatoServer.Services.Interfaces
{
    public interface IConnectionService
    {
        public void RemovePlayer(string userName);
        public void AddPlayer(string userName, string connectionId);
    }
}
