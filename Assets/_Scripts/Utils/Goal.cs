using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject NextGoal;
    
    public void ActivateGoal()
    {
        if (NextGoal == null) return;
        NextGoal.SetActive(true);
    }
}
