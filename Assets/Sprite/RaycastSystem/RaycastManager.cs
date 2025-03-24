using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    private Transform highLight; //doi tuong dang duoc highlight
    private Transform aim; //doi tuong dang nhin
    private RaycastHit hit;

    //public LayerMask layerMask;//lop chi dinh co the nhat
    public InventoryManager inventoryManager;// Them refence InventoryManager de quan ly
    public GameObject playerFlashlight; //den pin tren tay nguoi choi

    public GameObject interactText; //Tham chieu den game object 3DInteracText

    // Update is called once per frame
    void Update()
    {
        //Tat outline truoc do (Kiem tra outline co ton tai khong)
        if(highLight != null)
        {
            interactText.SetActive(false);
            Outline previousOutline = highLight.GetComponent<Outline>();
            if(previousOutline != null) 
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
            if(highLight.CompareTag("Lighted") && highLight != aim)
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
                                Quaternion.LookRotation(interactText.transform.position - Camera.main.transform.position);

                //Tuy chinh noi dung text
                TextMeshProUGUI textComponent = interactText.GetComponent<TextMeshProUGUI>();
                ItemType item = highLight.GetComponent<ItemType>();
                if(item != null && item.itemType == ItemType.Type.Flashlight)
                {
                    textComponent.text = "Nhấn E để nhặt đèn pin";
                }
                else
                {
                    textComponent.text = "Nhấn E để nhặt";
                }
            }
            else
            {
                Debug.Log("Khong co outline");
            }
        }
        //nhat do lkhi nhan E
        if(Input.GetKeyDown(KeyCode.E))
        {
            //kiem tra vat co the nhat
            if (highLight != null && highLight.CompareTag("Lighted") && highLight != aim)
            {
                //kiem tra xem vat pham co phai den pin khong
                ItemType item = highLight.GetComponent<ItemType>();
                if (item != null && item.itemType == ItemType.Type.Flashlight)
                {
                    if (playerFlashlight != null)
                    {
                        playerFlashlight.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Khong co den pin");
                    }
                }
                else
                {
                    //them vat vao inventory
                    if (inventoryManager != null)
                    {
                        inventoryManager.AddItem(highLight.gameObject);
                    }
                    else
                    {
                        Debug.Log("Khong co InventoryManager");
                    }
                }

                Destroy(highLight.gameObject);
                highLight = null;
            }
            else
            {
                Debug.Log("Khong co gi de nhat");
            }
        }
    }
}
