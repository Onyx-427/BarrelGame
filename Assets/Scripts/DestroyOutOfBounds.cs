using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float maxRange;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < maxRange)
        {
            Destroy(gameObject);
        }
    }
}
