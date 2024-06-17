using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class VisionCone : MonoBehaviour
{

    private Mesh mesh;
    private float visionRange;
    private double FOV;
    void Start()
    {
        EnemyController parent = GetComponentInParent<EnemyController>();
        if (parent == null)
        {
            Debug.Log("Vision Cone is not attached to enemy!");
        }
        this.visionRange = parent.visionRange + 1f;
        this.FOV = parent.FOV;

        MakeMesh();

    }


    //This code is adapted from the source of Draw2D.cs, which is a package that draws 2d shapes given arbitrary points. 
    //This code will create the vision cone for the enemy dynamically

    void MakeMesh()
    {
        mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        // vertices.Add(new Vector3(2, .1f, 2));
        // vertices.Add(new Vector3(0, .1f, 0));
        // vertices.Add(new Vector3(0, 2, 0));
        double x, z;
        x = visionRange * Math.Sin(-FOV);
        z = visionRange * Math.Cos(-FOV);
        vertices.Add(new Vector3((float)x, 0, (float)z));
        vertices.Add(new Vector3(0, .1f, 0));
        x = visionRange * Math.Sin(FOV);
        z = visionRange * Math.Cos(FOV);
        vertices.Add(new Vector3((float)x, 0, (float)z));


        Vector2[] vertices2D = new Vector2[vertices.Count];

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices2D[i] = vertices[i];
        }

        Triangulator tri = new Triangulator(vertices2D);
        int[] indices = tri.Triangulate();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        MeshFilter filter = GetComponent<MeshFilter>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        filter.sharedMesh = mesh;

        Vector2[] uv = new Vector2[vertices.Count];


        for (int i = 0; i < vertices.Count; i++)
        {
            uv[i] = new Vector2(vertices[i].x - mesh.bounds.min.x, vertices[i].y - mesh.bounds.min.y);
        }


        mesh.uv = uv;
    }
}