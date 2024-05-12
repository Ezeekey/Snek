using SnekGame.Models;

namespace SnekGame.Services;

public class MapHandler 
{
	private readonly MapItem[,] _map;
	private readonly int _width;
	private readonly int _height;
	private readonly Snek _snek;
	private readonly Food[] _foods;
	private readonly IMapDrawer _mapDrawer;

	public MapHandler(
		int width, 
		int height,
		int foodNumber,
		IMapDrawer mapDrawer)
	{
		_mapDrawer = mapDrawer;
		_map = new MapItem[width, height];
		_width = width;
		_height = height;

		_snek = new Snek(
			_width, 
			_height, 
			_map);

		_foods = new Food[foodNumber];
		for(int i = 0; i < _foods.Length; i++)
		{
			_foods[i] = new Food(
				_width, 
				_height, 
				_map);
		}
	}

	public async Task GameLoop()
	{
		var gameOn = true;
		await _mapDrawer.DrawAsync(_map, _width, _height);
	}
}
