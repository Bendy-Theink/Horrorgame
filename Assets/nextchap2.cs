using UnityEngine;

public class nextchap2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        }
    }
}
