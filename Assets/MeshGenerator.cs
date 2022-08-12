using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    public float width = 1;
    public float height = 1;
    Mesh mesh;
    public void Start()
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        mesh = new Mesh();

        createQuad(new Vector3(10, 0, 0), new Vector2(1, 1));


        meshFilter.mesh = mesh;
    }
    public void createQuad(Vector3 position, Vector2 size)
    {
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(position.x + 0, position.y + 0, position.z + 0),
            new Vector3(position.x + size.x, position.y + 0, position.z + 0),
            new Vector3(position.x + 0, position.y + 0, position.z + size.y),
            new Vector3(position.x + size.x, position.y + 0, position.z + size.y)
        };
        addQuad(vertices);
    }
    public void addQuad(Vector3[] verticesIN)
    {
        int[] tris = new int[mesh.triangles.Length + 6];
        tris[tris.Length - 6] = 0;
        tris[tris.Length - 5] = 2;
        tris[tris.Length - 4] = 1;
        tris[tris.Length - 3] = 2;
        tris[tris.Length - 2] = 3;
        tris[tris.Length - 1] = 1;



        Vector3[] normals = new Vector3[mesh.normals.Length + 4];
        normals[normals.Length - 4] = -Vector3.forward;
        normals[normals.Length - 3] = -Vector3.forward;
        normals[normals.Length - 2] = -Vector3.forward;
        normals[normals.Length - 1] = -Vector3.forward;


        Vector2[] uv = new Vector2[mesh.uv.Length + 4];
        uv[uv.Length - 4] = new Vector2(0, 0);
        uv[uv.Length - 3] = new Vector2(1, 0);
        uv[uv.Length - 2] = new Vector2(0, 1);
        uv[uv.Length - 1] = new Vector2(1, 1);

        Vector3[] vertices = new Vector3[mesh.vertices.Length + verticesIN.Length];
        vertices[vertices.Length - 4] = new Vector3(0, 0, 0);
        vertices[vertices.Length - 3] = new Vector3(width, 0, 0);
        vertices[vertices.Length - 2] = new Vector3(0, 0, height);
        vertices[vertices.Length - 1] =  new Vector3(width, 0, height);

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.normals = normals;

        mesh.uv = uv;

    }
}
