using UnityEngine;

public static class WorldData
{
    //för att håla koll på vilken chunk programet är på
    public static int chunkCountX;
    public static int chunkCountZ;

    //spara instälingar 
    public static int wChunks;
    public static int wSize;
    public static int wHeight;

    static int[,,] data;

    //ger data till voxel renderer som renderar den chunk
    public static int[,,] GetData
    {
        get
        {
            return Dataspliter();
        }
    }

    //skapar varablar för att splita datan till chunks och kallar MakePerlinNoise och MakeWorldTerrinData
    //kontrol metod (fördelar upgifter till olika metoder)
    public static void CreatWorld(int size, int height, float scale, float terrainscale, int chunks)
    {
        //spara variablar
        wChunks = chunks;
        wSize = size;
        wHeight = height;

        //för att skappa hella kartan
        size = size * chunks * chunks;


        float[,] PerlinNoiseMap = MakePerlinNoise(size, scale);
        MakeWorldTerrinData(PerlinNoiseMap, size, height, scale, terrainscale);
    }

    //skapar ett data sätt av PerlinNoise och offsetar den 
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

    //går igenom varje positon av PerlinNoise mapen bestäm om det ska vare att block där eller inte 
    public static void MakeWorldTerrinData(float[,] PerlinNoiseMap, int size, int worldHeight, float scale, float terrainscale)
    {
        data = new int[size, size, worldHeight];

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
    }

    //splitrar terring data på flera chunks
    public static int[,,] Dataspliter()
    {
        int[,,] splitData = new int[wSize, wSize, wHeight];
        for (int x = 0; x < wSize; x++)
        {
            for (int z = 0; z < wSize; z++)
            {
                for (int y = 0; y < wHeight; y++)
                {
                    splitData[x, z, y] = data[x + (chunkCountX * wSize), z + (chunkCountZ * wSize), y];

                }
            }
        }

        //orjenterar datan så den lägger utt den rätt

        chunkCountX++;
        if (chunkCountX >= wChunks)
        {
            chunkCountX = 0;
            chunkCountZ++;
        }

        return splitData;
    }
}


