using UnityEngine;
using System.Collections.Generic;

/**
 * Author: runevision 
 * From:
 * http://wiki.unity3d.com/index.php?title=Triangulator
 */



public class Triangulator
{
    private List<Vector2> m_points = new List<Vector2>();

    public Triangulator(Vector2[] points)
    {
        m_points = new List<Vector2>(points);
    }

    public int[] Triangulate()
    {
        List<int> indices = new List<int>();

        int numPoints = m_points.Count;
        if (numPoints < 3)
            return indices.ToArray();

        int[] triVertNdices = new int[numPoints];
        if (Area() > 0)
        {
            for (int vertexNdx = 0; vertexNdx < numPoints; vertexNdx++)
                triVertNdices[vertexNdx] = vertexNdx;
        }
        else
        {
            for (int vertexNdx = 0; vertexNdx < numPoints; vertexNdx++)
                triVertNdices[vertexNdx] = (numPoints - 1) - vertexNdx;
        }

        int pointCount = numPoints;
        int count = 2 * pointCount;
        for (int m = 0, ndxB = pointCount - 1; pointCount > 2;)
        {
            if ((count--) <= 0)
                return indices.ToArray();

            int ndxA = ndxB;
            if (pointCount <= ndxA)
                ndxA = 0;
            ndxB = ndxA + 1;
            if (pointCount <= ndxB)
                ndxB = 0;
            int ndxC = ndxB + 1;
            if (pointCount <= ndxC)
                ndxC = 0;

            if (Snip(ndxA, ndxB, ndxC, pointCount, triVertNdices))
            {
                //Delete an extra vertex
                int triVertA, triVertB, triVertC, curr, next;
                triVertA = triVertNdices[ndxA];
                triVertB = triVertNdices[ndxB];
                triVertC = triVertNdices[ndxC];
                indices.Add(triVertA);
                indices.Add(triVertB);
                indices.Add(triVertC);
                m++;
                for (curr = ndxB, next = ndxB + 1; next < pointCount; curr++, next++)
                    triVertNdices[curr] = triVertNdices[next];
                pointCount--;
                count = 2 * pointCount;
            }
        }

        indices.Reverse();
        return indices.ToArray();
    }

    private float Area()
    {
        int numPoints = m_points.Count;
        float A = 0.0f;
        for (int endNdx = numPoints - 1, startNdx = 0; startNdx < numPoints; endNdx = startNdx++)
        {
            Vector2 pval = m_points[endNdx];
            Vector2 qval = m_points[startNdx];
            A += pval.x * qval.y - qval.x * pval.y;
        }
        return (A * 0.5f);
    }

    private bool Snip(int ndxA, int ndxB, int ndxC, int numPoints, int[] vertices)
    {
        int curr;
        Vector2 triVertA = m_points[vertices[ndxA]];
        Vector2 triVertB = m_points[vertices[ndxB]];
        Vector2 triVertC = m_points[vertices[ndxC]];
        if (Mathf.Epsilon > (((triVertB.x - triVertA.x) * (triVertC.y - triVertA.y)) - ((triVertB.y - triVertA.y) * (triVertC.x - triVertA.x))))
            return false;
        for (curr = 0; curr < numPoints; curr++)
        {
            if ((curr == ndxA) || (curr == ndxB) || (curr == ndxC))
                continue;
            Vector2 P = m_points[vertices[curr]];
            if (InsideTriangle(triVertA, triVertB, triVertC, P))
                return false;
        }
        return true;
    }

    private bool InsideTriangle(Vector2 triVertA, Vector2 triVertB, Vector2 triVertC, Vector2 pointToCheck)
    {
        float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
        float cCROSSap, bCROSScp, aCROSSbp;

        ax = triVertC.x - triVertB.x; ay = triVertC.y - triVertB.y;
        bx = triVertA.x - triVertC.x; by = triVertA.y - triVertC.y;
        cx = triVertB.x - triVertA.x; cy = triVertB.y - triVertA.y;
        apx = pointToCheck.x - triVertA.x; apy = pointToCheck.y - triVertA.y;
        bpx = pointToCheck.x - triVertB.x; bpy = pointToCheck.y - triVertB.y;
        cpx = pointToCheck.x - triVertC.x; cpy = pointToCheck.y - triVertC.y;

        aCROSSbp = ax * bpy - ay * bpx;
        cCROSSap = cx * apy - cy * apx;
        bCROSScp = bx * cpy - by * cpx;

        return ((aCROSSbp >= 0.0f) && (bCROSScp >= 0.0f) && (cCROSSap >= 0.0f));
    }
}
