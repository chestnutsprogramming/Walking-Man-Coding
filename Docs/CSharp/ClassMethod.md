# Class.Method();

In Unity, almost everything interacts through methods.

```csharp
using UnityEngine;

public class Player /* our own class */ : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Game started!"); // Class: Debug; Method: Log;
        Jump(); // skips `class.` since this method is our own class
    }

    void Jump() // method
    {
        Debug.Log("The player jumped!");
    }
}

```

**Explanation**
- `Start()` is a Unity message method called once when the object is enabled.
- You can call your own method (`Jump()`) from other methods using `Class.Method()` format if it’s static, or simply `Method()` if inside the same class.
