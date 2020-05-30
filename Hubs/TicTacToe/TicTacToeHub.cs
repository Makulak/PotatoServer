using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.TicTacToe
{
    public class TicTacToeHub : Hub
    {
        private static BoardGetVm board = new BoardGetVm();

        public TicTacToeHub()
        {
        }

        public async Task GetBoard()
        {
            await Clients.All.SendAsync("get-info", board);
        }

        public async Task MakeMove(Move move)
        {
            board.items.Add(new BoardItemVm { PosX = move.x, PosY = move.y, Symbol = move.symbol[0] });

            await Clients.All.SendAsync("player-moved", board);
        }
    }
}
