using Godot;
using System.Collections.Generic;

namespace NeedForWoof.Level
{
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

        private void CreateDogs()
        {
            int dogNumber = 1;

            foreach(var player in _playersInfo)
            {
                DogType dogType = player.Value.Dog;
                Dog dog = DogCreator.CreateDog(dogType);
                _dogs.Add(player.Key, dog);

                float x = StartRoadWidthOffset + StartRoadWidth / (_playersInfo.Count + 1) * dogNumber;
                dog.Position = new Vector2(x, DogsStartYPosition);
                dog.Id = player.Key;
                dogNumber++;
            }

            PackedScene playerCameraScene = GD.Load<PackedScene>("res://Scenes/Level/PlayerCamera.tscn");
            Camera2D playerCamera = playerCameraScene.Instance<Camera2D>();
            Dog localDog = _dogs[GetTree().GetNetworkUniqueId()];
            localDog.AddChild(playerCamera);

            foreach(var dog in _dogs)
            {
                AddChild(dog.Value);
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