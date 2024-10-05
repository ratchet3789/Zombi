// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DataAsset.h"
#include "InventoryItem.generated.h"

/**
 * 
 */
UCLASS()
class ZOMBI_API UInventoryItem : public UDataAsset
{
	GENERATED_BODY()

private:
	uint8_t UID{0};
	FTexture* InventoryIcon{};
	// This is where the problem lies. We need a system that allows us to swap between a StaticMesh and SkeletalMesh
	UStaticMesh* StaticMesh;

public:
	FORCEINLINE uint8_t GetUID() { return UID;}
	FORCEINLINE void SetUID(uint8_t NewUID) { UID = NewUID;}
};
