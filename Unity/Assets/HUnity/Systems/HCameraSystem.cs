using UnityEngine;
using UnityEngine.InputSystem;
using HEngine.Core;
using HUnity.Core;
using HUnity.Extensions;

namespace HUnity.Systems
{
    public class HCameraSystem : HSystem, IHInputInterpretable
    {
        private Camera _mainCamera;
        private float _speed = 30f;
        private LayerMask _terrainMask;
        private LayerMask _interactableMask;

        public override void Initialize()
        {
            base.Initialize();

            _speed = HConfiguration.Shared.CameraDefaultSpeed;
            _terrainMask = LayerMask.GetMask("Terrain");
            _interactableMask = LayerMask.GetMask("Interactable");
        }

        public override void BeginPlay()
        {
            base.BeginPlay();

            _mainCamera = Camera.main;
        }

        public override void EndPlay()
        {
            base.EndPlay();
            _mainCamera = null;
        }

        public void UpdateCommand(HInputCommand command, float dt)
        {
            if (0 != command)
            {
                MoveCamera(command, dt);
            }
        }

        public bool TryGetMousePosition(out Vector3 outPosition)
        {
            outPosition = Vector3.zero;
            
            if (!_mainCamera)
            {
                return false;
            }

            return TryProjectToWorld(Mouse.current.position.value, out outPosition);
        }

        public bool TryGetTerrainObject(out GameObject outObject)
        {
            outObject = null;
            
            if (!_mainCamera)
            {
                return false;
            }
            
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.value);
            if (!Physics.Raycast(ray, out var hit, 10_000f, _terrainMask))
            {
                return false;
            }

            outObject = hit.collider.gameObject;
            return true;
        }
        
        public bool TryGetInteractableObject(out GameObject outObject)
        {
            outObject = null;
            
            if (!_mainCamera)
            {
                return false;
            }
            
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.value);
            if (!Physics.Raycast(ray, out var hit, 10_000f, _interactableMask))
            {
                return false;
            }

            outObject = hit.collider.gameObject;
            return true;
        }

        public bool TryProjectToWorld(Vector2 screenPos, out Vector3 outPosition)
        {
            outPosition = Vector3.zero;
            
            var ray = _mainCamera.ScreenPointToRay(screenPos);
            if (!Physics.Raycast(ray, out var hit, 10_000f, _terrainMask))
            {
                return false;
            }
            
            outPosition = hit.point;
            return true;
        }

        public bool TryProjectToCell(Vector2 screenPos, out Vector3 outPosition)
        {
            if (!TryProjectToWorld(screenPos, out outPosition))
            {
                return false;
            }

            outPosition = outPosition.NormalizeToCell();
            return true;
        }

        private void MoveCamera(HInputCommand command, float dt)
        {
            if (!_mainCamera)
            {
                return;
            }
            
            var deltaPos = Vector3.zero;
            var deltaValue = _speed * dt;

            if ((command & HInputCommand.Left) != 0)
                deltaPos.x -= deltaValue;

            if ((command & HInputCommand.Right) != 0)
                deltaPos.x += deltaValue;

            if ((command & HInputCommand.Up) != 0)
                deltaPos.z += deltaValue;

            if ((command & HInputCommand.Down) != 0)
                deltaPos.z -= deltaValue;
            
            Vector3 camForward = _mainCamera.transform.forward;
            camForward.y = 0; // 바라보는 정면을 향해가기 위해 y는 0으로 사용
            camForward.Normalize();

            Vector3 camRight = _mainCamera.transform.right;
            camRight.y = 0; // 바라보는 정면을 향해가기 위해 y는 0으로 사용
            camRight.Normalize();

            // 입력 방향을 카메라 기준으로 변환
            Vector3 moveDir = camRight * deltaPos.x + camForward * deltaPos.z;
            _mainCamera.gameObject.transform.position += (moveDir * deltaValue);
        }
    }
}