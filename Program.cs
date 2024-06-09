using SnekGame.Models;
using SnekGame.Services;

var argParser = new ArgumentParser();
if (argParser.TryParseArgs(args))
{
	var drawer = new MapDrawer();
	var handler = new MapHandler(argParser.MapWidth, argParser.MapHeight, argParser.Foods, drawer);
	await handler.GameLoop();
}
else
{
	Console.WriteLine("-width <number> for width\n-height <number> for height\n-foods <number> for number of foods");
}
