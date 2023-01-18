using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkCreater : MonoBehaviour
{

    public GameObject prefab;

    //chunks
    public int chunks;

    //terring instälningar
    public int worldSize;
    public int worldHeighet;
    public float worldScale;
    public float terrainscale;


    //skapa datan få den att hänga ihop

    void Awake()
    {

    }

    void Start()
    {
        MakeTerrain();
        MakeCunk();
    }

    void MakeTerrain()
    {
        WorldData.CreatWorld(worldSize, worldHeighet, worldScale, terrainscale, chunks);
    }

    void MakeCunk()
    {
        for (int x = 0; x < chunks; x++)
        {
            for (int z = 0; z < chunks; z++)
            {
                Instantiate(prefab, new Vector3(x * worldSize, 0, z * worldSize), Quaternion.identity);
            }
        }
    }
}
