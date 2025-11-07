# Character Class Reference

`Character` is a Unity `MonoBehaviour` class that handles player movement, visual feedback, and interaction with a tile grid and vision system.

---

## Public Fields

### Inscribed Fields

| Field | Type | Description |
|-------|------|-------------|
| `goals` | `GameObject[]` | Array of goal objects for collision detection. |
| `tile_grid` | `GameObject` | Reference to the tile grid GameObject. |
| `visionCube` | `GameObject` | Reference to the VisionCube object for visual feedback. |

### Tile Map

| Field | Type | Description |
|-------|------|-------------|
| `tileSize` | `float` | The size of a tile in Unity units (default 2.5). |

### Movement

| Field | Type | Description |
|-------|------|-------------|
| `moveSpeed` | `float` | Speed of movement in units/sec (default 4f). |
| `actionDelay` | `float` | Delay between queued actions/animations (default 0.25s). |

---

## Public Methods

### `void forward()`
Moves the character one tile forward if the tile is passable.
- Enqueues animation for movement and VisionCube update.
- Logs invalid moves.

### `void turn()`
Rotates the character 90° clockwise.
- Updates VisionCube position to match new facing.

### `bool canMoveForward()`
Checks if the character can move forward.
- Returns `true` if the next tile is passable.
- Enqueues a log if movement is blocked.

### `void Play()`
Executes all queued visual actions in order.
- Starts a coroutine if no other visuals are playing.

### `void print(params object[] args)`
Logs messages to Unity console with optional types: `"Log"`, `"Warn"`, `"Error"`.
- Queues output as a coroutine.

---

## Internal / Visual Execution Methods

> These methods manage animations and visual feedback; generally not called externally.

- `IEnumerator PlayVisuals()`: Executes queued visual coroutines sequentially.
- `IEnumerator AnimateMove(Vector3 target)`: Moves character toward target position smoothly.
- `IEnumerator AnimateTurn()`: Rotates character 90° smoothly.
- `IEnumerator AnimateVisionCube(Vector3 target)`: Moves VisionCube to a new target position.
- `IEnumerator ShowLogCoroutine(Func<string> msgFunc)`: Logs a message via coroutine.
- `IEnumerator PrintCoroutine(object[] args)`: Handles queued debug output.

---

## Helpers

- `bool checkMove(Vector3 pos)`: Returns whether a tile is passable.
- `Vector3 FacingVector(direction d)`: Returns a Vector3 representing the facing direction.
- `direction FacingFromRotation(float rotY)`: Converts Y-axis rotation to a `direction` enum.

---

## Unity Callbacks

- `void Awake()`: Initializes `tile_util`, `vc`, and simulation state.
- `void Start()`: Positions VisionCube in front of the character.
- `void OnCollisionEnter(Collision other)`: Detects collisions with goal objects and triggers success actions.

---

## Enums

### `direction`
Defines facing directions:
```csharp
public enum direction { up, right, down, left }
```

## Example Usage
```csharp
Character player = gameObject.GetComponent<Character>();

player.forward();     // Move forward
player.turn();        // Rotate 90° clockwise
player.canMoveForward(); // Check if forward is passable
player.Play();        // Execute queued visuals
player.print("Log", "Player moved");
```

## Raw File

[Character.cs](../../Assets/_Scripts/Utils/Character.cs ':include :type=code')