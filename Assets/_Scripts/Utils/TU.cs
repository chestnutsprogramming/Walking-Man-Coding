using UnityEngine;
using UnityEngine.Tilemaps;

public class TU : MonoBehaviour
{
    public Sprite[] waterSprites;
    public Sprite[] groundSprites;
    public Sprite[] wallSprites;
    public Sprite[] lavaSprites;
    public Sprite[] sandSprites;

    // public Sprite changeTest;
    public Tilemap tilemap;

    public TileType getCurrentTileType(Vector3 worldPos)
    {
        // Convert world position (like your character's) to tile cell position
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        // Read current tile sprite
        Sprite currentTileSprite = tilemap.GetSprite(cellPos);

        // Optional test: change it visually
        // if (changeTest != null)
        // {
        //     Tile tile = ScriptableObject.CreateInstance<Tile>();
        //     tile.sprite = changeTest;
        //     tilemap.SetTile(cellPos, tile);
        // }

        // Determine type
        if (currentTileSprite == null) return TileType.None;

        foreach (var s in wallSprites)
            if (s == currentTileSprite) return TileType.wall;

        foreach (var s in waterSprites)
            if (s == currentTileSprite) return TileType.water;

        foreach (var s in groundSprites)
            if (s == currentTileSprite) return TileType.ground;

        foreach (var s in lavaSprites)
            if (s == currentTileSprite) return TileType.lava;

        foreach (var s in sandSprites)
            if (s == currentTileSprite) return TileType.sand;

        return TileType.None;
    }
}


public enum TileType
{
    water,
    ground,
    wall,
    lava,
    sand,
    None
}
