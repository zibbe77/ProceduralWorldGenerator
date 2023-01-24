using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class VoxelRender : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    //cube instälningar
    public float scale = 1f;
    float adjScale;

    //fixar basic saker
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f;
    }

    //kontrol metod (fördelar upgifter till olika metoder)
    void Start()
    {
        GenerateVoxelMesh(new VoxelData());
        UppdateMesh();
    }

    //generatat en voxel mesh
    void GenerateVoxelMesh(VoxelData data)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int x = 0; x < data.Width; x++)
        {
            for (int z = 0; z < data.Depth; z++)
            {
                for (int y = 0; y < data.Hight; y++)
                {
                    if (data.GetCell(x, z, y) == 0)
                    {
                        continue;
                    }
                    MakeCube(adjScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), x, z, y, data);
                }
            }
        }
    }

    //skappar cube
    void MakeCube(float cubeScale, Vector3 cubePos, int x, int z, int y, VoxelData data)
    {
        //loppar igenom allas sidor
        for (int i = 0; i < 6; i++)
        {
            if (data.GetNaighbor(x, z, y, (Direction)i) == 0)
            {
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }
    }

    //skapar en rikning av cuben 
    void MakeFace(Direction dir, float facescale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.FaceVertices(dir, facescale, facePos));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    //uptaterar meshen och städare den om det skulle finas skräp kvar. 
    void UppdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
