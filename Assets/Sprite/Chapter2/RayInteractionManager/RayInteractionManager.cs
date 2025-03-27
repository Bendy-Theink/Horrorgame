using KeySystem;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayInteractionManager : MonoBehaviour
{
    private Transform highLight;
    private RaycastHit hit;
    private Transform aim;

    public KeyInventory keyInventory;
    [SerializeField] private GameObject _note;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (highLight != null)
        {
            highLight = null;
        }
        Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 3f))
        {
            highLight = hit.transform;
            if (highLight.CompareTag("Lighted") && highLight != aim)
            {
                Outline outline = highLight.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 5f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (highLight != null)
            {
                if (highLight.CompareTag("BoxSecret"))
                {
                    PassForBox pass = highLight.GetComponent<PassForBox>();
                    if (pass != null)
                    {
                        pass.ToggleSafeUI();
                    }
                }
            }
            InteractionRay();
        }
        if (Input.GetKeyDown(KeyCode.R) && _note.activeSelf)
        {
            _note.SetActive(false);
            Time.timeScale = 1;
        }
    }
    private void InteractionRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (!Physics.Raycast(ray, out var hitt, 4f))
        {
            return;
        }
        if (hitt.collider.CompareTag("Note"))
        {
            _note.SetActive(true);
            Time.timeScale = 0;
        }
        if (hitt.collider.CompareTag("Box2"))
        {
            var door = hitt.collider.GetComponent<DoorController>();
            if (door != null)
            {
                door?.TryOpenTreasure();
            }
        }
    }
    private void HandleItemPickup(GameObject obj)
    {
        var item = obj.GetComponent<ItemType>();
        if (item == null)
        {
            return;
        }
        switch (item.itemType)
        {
            case ItemType.Type.Key4:
                keyInventory.hasSecretKey = true;
                break;
        }
    }
}
