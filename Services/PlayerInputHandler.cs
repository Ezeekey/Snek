using SnekGame.Models;
using System.Threading;

namespace SnekGame.Services;

public class PlayerInputHandler : IDisposable
{
	public Direction CurrentDirection { get; private set; } = Direction.Right;
	private CancellationTokenSource _cancelSource = new();

	public PlayerInputHandler()
	{
		_ = Task.Run(() => ReadUserInput(), _cancelSource.Token);
	}

	public void Stop()
	{
		_cancelSource.Cancel();
	}

	public void Dispose()
	{
		_cancelSource.Dispose();
	}

	private void ReadUserInput()
	{
		while(true)
		{
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch(key.Key)
				{
					case ConsoleKey.RightArrow:
						CurrentDirection = Direction.Right;
						break;
					case ConsoleKey.DownArrow:
						CurrentDirection = Direction.Down;
						break;
					case ConsoleKey.LeftArrow:
						CurrentDirection = Direction.Left;
						break;
					case ConsoleKey.UpArrow:
						CurrentDirection = Direction.Up;
						break;
					default:
						break;
				}
			}
		}
	}
}
