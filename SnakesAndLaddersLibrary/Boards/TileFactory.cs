using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLaddersLibrary.Boards
{
    public class TileFactory : ITileFactory
    {

        public  ITile CreateTile(int tilePosition)
        {
            return new Tile(tilePosition);
        }
    }
}
