using System.Collections.Generic;

[System.Serializable]
public class GroundTileO
{
    public List<TileO> tiles;
    public GroundTileO()
    {
        tiles = new List<TileO>();
    }
}