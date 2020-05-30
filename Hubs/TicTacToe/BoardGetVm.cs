using System.Collections.Generic;

namespace PotatoServer.Hubs.TicTacToe
{
    public class BoardGetVm
    {
        public List<BoardItemVm> items { get; set; }

        public BoardGetVm()
        {
            items = new List<BoardItemVm>();
        }
    }
}
