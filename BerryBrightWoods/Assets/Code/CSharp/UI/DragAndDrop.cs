using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] Image _image = null;

    Vector2 _mousePos = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        _image.transform.position = new Vector3(_mousePos.x, _mousePos.y, 1);
    }


    Building b = null;
    void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(_mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.CompareTag("Building"))
                return;
            //bloat zone
            Debug.Log("building");
            b = hit.collider.gameObject.GetComponent<Building>();
            b.OnMouseEnter.Invoke();
        }
        else if(b)
            b.OnMouseExit.Invoke();
    }

    public void SetImage(Image image)
    {
        if (image == null)
        {
            _image.enabled = false;
            return;
        }
        _image.sprite = image.sprite;
        _image.color = image.color;
        _image.enabled = true;
    }

    //hook up to player in editor
    public void MousePosition(CallbackContext ctx)
    {
        _mousePos = ctx.ReadValue<Vector2>();
    }

    public void OnDrag(GameObject data)
    {
        Debug.Log(data.name);
        var img = data.GetComponent<Image>();
        if (img)
            SetImage(img);
    }

    public void OnMouseUp(CallbackContext ctx)
    {
        if (!ctx.canceled || !_image.enabled)
            return;
        SetImage(null);
        //replace with virtual method "add worker"
        b?.OnAddWorker.Invoke();
    }
}
