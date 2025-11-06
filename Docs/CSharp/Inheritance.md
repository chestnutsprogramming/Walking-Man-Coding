# Inheritance in Unity

Inheritance lets you reuse logic between related scripts.

```csharp
public class Enemy
{
    public int health = 100;
    public void TakeDamage(int amount) => health -= amount;
}

public class Boss : Enemy
{
    public void SpecialAttack() => Debug.Log("Boss uses special attack!");
}
```

In Unity:
- Derive your scripts from `MonoBehaviour` to make them attachable components.
- You can also derive one script from another for shared logic.

```csharp
public class FlyingEnemy : Enemy
{
    void Fly() => Debug.Log("Flying around!");
}
```