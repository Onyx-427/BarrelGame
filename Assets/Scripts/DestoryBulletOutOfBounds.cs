using UnityEngine;

public class DestoryBulletOutOfBounds : MonoBehaviour
{
    public float maxRange;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > maxRange)
        {
            Destroy(gameObject);
        }
    }
}
