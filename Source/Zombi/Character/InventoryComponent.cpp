// Fill out your copyright notice in the Description page of Project Settings.


#include "InventoryComponent.h"


// Sets default values for this component's properties
UInventoryComponent::UInventoryComponent()
{
	// Set this component to be initialized when the game starts, and to be ticked every frame.  You can turn these features
	// off to improve performance if you don't need them.
	PrimaryComponentTick.bCanEverTick = true;

	// ...
}


// Called when the game starts
void UInventoryComponent::BeginPlay()
{
	Super::BeginPlay();

	// ...
	
}


// Called every frame
void UInventoryComponent::TickComponent(float DeltaTime, ELevelTick TickType,
                                        FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);

	// ...
}

void UInventoryComponent::Pickup(UInventoryItem* NewInventoryItem)
{
	// Check we don't already have the inv item
	for(const TTuple<UInventoryItem*, int>& Item : InventoryItems)
	{
		if(Item.Key->GetUID() == NewInventoryItem->GetUID())
		{
			// We found it!
			InventoryItems[Item.Key]++;
			return;
		}
	}
	
	// Add a new inventory item
	InventoryItems.Add(NewInventoryItem, 1);
}

void UInventoryComponent::Drop(UInventoryItem* NewPhysicalItem)
{
	if(InventoryItems.Find(NewPhysicalItem))
	{
		if(InventoryItems[NewPhysicalItem] - 1 <= 0)
		{
			InventoryItems.Remove(NewPhysicalItem);
			return;
		}
		InventoryItems[NewPhysicalItem]--;
	}
}