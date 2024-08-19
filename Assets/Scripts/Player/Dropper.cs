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
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private List<Tile> inventory;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerEvents.onInventory += Inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.Count == 0)
        {
            return;
        }
        //Cursor.SetCursor(sprite[selected].texture, Vector2.one * 5, CursorMode.Auto);
        // Set SpriteRenderer to show the selected sprite
        target.sprite = inventory[selected].sprite;
        // Set spriterenderer position to mouse position
    }

    private void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.transform.position = mousePos;
    }

    private void Inventory(List<Tile> inventory)
    {
        this.inventory = inventory;
    }

    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.performed && inventory.Count != 0)
        {
            int scrollValue = context.ReadValue<float>() >= 0 ? 1 : -1;
            selected += scrollValue;
            selected = (selected + inventory.Count) % inventory.Count;
        }
    }
    public void Select1(InputAction.CallbackContext context)
    {
        if (context.performed && inventory.Count > 0)
        {
            selected = 0;
        }
    }
    public void Select2(InputAction.CallbackContext context)
    {
        if (context.performed && inventory.Count > 1)
        {
            selected = 1;
        }
    }
    public void Select3(InputAction.CallbackContext context)
    {
        if (context.performed && inventory.Count > 2)
        {
            selected = 2;
        }
    }

    public void Drop(InputAction.CallbackContext context)
    {
        if (context.performed && inventory.Count != 0)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            GameObject reference = Instantiate(prefab[inventory[selected].id], mousePos, Quaternion.identity);
            ps.Play();

            Tile _tile = inventory[selected];
            _tile.amount--;
            if (inventory[selected].amount <= 1)
            {
                inventory.RemoveAt(selected);
            }
            else
            {
                inventory[selected] = _tile;
            }
            if (inventory.Count != 0)
            {
                selected = (selected + inventory.Count) % inventory.Count;
            }
            else
            {
                target.sprite = null;
            }
            PlayerEvents.TilePlaced(reference);
        }
    }

    public void Remove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D target = Physics2D.OverlapCircle(mousePos, 0.5f, layerMask);
            if(target)
            {
                Destroy(target.gameObject);
            }
        }
    }
}
