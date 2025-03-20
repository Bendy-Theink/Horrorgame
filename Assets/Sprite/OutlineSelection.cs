using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform hightLight; // Đối tượng đang được highlight
    private Transform aim; // Đối tượng đang được nhắm
    private RaycastHit hit;

    public LayerMask pickableLayer; // Lớp chỉ định vật thể có thể nhặt
    public GameObject _item; // Đồ trên tay người chơi
    [SerializeField] private TextMeshProUGUI _textContact; //hien text 

    void Update()
    {
        // Xóa hiệu ứng highlight khi không nhắm vào vật thể nào
        if (hightLight != null)
        {
            hightLight.gameObject.GetComponent<Outline>().enabled = false;
            hightLight = null;
            _textContact.gameObject.SetActive(false); // Ẩn text khi không có highlight
        }

        // Tạo tia ray từ tâm màn hình
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 10f, pickableLayer))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);
            hightLight = hit.transform;

            // Nếu đối tượng có tag "Lighted" và không phải là vật thể đang nhắm
            if (hightLight.CompareTag("Lighted") && hightLight != aim)
            {
                Outline outline = hightLight.GetComponent<Outline>();

                // Nếu có component Outline, bật nó lên
                if (outline != null)
                {
                    outline.enabled = true;
                    outline.OutlineColor = Color.magenta;
                    outline.OutlineWidth = 2.0f;

                    // Hiển thị text khi highlight vào vật thể
                    _textContact.gameObject.SetActive(true);
                    _textContact.text = "Nhấn E để nhặt";
                }

                // Nhấn E để nhặt đồ
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUpItem(hightLight.gameObject);
                }
            }
            else
            {
                hightLight = null;
            }
        }
    }

    // Xử lý nhặt đồ
    void PickUpItem(GameObject item)
    {
        // Tắt highlight trước khi nhặt
        Outline outline = item.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }

        // Xóa vật thể trên bàn
        Destroy(item);

        // Hiển thị vật thể trên tay
        if (_item != null)
        {
            _item.SetActive(true);
        }
    }
}
