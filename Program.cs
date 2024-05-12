using SnekGame.Models;
using SnekGame.Services;

var drawer = new MapDrawer();
var handler = new MapHandler(20, 20, 1, drawer);
await handler.GameLoop();
