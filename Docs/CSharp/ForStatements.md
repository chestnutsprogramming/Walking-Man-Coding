# For Loops in Unity

`for` loops repeat code a specific number of times.

```csharp
void Start()
{
    for (int i = 0; i < 5; i++)
    {
        Debug.Log("Enemy " + i + " spawned");
    }
}
```

**Use cases**
- Spawning multiple objects
- Updating arrays or lists of game objects
