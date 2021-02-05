using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private Vector3[] vertices;
    private int vertexID = 0;

    public float m_delay;
    public float minDistance = 2.0f;
    public GameObject character;

    public ProtagonistController protagonist;

    Mesh mesh;

    public float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {

        if (protagonist.mining)
        {
            timer += Time.deltaTime;

            if (timer > m_delay)
            {
                float distanceToObject = Vector3.Distance(character.transform.position, transform.position);

                if (distanceToObject < 20.0f)
                { 
                    for (int i = 0; i < vertices.Length; ++i)
                    {
                        float distance = Mathf.Sqrt(((character.transform.position.x - (vertices[i].x + transform.position.x)) * (character.transform.position.x - (vertices[i].x + transform.position.x))) +
                            ((character.transform.position.y - (vertices[i].y + transform.position.y)) * (character.transform.position.y - (vertices[i].y + transform.position.y))) +
                            ((character.transform.position.z - (vertices[i].z + transform.position.z)) * (character.transform.position.z - (vertices[i].z + transform.position.z))));

                        if (distance < minDistance)
                        {
                            Bacuum(i);
                        }

                    }
                }

            }
        }
        else
            timer = 0;

        mesh.vertices = vertices;
    }

    void Bacuum(int id)
    {
       
         vertices[id].y -= 1;
         timer = 0;
         vertexID++;
         if (vertexID > vertices.Length)
         {
             vertexID = 0;
         }
      
    }
}
