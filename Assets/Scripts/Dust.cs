using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private Vector3[] vertices;
    private int vertexID = 0;

    public float delay = 0.20f;
    public float minDistance = 2.0f;
    public GameObject character;

    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
       
       for(int i = 0; i < vertices.Length; ++i)
        {
            float distance = Mathf.Sqrt(((character.transform.position.x - (vertices[i].x + transform.position.x)) * (character.transform.position.x - (vertices[i].x + transform.position.x))) + 
                ((character.transform.position.y - (vertices[i].y + transform.position.y)) * (character.transform.position.y - (vertices[i].y + transform.position.y))) + 
                ((character.transform.position.z - (vertices[i].z + transform.position.z)) * (character.transform.position.z - (vertices[i].z + transform.position.z))));

            if(distance  < minDistance )
            {
                Bacuum(i, Time.deltaTime);
            }
        }

        mesh.vertices = vertices;
    }

    void Bacuum(int id, float time)
    {
        if (time < delay)
        {
            vertices[id].y -= 1;
            time = 0;
            vertexID++;
            if (vertexID > vertices.Length)
            {
                vertexID = 0;
            }
        }
    }
}
