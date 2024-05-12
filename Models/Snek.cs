using System.Collections.Generic;

namespace SnekGame.Models;

public class Snek
{
	private readonly Queue<Coordinate> _body;

	public Snek(int width, int height, MapItem[,] map)
	{
		_body = new Queue<Coordinate>(width * height);
		var halfWidth = width / 2;
		var halfHeight = height / 2;
		for(int i = 0; i < 3; i++)
		{
			_body.Enqueue(new Coordinate
			{
				X = halfWidth + i,
				Y = halfHeight
			});
			map[halfWidth + i, halfHeight].ItemType = MapItemType.Snek;
		}
	}
}
