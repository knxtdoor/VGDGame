using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class VisionCone : MonoBehaviour
{

    private Mesh mesh;
    private float visionRange;
    private float FOV;
    public List<Vector3> vertices;
    private Transform parentTransform;
    void Start()
    {
        EnemyController parent = GetComponentInParent<EnemyController>();
        if (parent == null)
        {
            Debug.Log("Vision Cone is not attached to enemy!");
        }
        parentTransform = GetComponentInParent<Transform>();
        this.visionRange = parent.visionRange + 1f;
        this.FOV = parent.FOV;
        MakeMesh();


    }
    void Update()
    {
        MakeMesh();
    }


    //This code is adapted from the source of Draw2D.cs, which is a package that draws 2d shapes given arbitrary points. 
    //This code will create the vision cone for the enemy dynamically

    void MakeMesh()
    {

        mesh = new Mesh();

        vertices = GenerateVertexList();

        List<int> triIndices = new List<int>();
        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triIndices.Add(i);
            triIndices.Add(0);
            triIndices.Add(i + 1);
        }
        triIndices.Reverse();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triIndices.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.isStatic = false;
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


    List<Vector3> GenerateVertexList()
    {
        List<Vector3> vertexList = new List<Vector3>();
        vertexList.Add(Vector3.zero);
        float radianIncrement = Mathf.PI / 16;
        float facingDirection = Mathf.Atan2(parentTransform.forward.x, parentTransform.forward.z);
        // float facingDirection = 0;
        float FOVrad = Mathf.Deg2Rad * FOV;
        for (float i = -(FOVrad / 2); i < (FOVrad / 2); i += radianIncrement)
        {
            Vector3 currDirection = new Vector3(Mathf.Sin(facingDirection + i), 0, Mathf.Cos(facingDirection + i)).normalized;

            Vector3 localCurrDirection = new Vector3(Mathf.Sin(i), 0, Mathf.Cos(i)).normalized;

            float vertexDist = 0;
            RaycastHit rayHit;
            // Debug.DrawLine(transform.position, transform.position + (currDirection * visionRange));
            if (Physics.Raycast(transform.position, currDirection, out rayHit, visionRange))
            {
                vertexDist = rayHit.distance;
            }
            else
            {
                vertexDist = visionRange;
            }
            Vector3 impactPoint = (localCurrDirection * vertexDist);

            vertexList.Add(impactPoint);

        }
        return vertexList;
    }
}