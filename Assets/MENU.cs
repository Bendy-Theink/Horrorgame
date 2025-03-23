using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MENU : MonoBehaviour
{
    

    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void help()
    {
        SceneManager.LoadScene(2);
    }
}
