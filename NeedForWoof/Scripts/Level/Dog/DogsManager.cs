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
                DogType dogType = player.Value.Dog;
                Dog dog = DogCreator.CreateDog(dogType);
                _dogs.Add(player.Key, dog);
                dog.Id = player.Key;
            }

            int dogNumber = 1;
            foreach (var dog in _dogs)
            {
                float x = StartRoadWidthOffset + StartRoadWidth / (_playersInfo.Count + 1) * dogNumber;
                dog.Value.Position = new Vector2(x, DogsStartYPosition);

                DogMovementController controller = new DogMovementController();
                controller.Name = "MovementController";
                dog.Value.AddChild(controller, true);

                dogNumber++;
            }

            PackedScene playerCameraScene = GD.Load<PackedScene>("res://Scenes/Level/PlayerCamera.tscn");
            Camera2D playerCamera = playerCameraScene.Instance<Camera2D>();
            Dog selfDog = GetSelfDog();
            selfDog.AddChild(playerCamera, true);

            foreach (var dog in _dogs)
            {
                AddChild(dog.Value, true);
            }
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