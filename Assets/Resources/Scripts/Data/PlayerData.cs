using UnityEngine;

namespace WalkingMan
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Data/Unit/PlayerSettings")]
    public sealed class PlayerData : ScriptableObject
    {
        public GameObject PlayerPrefab;
        [SerializeField] private string _name = "Player";
        [SerializeField, Range(0, 10)] private float _speed = 1.0f;
        [SerializeField] private Vector2 _position = new Vector2(0.0f, -7.0f);

        public string Name => _name;
        public float Speed => _speed;
        public Vector2 Position => _position;
    }
}
