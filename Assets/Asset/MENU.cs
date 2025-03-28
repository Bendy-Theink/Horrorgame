using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MENU : MonoBehaviour
{
    

    public void play()
    {
        SceneManager.LoadScene(2);
    }
    public void help()
    {
        SceneManager.LoadScene(1);
    }
    public void next()
    {
        SceneManager.LoadScene(3);
    }
    public void exit()
    {
        SceneManager.LoadScene(0);
    }
    public void nextchap2()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(5);
        }

    }
}
