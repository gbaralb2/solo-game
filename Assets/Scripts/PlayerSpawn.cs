using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private float[] pos = new float[2];
    private static PlayerSpawn instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Spawn(float[] spawnPos)
    {
        gameObject.transform.position = new Vector3(pos[0], pos[1], 0);
    }

    public void UpdateSpawn(float[] newPos)
    {
        pos[0] = newPos[0];
        pos[1] = newPos[1];
    }

    public float[] GetPos()
    {
        return pos;
    }
}
