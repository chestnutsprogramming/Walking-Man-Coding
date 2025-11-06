# Classes in Unity

A class is a blueprint for objects or behaviors.

```csharp
public class Enemy
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
```

In Unity, scripts automatically define classes that inherit from `MonoBehaviour`.

```csharp
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
}
```

You attach these to GameObjects to give them functionality.

