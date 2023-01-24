using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{
    //sparar datan
    int[,,] data;

    //returnar storleken på data sättet 
    public int Width
    {
        get { return data.GetLength(0); }
    }
    public int Depth
    {
        get { return data.GetLength(1); }
    }
    public int Hight
    {
        get { return data.GetLength(2); }
    }

    //skafare en cell av datan 
    public int GetCell(int x, int z, int y)
    {
        return data[x, z, y];
    }

    //skaffare en chunk som den ska rita ut 
    public VoxelData()
    {
        data = WorldData.GetData;
    }

    //kollar om grannen fins eller inte utan att crash systemt baserat på hål
    public int GetNaighbor(int x, int z, int y, Direction dir)
    {
        DataCoordinate checkOffset = offset[(int)dir];
        DataCoordinate neighborCoord = new DataCoordinate(x + checkOffset.x, y + checkOffset.y, z + checkOffset.z);

        if (neighborCoord.x < 0 || neighborCoord.x >= Width || neighborCoord.y < 0 || neighborCoord.y >= Hight || neighborCoord.z < 0 || neighborCoord.z >= Depth)
        {
            return 0;
        }
        else
        {
            return GetCell(neighborCoord.x, neighborCoord.z, neighborCoord.y);
        }
    }

    //sparar offsets som ska kollas 
    struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;

        public DataCoordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    DataCoordinate[] offset =
    {
        new DataCoordinate(0,0,1),
        new DataCoordinate(1,0,0),
        new DataCoordinate(0,0,-1),
        new DataCoordinate(-1,0,0),
        new DataCoordinate(0,1,0),
        new DataCoordinate(0,-1,0),
    };
}

//sparar hål för att lättare kunna arbeta med datan 
public enum Direction
{
    North,
    East,
    South,
    West,
    Up,
    down,
}
