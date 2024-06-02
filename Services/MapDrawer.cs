using SnekGame.Models;

namespace SnekGame.Services;

public class MapDrawer : IMapDrawer
{
	private bool _firstRender = true;
	public Task DrawAsync(MapItem[,] map, int width, int height)
	{
		if (!_firstRender)
		{
			Console.Clear();
		}
		else 
		{
			_firstRender = false;
		}
		for (int h = 0; h < height; h++)
		{
			for (int w = 0; w < width; w++)
			{
				Console.Write(GetTile(map[w,h].ItemType));
			}
			Console.Write('\n');
		}
		return Task.CompletedTask;
	}

	private string GetTile(MapItemType item)
	{
		return item switch
		{
			MapItemType.None => "~~",
			MapItemType.Snek => "██",
			MapItemType.Food => "ff",
			_ => throw new Exception($"could not draw tile of type {item}")
		};
	}
}
