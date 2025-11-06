# If Statements in Unity

Use `if` to check conditions during gameplay.

```csharp
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log("Space pressed!");
    }

    if (transform.position.y < -5)
    {
        Debug.Log("Player fell off the map!");
    }
}
```

**Usage**
- Common in `Update()` for reacting to player input or world state.
- Combine with `else if` and `else` for multiple outcomes.