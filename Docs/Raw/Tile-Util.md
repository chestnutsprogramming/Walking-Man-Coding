# TU (Tile-Util) Class Reference

`TU` is a Unity `MonoBehaviour` class that handles tile map information and identifies the type of tile at a given world position.

---

## Public Fields

| Field | Type | Description |
|-------|------|-------------|
| `waterSprites` | `Sprite[]` | Array of sprites representing water tiles. |
| `groundSprites` | `Sprite[]` | Array of sprites representing ground tiles. |
| `wallSprites` | `Sprite[]` | Array of sprites representing wall tiles. |
| `lavaSprites` | `Sprite[]` | Array of sprites representing lava tiles. |
| `sandSprites` | `Sprite[]` | Array of sprites representing sand tiles. |
| `tilemap` | `Tilemap` | Reference to the Unity `Tilemap` component that contains all tiles. |

---

## Public Methods

### `TileType getCurrentTileType(Vector3 worldPos)`

Returns the type of tile at the specified world position.

**Parameters:**

| Name | Type | Description |
|------|------|-------------|
| `worldPos` | `Vector3` | World position to check, e.g., the player’s position. |

**Returns:**  
`TileType` — the type of the tile at that position (`water`, `ground`, `wall`, `lava`, `sand`, or `None` if no match).

**Behavior:**
- Converts `worldPos` to tile cell coordinates using `tilemap.WorldToCell()`.
- Retrieves the sprite at that cell using `tilemap.GetSprite()`.
- Compares the sprite against each category of tiles (`wallSprites`, `waterSprites`, etc.) to determine type.
- Returns `TileType.None` if the tile has no sprite or doesn’t match any known type.

---

## Enums

### `TileType`

Defines all possible types of tiles:

```csharp
public enum TileType
{
    water,  // Water tiles
    ground, // Ground tiles
    wall,   // Wall tiles
    lava,   // Lava tiles
    sand,   // Sand tiles
    None    // No recognized tile
}
```

## Example Usage
```csharp
TU tileUtil = tilemapGameObject.GetComponent<TU>();
Vector3 playerPos = player.transform.position;

TileType currentTile = tileUtil.getCurrentTileType(playerPos);

if(currentTile == TileType.wall)
{
    Debug.Log("Player is facing a wall!");
}
```


## Raw File

[Character.cs](../../Assets/_Scripts/Utils/TU.cs ':include :type=code')