using TMPro;
using UnityEngine;

public class PassForBox : MonoBehaviour
{
    [SerializeField] private GameObject safeUI;
    [SerializeField] TMP_InputField passworkInput;
    [SerializeField] private Animator safeAnimator;
    [SerializeField] private string correctPassword = "4215";

    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip AudioClip; //tieng mo
    [SerializeField] private AudioClip clip;//nut bam

    private bool isUIVisible = false;

    private void Start()
    {
        safeUI.SetActive(false);
    }
    public void ToggleSafeUI()
    {
        isUIVisible = !isUIVisible;
        safeUI.SetActive(isUIVisible);
        Time.timeScale = isUIVisible ? 0 : 1;
        Cursor.lockState = isUIVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddNumber(string number)
    {
        if (passworkInput.text.Length < 4)
        {
            passworkInput.text += number;
        }
    }

    public void CheckPassword()
    {
        if (passworkInput.text == correctPassword)
        {
            safeAnimator.SetTrigger("Open");
            Debug.Log("Mat khau dung");
            ToggleSafeUI();
        }
        else
        {
            Debug.Log("Mat khau sai");
            passworkInput.text = "";
        }
    }
    public void PlaySound()
    {
        AudioSource.clip = AudioClip;
        AudioSource.Play();
    }
    public void PlayAuidoClip()
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }
}
