# While Loops in Unity

`while` loops run as long as a condition is true.

```csharp
void Start()
{
    int count = 0;
    while (count < 3)
    {
        Debug.Log("Loop count: " + count);
        count++;
    }
}
```

**Warning:** Avoid infinite loops in Unity. They freeze the game since Unity’s main thread never returns to the engine.

