using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        Debug.Log($"{other.gameObject.name}");
    }
}
