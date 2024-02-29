using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    public GameObject bodyPartPrefab;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> posHistory = new List<Vector3>();

    public float speed;
    public float steerSpeed;
    public int gap = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * speed * Time.deltaTime;

        transform.Rotate(transform.up * Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime);

        posHistory.Insert(0, transform.position);

        int index = 1;
        foreach(var part in bodyParts)
        {
            Vector3 newPos = posHistory[Mathf.Min(index * gap, posHistory.Count -1)];
            part.transform.position = newPos;
            index++;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            Grow();
            Destroy(collision.gameObject);
        }
    }

    private void Grow()
    {
        GameObject newPart = Instantiate(bodyPartPrefab);
        bodyParts.Add(newPart);
    }
}
