using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // вытаскивает игрока из объекта
        for (int i = transform.childCount - 1; i >= 0; i--) {
            transform.GetChild(i).parent = null;
        }
        // уничтожить объект
        Destroy(gameObject);
    }
    
}
