using UnityEngine;

public class FridgeController : MonoBehaviour
{
    [SerializeField] private Animator _aimFridge;
    private bool isOpend = false;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    public void ToggleFridge()
    {
        isOpend = !isOpend;
        if (isOpend)
        {
            _aimFridge.SetTrigger("Open");
        }
    }
    public void PlaySound()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
}
