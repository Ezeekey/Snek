using SnekGame.Models;

namespace SnekGame.Services;

public interface IMapDrawer
{
	Task DrawAsync(MapItem[,] map, int width, int height);
}
