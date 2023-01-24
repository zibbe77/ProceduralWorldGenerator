using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
    //kontrol metod (fördelar upgifter till olika metoder)
    void Start()
    {
        MakeTerrain();
        MakeCunk();
    }

    //Skapar ett data sätt för värden 
    void MakeTerrain()
    {
        WorldData.CreatWorld(worldSize, worldHeighet, worldScale, terrainscale, chunks);
    }

    //seperarar datan i chunks för att unity har en gräns på vertesis i en mesh
    void MakeCunk()
    {
        //numerarar cunksen
        int name = 1;

        //plaserar utt dom    
        for (int z = 0; z < chunks; z++)
        {
            for (int x = 0; x < chunks; x++)
            {
                GameObject ChunksObj;
                ChunksObj = Instantiate(prefab, new Vector3(x * worldSize, 0, z * worldSize), Quaternion.identity);
                ChunksObj.name = (name).ToString();
                name++;
            }
        }
    }
}
