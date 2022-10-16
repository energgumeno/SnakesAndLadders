using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLaddersLibrary.Boards
{
    public interface ITileFactory
    {
        ITile CreateTile(int tilePosition);
    }
}
