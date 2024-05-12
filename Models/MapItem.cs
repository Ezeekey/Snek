namespace SnekGame.Models;

public struct MapItem
{
	public MapItemType ItemType;
	public int Index;
}

public enum MapItemType : byte
{
	None = 0,
	Snek = 1,
	Food = 2
}
