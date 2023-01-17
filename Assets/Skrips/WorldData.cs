using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldData
{

    public static int[,,] CreatWorld(int size, int height, float scale, float terrainscale)
    {
        float[,] PerlinNoiseMap = MakePerlinNoise(size, scale);
        int[,,] data = MakeWorldTerrinData(PerlinNoiseMap, size, height, scale, terrainscale);

        return data;
    }

    public static float[,] MakePerlinNoise(int size, float scale)
    {
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);

        float[,] noiseMap = new float[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);

                noiseMap[x, y] = noiseValue;
            }
        }
        return noiseMap;
    }

    public static int[,,] MakeWorldTerrinData(float[,] PerlinNoiseMap, int size, int worldHeight, float scale, float terrainscale)
    {
        int[,,] data = new int[size, size, worldHeight];
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                int height = (int)Mathf.Round((PerlinNoiseMap[x, z] * terrainscale));
                if (height == 0)
                {
                    height++;
                }
                for (int y = 0; y < height; y++)
                {
                    data[x, z, y] = 1;
                }
            }
        }
        return data;
    }
}
