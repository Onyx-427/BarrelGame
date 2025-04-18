using UnityEngine;

public class GroundMove : MonoBehaviour
{

    public float floorSpeed = 5.0f;
    public float resetBoundZ;
    public float startZ = -40f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * floorSpeed * Time.deltaTime);

        if (transform.position.z < resetBoundZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
        }
    }
}
