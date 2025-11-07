# First Script
## ReachTheGoal.cs
find the `ReachTheGoal` script in the project panel should be in the Assets folder, this script is where you will be coding, but we need to set it up for the first time.
Not all of this will make sense but lets try it out.

### Open the script
You will see this:
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTheGoal : MonoBehaviour
{

    void Awake()
    {
    }
    void Start()
    {

    }

}
```
We need to change this by adding some things like our `gameobjet` character
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTheGoal : MonoBehaviour
{
    public GameObject character; // The character object
    private Character controller; // The script for the character 
...

}
```

now with that we need to get our script from our character

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTheGoal : MonoBehaviour
{
...

    void Awake()
    {
        controller = character.GetComponent<Character>();
    }
    
...

}
```
GameObject's come with components we will see these later down the line.
These components let us change things about our object such as adding scripts.
In this case we are trying to find our script Character from our object character.
If you click on Character in the hierarchy then look in the inspector you will find that script named Character.
The code above did the same thing you did by looking for it.

## Createing the script object

In the `Hierarchy` right click press `Create Empty` rename this to script or a variant of,
from your `Project` window find the ReachTheGoal.cs script and drag it onto the object we just created.
This add the script for us to our object, and adds a script component in our inspector. in the script portion of the inspector
you will see `character` with the words empty gameobject or missing object... Drag the `Character` object from the Hierarchy
into this box.