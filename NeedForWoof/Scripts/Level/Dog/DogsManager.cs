using Godot;
using System.Collections.Generic;
using System.Linq;

namespace NeedForWoof.Level
{
    // TODO Чисти гавно

    /// <summary>
    /// Creates, deletes and manages dogs on the level.
    /// </summary>
    public class DogsManager : YSort
    {
        /// <value>
        /// Position of the all dogs on start the game. <br/>
        /// Used for initial dog positioning.
        /// </value>
        private const int DogsStartYPosition = 600;

        /// <value>
        /// Road width on start of the game.<br/>
        /// Used for initial dog positioning.
        /// </value>        
        private const int StartRoadWidth = 380;

        /// <value>
        /// Road width offset on start of the game.<br/>
        /// Used for initial dog positioning.
        /// </value>   
        private const int StartRoadWidthOffset = 47;

        private Network _network;
        private Dictionary<int, PlayerInfo> _playersInfo;

        private Dictionary<int, Dog> _dogs;

        public override void _Ready()
        {
            _network = GetNode<Network>("/root/Network");
            _playersInfo = _network.ConnectedPlayers;

            _dogs = new Dictionary<int, Dog>();
            CreateDogs();

            _network.Connect(nameof(Network.PlayerLeft), this, nameof(DeleteDog));
        }

        public Dog GetSelfDog()
        {
            int id = GetTree().GetNetworkUniqueId();
            return _dogs[id];
        }

        public Dog GetDogById(int id)
        {
            return _dogs[id];
        }

        private void CreateDogs()
        {
            foreach (var player in _playersInfo.OrderBy(x => x.Key))
            {
                Dog dog = CreateNewDog(player.Key, player.Value);
            }

            AddDogMovementController(GetSelfDog());
            SetDogsStartPosotions();
            AttachPlayerCamera(GetSelfDog());

            foreach (var dog in _dogs)
            {
                AddChild(dog.Value, true);
            }
        }

        private Dog CreateNewDog(int playerKey, PlayerInfo playerInfo)
        {
            DogType dogType = playerInfo.Dog;
            Dog dog = DogCreator.CreateDog(dogType);
            _dogs.Add(playerKey, dog);
            dog.Name = playerKey.ToString();
            dog.SetNetworkMaster(playerKey);

            return dog;
        }

        // TODO Create factory method inside DogMvmntCtrl and refactor this
        private void AddDogMovementController(Dog dog)
        {
            DogMovement movement = dog.GetNode<DogMovement>("DogMovement");
            movement.SetNetworkMaster(dog.GetNetworkMaster());
            DogMovementController controller = new DogMovementController(movement);
            controller.Name = dog.Name;
            controller.SetNetworkMaster(dog.GetNetworkMaster());
            dog.AddChild(controller);
        }

        // TODO Create DogPositioner class and refactor it
        private void SetDogsStartPosotions()
        {
            int dogNumber = 1;
            foreach (var dog in _dogs)
            {
                float x = StartRoadWidthOffset + StartRoadWidth / (_dogs.Count + 1) * dogNumber;
                dog.Value.Position = new Vector2(x, DogsStartYPosition);

                dogNumber++;
            }
        }
        
        // TODO Create special PlayerCamera class and refactor it
        private void AttachPlayerCamera(Node2D target)
        {
            PackedScene playerCameraScene = GD.Load<PackedScene>("res://Scenes/Level/PlayerCamera.tscn");
            Camera2D playerCamera = playerCameraScene.Instance<Camera2D>();
            target.AddChild(playerCamera, true);
        }
        
        private void DeleteDog(int id)
        {
            if (_dogs.ContainsKey(id))
            {
                RemoveChild(_dogs[id]);
                _dogs.Remove(id);
            }
        }
    }
}