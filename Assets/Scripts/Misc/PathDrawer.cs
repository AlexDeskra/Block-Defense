using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public static GameObject LinePrefab;
    private GameObject PathParent;
    private float GridSize;

    private void Start()
    {
        PathParent = GameObject.Instantiate(new GameObject("PathParent"), new Vector3(0, 0, 0), Quaternion.identity);
        GridSize = GameObject.Find("Grid").GetComponent<Grid>().cellSize.x;
    }
    public void DrawPath(List<Vector3> Path)
    {
        foreach (Transform t in PathParent.transform)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < Path.Count - 1; ++i)
        {
            GameObject Line = Instantiate(LinePrefab, Path[i], Quaternion.identity);
            Line.transform.parent = PathParent.transform;
            Vector3 Dif = Path[i + 1] - Path[i];
            if (Dif.x > 0)
            {
                Line.transform.Rotate(new Vector3(0, 0, -90));
            }
            else if (Dif.x < 0)
            {
                Line.transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (Dif.y < 0)
            {
                Line.transform.position -= new Vector3(0, GridSize, 0);
            }
        }
    }
}
