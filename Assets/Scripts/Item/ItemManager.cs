using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<ConsumableItemData> consumableItemList;
    public List<EquipmentItemData> equipmentItemList;
}
