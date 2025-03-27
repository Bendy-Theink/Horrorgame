using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(4);
        }
    }
    
    

}
