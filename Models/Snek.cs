using System.Collections.Generic;

namespace SnekGame.Models;

public class Snek
{
	private readonly Queue<Coordinate> _body;
	private Direction _lastDirection;
	private Coordinate _headCoordinate;
	private Coordinate _lastTailCoordinate;

	public Coordinate HeadCoordinate { get => _headCoordinate; }
	public Coordinate LastTailCoordinate { get => _lastTailCoordinate; }

	public Snek(int width, int height, MapItem[,] map)
	{
		_body = new Queue<Coordinate>(width * height);
		_lastDirection = Direction.Right;
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
		_headCoordinate = new Coordinate
		{
			X = halfWidth + 2,
			Y = halfHeight
		};

		_lastTailCoordinate = new Coordinate
		{
			X = halfWidth - 1,
			Y = halfHeight
		};
	}

	public bool TryMove(
		MapItem[,] map,
		int width,
		int height,
		Direction newDirection)
	{
		CheckDirectionGood(ref newDirection);
		var newCoord = SetCoordinate(newDirection);
		if (!BoundsCheck(ref width, ref height, ref newCoord))
		{
			return false;
		}
		return CheckMap(map, ref newCoord);
	}

	private bool CheckMap(
		MapItem[,] map,
		ref Coordinate coord)
	{
		var mapItem = map[coord.X,coord.Y];
		switch(mapItem.ItemType)
		{
			case MapItemType.Snek:
				return false;
			case MapItemType.Food:
				_body.Enqueue(coord);
				_headCoordinate = coord;
				return true;
			default:
				_body.Enqueue(coord);
				_lastTailCoordinate = _body.Dequeue();
				_headCoordinate = coord;
				return true;
		}
	}

	private bool BoundsCheck(
			ref int width, 
			ref int height, 
			ref Coordinate coordinate)
	{
		return 
			coordinate.X >= 0 && 
			coordinate.X < width && 
			coordinate.Y >= 0 && 
			coordinate.Y < height;
	}

	private void CheckDirectionGood(ref Direction direction)
	{
		switch(_lastDirection)
		{
			case Direction.Right:
				if (direction == Direction.Left)
				{
					direction = _lastDirection;
				}
				break;
			case Direction.Down:
				if (direction == Direction.Up)
				{
					direction = _lastDirection;
				}
				break;
			case Direction.Left:
				if (direction == Direction.Right)
				{
					direction = _lastDirection;
				}
				break;
			case Direction.Up:
				if (direction == Direction.Down)
				{
					direction = _lastDirection;
				}
				break;
			default:
				break;
		}
	}

	private Coordinate SetCoordinate(Direction direction)
	{
		return direction switch 
		{
			Direction.Right =>
				new Coordinate { X = _headCoordinate.X + 1, Y = _headCoordinate.Y },
			Direction.Down =>
				new Coordinate { X = _headCoordinate.X, Y = _headCoordinate.Y + 1 },
			Direction.Left =>
				new Coordinate { X = _headCoordinate.X - 1, Y = _headCoordinate.Y },
			Direction.Up =>
				new Coordinate { X = _headCoordinate.X, Y = _headCoordinate.Y - 1},
			_ => new Coordinate { X = _headCoordinate.X, Y = _headCoordinate.Y }
		};
	}
}
