// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "InventoryItem.h"
#include "Components/ActorComponent.h"
#include "InventoryComponent.generated.h"


UCLASS(ClassGroup=(Custom), meta=(BlueprintSpawnableComponent))
class ZOMBI_API UInventoryComponent : public UActorComponent
{
	GENERATED_BODY()

public:
	// Sets default values for this component's properties
	UInventoryComponent();

protected:
	// Called when the game starts
	virtual void BeginPlay() override;

public:
	// Called every frame
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

public:
	UPROPERTY(BlueprintReadOnly, EditInstanceOnly)
	int MaxInventorySlots{4*4};

	TMap<UInventoryItem*, int> InventoryItems{};

	void Pickup(UInventoryItem* NewInventoryItem);
	void Drop(UInventoryItem* NewPhysicalIte);

	UFUNCTION(BlueprintImplementableEvent, DisplayName="Pickup")
	void BP_Pickup(UInventoryItem* NewInventoryItem);
	UFUNCTION(BlueprintImplementableEvent, DisplayName="Dropped")
	void BP_Dropped(UInventoryItem* NewPhysicalItem);
};
