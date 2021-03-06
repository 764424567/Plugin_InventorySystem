﻿#if UFPS__

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.InventorySystem;
using Devdog.InventorySystem.Models;

namespace Devdog.InventorySystem.Integration.UFPS
{
    public partial class ConsumableUFPSInventoryItem : InventoryItemBase
    {
        public bool useUFPSItemData = true;
        public vp_Pickup itemPickup;
        
        protected vp_PlayerEventHandler ufpsEventHandler
        {
            get
            {
                return InventoryPlayerManager.instance.currentPlayer.GetComponent<vp_PlayerEventHandler>();
            }
        }
        protected vp_PlayerInventory ufpsInventory
        {
            get
            {
                return InventoryPlayerManager.instance.currentPlayer.GetComponent<vp_PlayerInventory>();
            }
        }

        public override string name
        {
            get
            {
                if (useUFPSItemData && itemPickup != null)
                    return itemPickup.InventoryName;
                else
                    return base.name;
            }
            set { base.name = value; }
        }

        public override string description
        {
            get
            {
                if (useUFPSItemData && itemPickup != null)
                    return string.Empty;
                else
                    return base.description;
            }
            set { base.description = value; }
        }

        public override LinkedList<InventoryItemInfoRow[]> GetInfo()
        {
            var basic = base.GetInfo();
            basic.Remove(basic.First.Next);
            //basic.AddAfter(basic.First, new InfoBoxUI.Row[]
            //{
            //    new InfoBoxUI.Row("Ammo", "abc")
            //});

            return basic;
        }

        public override int Use()
        {
            int used = base.Use();
            if (used < 0)
            {
                return used;
            }

            itemPickup.TryGiveTo

            //ufpsEventHandler
            bool added = ufpsEventHandler.AddItem.Try(new object[] { itemPickup.InventoryName, 1 });
            if (added)
            {
                currentStackSize--;
                NotifyItemUsed(1, true);
                return 1;
            }

            return 0; // 0 we're used, UFPS rejected the item
        }
    }
}

#endif