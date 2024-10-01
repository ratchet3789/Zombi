using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[Header("Inventory")]
	[SerializeField] private int MaxInventorySlots = 4 * 4;
	
	// I've set this up this way becuase we will always know the byte array of the object. Its a dynamic list that we can quickly query
	// The package is as simple as {GUID, {Item Type, Item Count}}
	[SerializeField] private Dictionary<InventoryObject, int> InventoryData = new Dictionary<InventoryObject, int>();

	#region INVENTORY
	
	// Note: We don't give a shit about the GameObject on Pickup/Drop. We can recreate it at runtime via instantiation.
	void Pickup(InventoryObject NewObject)
	{
		int ID = 0;
		
		// Increment our held count
		if (InventoryData.TryGetValue(NewObject, out ID))
		{
			InventoryData[NewObject] += 1;
		}
		else
		{
			InventoryData.Add(NewObject, 1);
		}
	}

	void Drop(InventoryObject NewObject)
	{
		int ID = 0;
		
		// Increment our held count
		if (InventoryData.TryGetValue(NewObject, out ID))
		{
			if (InventoryData[NewObject] - 1 <= 0)
			{
				InventoryData.Remove(NewObject);
				return;
			}
			InventoryData[NewObject] -= 1;
			return;
		}
		else
		{
			Debug.Log("CHEATER!!!");
		}
	}

	#endregion
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}
}