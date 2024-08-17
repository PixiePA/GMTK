using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] int selected = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(sprite[selected].texture, Vector2.zero, CursorMode.Auto);
    }

    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int scrollValue = context.ReadValue<float>() >= 0 ? 1 : -1;
            selected += scrollValue;
            selected = (selected + prefab.Length) % prefab.Length;
        }
    }
    public void Select1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selected = 0;
        }
    }
    public void Select2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selected = 1;
        }
    }
    public void Select3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selected = 2;
        }
    }

    public void Drop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Instantiate(prefab[selected], mousePos, Quaternion.identity);
        }
    }
}
