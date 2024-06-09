using SnekGame.Models;
using System.Collections.Generic;

namespace SnekGame.Services;

public class MapHandler 
{
	private readonly MapItem[,] _map;
	private readonly int _width;
	private readonly int _height;
	private readonly Snek _snek;
	private readonly Food[] _foods;
	private readonly IMapDrawer _mapDrawer;
	private readonly Stack<Action> _actionStack = new();

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
		_ = _mapDrawer.DrawAsync(_map, _width, _height);
		var gameOn = true;
		using PlayerInputHandler playerInput = new();
		while (gameOn)
		{
			await Task.Delay(200);
			if (_snek.TryMove(_map, _width, _height, playerInput.CurrentDirection))
			{
				CheckFoodAte();
				MoveSnekInMap();
				ExecuteEvents();
			}
			else
			{
				playerInput.Stop();
				gameOn = false;
			}
			_ = _mapDrawer.DrawAsync(_map, _width, _height);
		}
	}

	private void MoveSnekInMap() 
	{
		_map[_snek.HeadCoordinate.X, _snek.HeadCoordinate.Y].ItemType = MapItemType.Snek;
		if (_map[_snek.HeadCoordinate.X, _snek.HeadCoordinate.Y].ItemType != MapItemType.Food)
		{
			_map[_snek.LastTailCoordinate.X, _snek.LastTailCoordinate.Y].ItemType = MapItemType.None;
		}
	}

	private void CheckFoodAte()
	{
		if (_map[_snek.HeadCoordinate.X, _snek.HeadCoordinate.Y].ItemType == MapItemType.Food)
		{
			_actionStack.Push(() => 
				_ = _foods[_map[_snek.HeadCoordinate.X, _snek.HeadCoordinate.Y].Index]
					.TrySetPosition(_map, _width, _height));
		}
	}

	private void ExecuteEvents()
	{
		while(_actionStack.Count > 0)
		{
			_actionStack
				.Pop()
				.Invoke();
		}
	}
}
