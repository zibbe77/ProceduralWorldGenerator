using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkCreater : MonoBehaviour
{
    public int chunks;
    public GameObject prefab;

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
        WorldData.CreatWorld(worldSize, worldHeighet, worldScale, terrainscale);
    }

    void MakeCunk()
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);

    }
}
