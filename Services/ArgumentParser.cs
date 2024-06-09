using System.Collections.Generic;

namespace SnekGame.Services;

public class ArgumentParser
{
	public int MapWidth { get; private set; } = 20;
	public int MapHeight { get; private set; } = 20;
	public int Foods { get; private set; } = 1;

	public bool TryParseArgs(string[] args)
	{
		var argQueue = new Queue<string>(args);
		while (argQueue.Count > 0)
		{
			if (!ConsumeTokens(argQueue))
			{
				return false;
			}
		}
		return true;
	}

	private bool ConsumeTokens(Queue<string> argQueue)
	{
		string token = argQueue.Dequeue();
		if (token.Equals("-width", StringComparison.OrdinalIgnoreCase))
		{
			if (argQueue.TryDequeue(out string? possibleWidth))
			{
				if (int.TryParse(possibleWidth, out int actualWidth))
				{
					MapWidth = actualWidth;
				}
				else 
				{
					return false;
				}
			}
			else 
			{
				return false;
			}
		}
		else if (token.Equals("-height", StringComparison.OrdinalIgnoreCase))
		{
			if (argQueue.TryDequeue(out string? possibleHeight))
			{
				if (int.TryParse(possibleHeight, out int actualHeight))
				{
					MapHeight = actualHeight;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		else if (token.Equals("-foods", StringComparison.OrdinalIgnoreCase))
		{
			if (argQueue.TryDequeue(out string? possibleFoods))
			{
				if (int.TryParse(possibleFoods, out int actualFoods))
				{
					Foods = actualFoods;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		return true;
	}
}
