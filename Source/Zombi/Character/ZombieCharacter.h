// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "InputMappingContext.h"
#include "InventoryComponent.h"
#include "Camera/CameraComponent.h"
#include "GameFramework/Actor.h"
#include "GameFramework/Character.h"
#include "ZombieCharacter.generated.h"

UCLASS()
class ZOMBI_API AZombieCharacter : public ACharacter
{
	GENERATED_BODY()

public:
	// Sets default values for this actor's properties
	AZombieCharacter();

	// Components
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly)
	UCameraComponent* FPSCamera;

	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly)
	USkeletalMeshComponent* FPSArms;

	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly)
	UInventoryComponent* PlayerInventory;

public:
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs")
	UInputMappingContext* MappingContext;

	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs | Actions")
	UInputAction* IA_Look;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs | Actions")
	UInputAction* IA_Move;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs | Actions")
	UInputAction* IA_Jump;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs | Actions")
	UInputAction* IA_Crouch;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs | Actions")
	UInputAction* IA_Aim;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category="Inputs | Actions")
	UInputAction* IA_Fire;

public:
	void BeginPlay() override;
	void Tick(float DeltaTime) override;
	void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent);
	void GetLifetimeReplicatedProps(TArray<class FLifetimeProperty>& OutLifetimeProps) const override;
	void Restart();
	void FellOutOfWorld(const class UDamageType& dmgType) override;

public:
	void InputLook(const FInputActionValue& Value);
	void InputMove(const FInputActionValue& Value);
	void InputJumpStart(const FInputActionValue& Value);
	void InputJumpEnd(const FInputActionValue& Value);
};
