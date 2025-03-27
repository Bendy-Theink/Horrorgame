using KeySystem;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class RaycastManager : MonoBehaviour
{
    private Transform highLight; //doi tuong dang duoc highlight
    private Transform aim; //doi tuong dang nhin
    private RaycastHit hit;

    //public LayerMask layerMask;//lop chi dinh co the nhat
    public InventoryManager inventoryManager;// Them refence InventoryManager de quan ly
    public GameObject playerFlashlight; //den pin tren tay nguoi choi
    [SerializeField] private GameObject _note; //ghi chu

    public GameObject interactText; //Tham chieu den game object 3DInteracText
    public KeyInventory keyInventory; //Tham chieu den KeyInventory


    // Update is called once per frame
    void Update()
    {
        //Tat outline truoc do (Kiem tra outline co ton tai khong)
        if (highLight != null)
        {
            interactText.SetActive(false);
            Outline previousOutline = highLight.GetComponent<Outline>();
            if (previousOutline != null)
            {
                previousOutline.enabled = false;
            }

            highLight = null;
        }

        Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 3f))
        {
            highLight = hit.transform;

            //neu toi tuong co tag Lighted va khong phai la vat the dang nham
            if (highLight.CompareTag("Lighted") && highLight != aim)
            {
                Outline outline = highLight.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 5f;
                }
                //hien thi text
                interactText.SetActive(true);
                interactText.transform.position = highLight.position + Vector3.up * 0.05f; //dieu chinh offset
                interactText.transform.LookAt(Camera.main.transform);
                interactText.transform.rotation =
                                Quaternion.LookRotation(interactText.transform.position
                                -
                                Camera.main.transform.position);

                //Tuy chinh noi dung text
                TextMeshProUGUI textComponent = interactText.GetComponent<TextMeshProUGUI>();
                ItemType item = highLight.GetComponent<ItemType>();
                textComponent.color = Color.white;

                textComponent.text = item switch
                {
                    { itemType: ItemType.Type.Flashlight } => "Nhấn E để nhặt đèn pin",
                    {
                        itemType: ItemType.Type.Key1
                    } => "Nhấn E để nhặt chìa khóa",
                    {
                        itemType: ItemType.Type.Key2
                    } => "Nhấn E để nhặt chìa khóa",
                    {
                        itemType: ItemType.Type.Key3
                    } => "Nhấn E để nhặt chìa khóa",
                    {
                        itemType: ItemType.Type.HandleMusicBox
                    } => "Nhấn E để nhặt tay quay nhạc",
                    _ => "Nhấn E để nhặt"
                };
            }
            else if (highLight.CompareTag("Note"))
            {
                interactText.SetActive(true);
                interactText.transform.position = highLight.position + Vector3.up * 0.05f;
                interactText.transform.LookAt(Camera.main.transform);
                interactText.transform.rotation =
                    Quaternion.LookRotation(interactText.transform.position - Camera.main.transform.position);

                TextMeshProUGUI textComponent = interactText.GetComponent<TextMeshProUGUI>();
                textComponent.color = Color.white;
                textComponent.text = "E: Xem xét";
            }
            else
            {
                Debug.Log("Khong co outline");
            }
        }
        //nhat do khi nhan E
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(highLight != null)
            {
                if (highLight.CompareTag("Fridge"))
                {
                    FridgeController fridge = highLight.GetComponent<FridgeController>();
                    if(fridge != null)
                    {
                        fridge.ToggleFridge();
                    }
                }
            }
            if(highLight != null)
            {
                if (highLight.CompareTag("Safe"))
                {
                    SafeController safe = highLight.GetComponent<SafeController>();
                    if (safe != null)
                    {
                        safe.ToggleSafeUI();
                    }
                }
            }
            if (highLight != null)
            {
                if (highLight.CompareTag("Treasure"))
                {
                    PassWork passWork = highLight.GetComponent<PassWork>();
                    if (passWork != null)
                    {
                        passWork.ToggleSafeUI();
                    }
                }
            }
            HandleInteraction();
        }
        if (Input.GetKeyDown(KeyCode.R) && _note.activeSelf)
        {
            _note.SetActive(false);
            Time.timeScale = 1;
        }
    }
    private void HandleInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (!Physics.Raycast(ray, out var hitInfor, 4f))
        {
            return;
        }

        if(hitInfor.collider.CompareTag("Note"))
        {
            _note.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Khong co ghi chu");
        }
        if(hitInfor.collider.CompareTag("Box"))
        {
            var musicBox = hitInfor.collider.GetComponent<MusicBoxManager>();
            if (musicBox != null)
            {
                musicBox?.TryOpenMusicBox();
            }
        }
        else
        {
            Debug.Log("Khong co hop nhac de mo");
        }
        if (hitInfor.collider.CompareTag("Door1"))
        {
            var door = hitInfor.collider.GetComponent<DoorController>();
            if (door != null)
            {
                door?.TryOpenDoor();
            }
        }
        else
        {
            Debug.Log("Khong co cua de mo");
        }
        if (hitInfor.collider.CompareTag("Door2"))
        {
            var door = hitInfor.collider.GetComponent<DoorController>();
            if (door != null)
            {
                door?.TryOpenDoor2();
            }
        }
        if (hitInfor.collider.CompareTag("Door3"))
        {
            var door = hitInfor.collider.GetComponent<DoorController>();
            if (door != null)
            {
                door?.TryOpenDoor3();
            }
        }
        if (hitInfor.collider.CompareTag("Lighted"))
        {
            HandleItemPickup(hitInfor.collider.gameObject);
        }
        else
        {
            Debug.Log("Khong co gi de nhat");
        }
    }
    private void HandleItemPickup(GameObject obj)
    {
        var item = obj.GetComponent<ItemType>();
        if(item == null)
        {
            return;
        }
        
        switch (item.itemType)
        {
            case ItemType.Type.Flashlight:
                playerFlashlight?.SetActive(true);
                break;
            case ItemType.Type.HandleMusicBox:
                keyInventory.hasHanldeMusicBox = true;
                break;
            case ItemType.Type.Key1:
                keyInventory.hasRedKey = true;
                break;
            case ItemType.Type.Key2:
                keyInventory.hasGreenKey = true;
                break;
            case ItemType.Type.Key3:
                keyInventory.hasEndKey = true;
                break;
            case ItemType.Type.Other:
                inventoryManager?.AddItem(obj);
                break;
        }

        Destroy(obj);
    }
}
