using UnityEngine;

public class TapToStart : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
