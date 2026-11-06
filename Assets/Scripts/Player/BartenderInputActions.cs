using System;
using UnityEngine.InputSystem;

public class BartenderInputActions : IDisposable
{
    private readonly InputActionAsset asset;
    private readonly InputActionMap player;
    private readonly InputAction move;

    public PlayerActions Player => new PlayerActions(this);

    public BartenderInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BartenderInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c0f7119d-71ad-4b2b-9a7c-d7b41e33d67e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""aa6644d0-06a0-4b30-907d-4cda6ea28e8f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f724b1a2-f58d-4777-9658-2c3fd2e4be32"",
                    ""path"": ""2DVector"",
                    ""action"": ""Move"",
                    ""isComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""09688a1e-1d3c-4c49-81ed-0f5228415629"",
                    ""path"": ""<Keyboard>/w"",
                    ""action"": ""Move"",
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9914a540-2a7f-4efb-9ff6-124740799132"",
                    ""path"": ""<Keyboard>/s"",
                    ""action"": ""Move"",
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e87f4356-35dd-4b8a-82d0-c1233779b75e"",
                    ""path"": ""<Keyboard>/a"",
                    ""action"": ""Move"",
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""349689e8-bfa8-4528-9d91-6615f105a2b6"",
                    ""path"": ""<Keyboard>/d"",
                    ""action"": ""Move"",
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        player = asset.FindActionMap("Player", true);
        move = player.FindAction("Move", true);
    }

    public void Enable() => player.Enable();

    public void Disable() => player.Disable();

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public struct PlayerActions
    {
        private readonly BartenderInputActions wrapper;

        internal PlayerActions(BartenderInputActions wrapper)
        {
            this.wrapper = wrapper;
        }

        public InputAction Move => wrapper.move;
    }
}