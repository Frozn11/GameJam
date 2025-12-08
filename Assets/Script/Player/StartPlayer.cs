using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            transform.GetChild(i).parent = null;
        }
        Destroy(gameObject);
    }
    
}
