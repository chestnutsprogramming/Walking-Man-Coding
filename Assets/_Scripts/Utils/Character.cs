using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject[] goals;
    public GameObject tile_grid;
    public GameObject visionCube;

    [Header("Tile Map")]
    public float tileSize = 2.5f;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float actionDelay = 0.25f;

    // internal state
    private TU tile_util;
    private VisionCube vc;

    private Queue<IEnumerator> visualQueue = new();  // all animations and logs
    private bool playingVisuals = false;
    private bool isAnimating = false;

    // backend simulation
    private Vector3 simPos;
    private direction simFacing;

    void Awake()
    {
        tile_util = tile_grid.GetComponent<TU>();
        vc = visionCube.GetComponent<VisionCube>();

        simPos = transform.position;
        simFacing = FacingFromRotation(transform.rotation.eulerAngles.y);
        // Debug.Log(simFacing);
    }

    void Start()
    {
        // Position VisionCube in front of the player
        vc.transform.position = simPos + FacingVector(simFacing) * tileSize;
    }

    // ============ PUBLIC API ============

    public void forward()
    {
        Vector3 next = simPos + FacingVector(simFacing) * tileSize;

        if (checkMove(next))
        {
            simPos = next;

            // enqueue movement
            visualQueue.Enqueue(AnimateMove(next));

            // enqueue VisionCube update
            visualQueue.Enqueue(AnimateVisionCube(simPos + FacingVector(simFacing) * tileSize));
        }
        else
        {
            visualQueue.Enqueue(ShowLogCoroutine(() => $"Invalid move. The character cannot move on the tile ({tile_util.getCurrentTileType(next)})"));
        }
    }

    public void turn()
    {
        simFacing = (direction)(((int)simFacing + 1) % 4);
        visualQueue.Enqueue(AnimateTurn());

        // Update VisionCube to be in front of new facing
        visualQueue.Enqueue(AnimateVisionCube(simPos + FacingVector(simFacing) * tileSize));
    }

    public bool canMoveForward()
    {
        Vector3 next = simPos + FacingVector(simFacing) * tileSize;
        if (!checkMove(next))
            visualQueue.Enqueue(ShowLogCoroutine(() => $"Cannot move forward. Visuals {simFacing}"));
        return checkMove(next);
    }

    public void Play()
    {
        Debug.Log($"Playing {!playingVisuals}, moves queued {visualQueue.Count}");
        if (!playingVisuals && visualQueue.Count > 0)
            StartCoroutine(PlayVisuals());
    }

    public void print(params object[] args)
    {
        visualQueue.Enqueue(PrintCoroutine(args));
    }


    // ============ VISUAL EXECUTION ============

    IEnumerator PlayVisuals()
    {
        playingVisuals = true;
        while (visualQueue.Count > 0)
        {
            var next = visualQueue.Dequeue();
            yield return StartCoroutine(next);
            yield return new WaitForSeconds(actionDelay);
        }
        playingVisuals = false;
    }

    IEnumerator AnimateMove(Vector3 target)
    {
        isAnimating = true;
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        isAnimating = false;
    }

    IEnumerator AnimateTurn()
    {
        isAnimating = true;
        Quaternion start = transform.rotation;
        Quaternion end = start * Quaternion.Euler(0, 90, 0);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 4f;
            transform.rotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }
        isAnimating = false;
    }

    IEnumerator AnimateVisionCube(Vector3 target)
    {
        vc.transform.position = target;
        yield break;
    }

    IEnumerator ShowLogCoroutine(Func<string> msgFunc)
    {
        Debug.Log(msgFunc());
        yield break;
    }
    
    IEnumerator PrintCoroutine(object[] args)
    {
        if (args == null || args.Length == 0) yield break;

        string output = "";
        for (int i = 0; i < args.Length; i++)
            output += args[i] + " ";

        switch (args[0])
        {
            case "Log":
                Debug.Log(output);
                break;
            case "Warn":
                Debug.LogWarning(output);
                break;
            case "Error":
                Debug.LogError(output);
                break;
            default:
                output = "";
                foreach (var o in args)
                    output += o + " ";
                Debug.Log(output);
                break;
        }
        yield break;
    }

    // ============ HELPERS ============

    bool checkMove(Vector3 pos)
    {
        TileType t = tile_util.getCurrentTileType(pos);
        return t != TileType.None && t != TileType.water && t != TileType.wall && t != TileType.lava;
    }

    Vector3 FacingVector(direction d)
    {
        return d switch
        {
            direction.up    => new Vector3(0, 1, 0),   // +Y
            direction.down  => new Vector3(0, -1, 0),  // -Y
            direction.left  => new Vector3(-1, 0, 0),  // -X
            direction.right => new Vector3(1, 0, 0),   // +X
            _ => Vector3.zero
        };
    }

    direction FacingFromRotation(float rotY)
    {
        int quadrant = Mathf.RoundToInt(rotY / 90f) % 4;
        if (quadrant < 0) quadrant += 4;
        return quadrant switch
        {
            0 => direction.up,
            1 => direction.right,
            2 => direction.down,
            3 => direction.left,
            _ => direction.up
        };
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (GameObject goal in goals)
        {
            if (other.gameObject == goal)
            {
                visualQueue.Enqueue(ShowLogCoroutine(() => "Success"));
                Goal gs = goal.GetComponent<Goal>();
                gs.ActivateGoal();
                goal.SetActive(false);
            }
            else
            {
                visualQueue.Enqueue(ShowLogCoroutine(() => other.gameObject.name));
            }
        }
    }
}

public enum direction { up, right, down, left }
