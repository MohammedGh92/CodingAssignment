using System;

[Serializable]
public class TileO
{
    public TileType tileType;
    public TileO(TileType tileType)
    {
        SettileType(tileType);
    }

    public TileO()
    {
        SettileType(TileType.b1);
    }

    public void SettileType(TileType tileType)
    {
        this.tileType = tileType;
    }

    public TileType GetTileType()
    {
        return tileType;
    }
}