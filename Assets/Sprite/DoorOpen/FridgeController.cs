using UnityEngine;

public class FridgeController : MonoBehaviour
{
    [SerializeField] private Animator _aimFridge;
    private bool isOpend = false;

    public void ToggleFridge()
    {
        isOpend = !isOpend;
        if (isOpend)
        {
            _aimFridge.SetTrigger("Open");
        }
    }
}
