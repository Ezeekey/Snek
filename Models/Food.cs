using System.Collections.Generic;

namespace SnekGame.Models;

public class Food
{
	public int X { get; private set; }
	public int Y { get; private set; }

	private Random rando = new Random();

	public Food(int width, int height, MapItem[,] map)
	{
		_ = TrySetPosition(map, width, height);
	}

	public bool TrySetPosition(
		MapItem[,] map,
		int mapWidth,
		int mapHeight)
	{
		int attempts = 10000;
		while(attempts > 0) 
		{
			X = rando.Next(mapWidth);
			Y = rando.Next(mapHeight);

			if (CheckGood(map))
			{
				map[X, Y].ItemType = MapItemType.Food;
				return true;
			}
			attempts -= 1;
		}
		// I'm just going to assume the player won here.
		return false;
	}

	private bool CheckGood(MapItem[,] map)
	{
		return map[X, Y].ItemType == MapItemType.None;
	}
}
