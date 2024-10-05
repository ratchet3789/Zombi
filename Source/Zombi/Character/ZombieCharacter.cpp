// Fill out your copyright notice in the Description page of Project Settings.


#include "ZombieCharacter.h"

#include "EnhancedInputComponent.h"
#include "EnhancedInputSubsystems.h"
#include "GameFramework/Character.h"


// Sets default values
AZombieCharacter::AZombieCharacter()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	FPSCamera = CreateDefaultSubobject<UCameraComponent>(TEXT("FPS Camera"));
	FPSCamera->bUsePawnControlRotation = true;
	FPSCamera->SetupAttachment(RootComponent);

	FPSArms = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("FPS Arms"));
	FPSArms->SetupAttachment(FPSCamera);
	FPSArms->SetRelativeLocation(FVector(0.0f, 0.0f, 72.0f));

	PlayerInventory = CreateDefaultSubobject<UInventoryComponent>(TEXT("Inventory"));
}

// Called when the game starts or when spawned
void AZombieCharacter::BeginPlay()
{
	Super::BeginPlay();
}

void AZombieCharacter::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}

void AZombieCharacter::SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	if (APlayerController* PC = Cast<APlayerController>(Controller))
	{
		if (UEnhancedInputLocalPlayerSubsystem* InputSubsystem = ULocalPlayer::GetSubsystem<
			UEnhancedInputLocalPlayerSubsystem>(PC->GetLocalPlayer()))
		{
			InputSubsystem->AddMappingContext(MappingContext, 0);
		}
	}

	if (UEnhancedInputComponent* EIC = CastChecked<UEnhancedInputComponent>(PlayerInputComponent))
	{
		EIC->BindAction(IA_Look, ETriggerEvent::Triggered, this, &AZombieCharacter::InputLook);

		EIC->BindAction(IA_Move, ETriggerEvent::Triggered, this, &AZombieCharacter::InputMove);

		EIC->BindAction(IA_Jump, ETriggerEvent::Triggered, this, &AZombieCharacter::InputJumpStart);

		EIC->BindAction(IA_Look, ETriggerEvent::Triggered, this, &AZombieCharacter::InputJumpEnd);
	}
}

void AZombieCharacter::GetLifetimeReplicatedProps(TArray<class FLifetimeProperty>& OutLifetimeProps) const
{
	Super::GetLifetimeReplicatedProps(OutLifetimeProps);
}

void AZombieCharacter::Restart()
{
	Super::Restart();
}

void AZombieCharacter::FellOutOfWorld(const class UDamageType& dmgType)
{
	Super::FellOutOfWorld(dmgType);
}

void AZombieCharacter::InputLook(const FInputActionValue& Value)
{
	FVector2d Input = Value.Get<FVector2d>();

	AddControllerYawInput(Input.X);
	AddControllerPitchInput(Input.Y);
}

void AZombieCharacter::InputMove(const FInputActionValue& Value)
{
	FVector2d Input = Value.Get<FVector2d>();

	AddMovementInput(GetActorRightVector(), Input.X);
	AddMovementInput(GetActorForwardVector(), Input.Y);
}

void AZombieCharacter::InputJumpStart(const FInputActionValue& Value)
{
	Super::Jump();
}

void AZombieCharacter::InputJumpEnd(const FInputActionValue& Value)
{
	Super::StopJumping();
}
