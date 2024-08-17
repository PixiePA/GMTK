using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private SpriteRenderer target;
    [SerializeField] int selected = 0;
    [SerializeField] private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.SetCursor(sprite[selected].texture, Vector2.one * 5, CursorMode.Auto);
        // Set SpriteRenderer to show the selected sprite
        target.sprite = sprite[selected];
        // Set spriterenderer position to mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.transform.position = mousePos;
    }

    //private void OnGUI()
    //{
    //    // Draw the selected sprite at the mouse position
    //    Vector2 mousePos = Event.current.mousePosition;
    //    GUI.DrawTexture(new Rect(mousePos.x -10, mousePos.y -10, 50, 50), sprite[selected].texture);
    //}

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
            ps.Play();
        }
    }
}
