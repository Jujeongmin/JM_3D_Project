using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;
    int selectedItemIndex = 0;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }

    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    private void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;
        if(data == null)
        {
            Debug.Log("data is null");
        }

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if(slot != null)
            {
                slot.quantity ++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        CharacterManager.Instance.Player.itemData = null;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        } 
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        } 
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        for(int i = 0; i < selectedItem.consumables.Length; ++i)
        {
            selectedStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
    }

    public void OnUseButton()
    {
        if(selectedItem.type == ItemType.Consumable)
        {
            for(int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch(selectedItem.consumables[i].type)
                {
                    case ConsumableType.Hp:
                        condition.Heal(selectedItem.consumables[i].value);
                        break;                        
                }
            }
            RemoveSelectedItem();
        }
    }

    private void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }
}
