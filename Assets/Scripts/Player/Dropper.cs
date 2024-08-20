using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Sprite NuhUh;
    [SerializeField] private SpriteRenderer target;
    [SerializeField] int selected = 0;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private List<Tile> inventory;
    [SerializeField] private AudioSource DropSFX;
    [SerializeField] private AudioSource BreakSFX;

    private bool isBlocked = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerEvents.onInventory += Inventory;
    }

    private void OnDisable()
    {
        PlayerEvents.onInventory -= Inventory;
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
        if (isBlocked)
        {
            target.sprite = NuhUh;
        }
        else
        {
            target.sprite = inventory[selected].sprite;
        }
        // Set spriterenderer position to mouse position
    }

    private void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.transform.position = mousePos;

        isBlocked = Physics2D.OverlapCircle(mousePos, 0.5f, playerMask);
        //{
        //    Transform other = Physics2D.OverlapCircle(mousePos, 0.5f, playerMask).transform;
        //    int loopbreaker5000 = 0;
        //    while(Physics2D.OverlapCircle(mousePos, 0.5f, playerMask) || loopbreaker5000 > 100)
        //    {
        //        mousePos = Vector2.MoveTowards(mousePos, other.position, -0.1f);
        //        loopbreaker5000++;
        //    }
        //    target.transform.position = mousePos;
        //}
        //else
        //{
        //    target.transform.position = mousePos;
        //}
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
        if(isBlocked)
        {
            return;
        }
        if (context.performed && inventory.Count != 0)
        {
            GameObject reference = Instantiate(prefab[inventory[selected].id], target.transform.position, Quaternion.identity);

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
            if(DropSFX) DropSFX.Play();
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
                target.transform.GetChild(0).gameObject.SetActive(false);
                target.GetComponent<Collider2D>().enabled = false;
                if(BreakSFX) BreakSFX.Play();
                ps.Play();
            }
        }
    }
}
