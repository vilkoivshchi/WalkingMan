using UnityEngine;

namespace WalkingMan
{
    public sealed class MoveController : IInitialization, IExecute, ICleanup
    {
        private readonly Transform _unit;
        private readonly Collider2D _unitCollider;
        private readonly IPlayerModel _unitData;
        private readonly CameraController _cameraController;
        private float _horizontal;
        private float _vertical;
        private Vector3 _move;
        private IUserInputProxy _horizontalInputProxy;
        private IUserInputProxy _verticalInputProxy;
        private Animator _animator;

        public MoveController(InputData input, Transform unit, IPlayerModel unitData, CameraController cameraController)
        {
            _unit = unit;
            _unitCollider = _unit.gameObject.GetComponent<Collider2D>();
            _unitData = unitData;
            _cameraController = cameraController;
            _horizontalInputProxy = input.InputHorizontal;
            _verticalInputProxy = input.InputVertical;
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;
        }

        private void HorizontalOnAxisOnChange(float value)
        {
            _horizontal = value;
            
        }
        
        private void VerticalOnAxisOnChange(float value)
        {
            _vertical = value;
        }
        
        public void Initialization()
        {
            _animator = _unit.gameObject.GetComponent<Animator>();
        }

        public void Execute(float deltaTime)
        {
            var speed = deltaTime * _unitData.Speed;

            _move.Set(_horizontal * speed, _vertical * speed, 0.0f);
            _unit.localPosition += _move;
            if (_horizontal != 0)
            {
                _animator.SetBool("isWalk", true);
            }
            else
            {
                _animator.SetBool("isWalk", false);
            }

            if(_horizontal > 0)
            {
                _unit.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if(_horizontal < 0)
            {
                _unit.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if(_vertical < 0)
            {
                _animator.SetBool("isSitting", true);
            }
            else
            {
                _animator.SetBool("isSitting", false);
            }
        }

        public void Cleanup()
        {
            _horizontalInputProxy.AxisOnChange -= HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange -= VerticalOnAxisOnChange;
        }
    }
}
