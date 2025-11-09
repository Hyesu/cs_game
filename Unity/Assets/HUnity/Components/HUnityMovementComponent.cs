using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HEngine.Core;
using HEngine.Extensions;
using HUnity.Extensions;

namespace HUnity.Components
{
    public class HUnityMovementComponent : HActorComponent
    {
        private const float ArriveDistSquared = 0.01f * 0.01f;
        
        private readonly Queue<Vector3> _segments = new();
        private float _speed;

        public event Action OnArrived;
        public bool HasDestination => 0 < _segments.Count;
        
        public HUnityMovementComponent() : base()
        {
            Pref.Tickable = true;
        }

        public override void BeginPlay()
        {
            base.BeginPlay();

            var owner = GetOwner();
            var meta = owner.GetComponent<HUnityActorMetaComponent>();
            
            _speed = meta.D.Speed;
        }
        
        public override void Tick(float dt)
        {
            base.Tick(dt);
            MoveNextSegment(dt);
        }

        public void Go(IList<Vector3> segments)
        {
            Stop();
            _segments.AddRange(segments);
        }

        public void Stop()
        {
            _segments.Clear();
        }

        private void MoveNextSegment(float dt)
        {
            if (!HasDestination)
            {
                return;
            }

            var owner = GetOwner();
            var gObj = owner.GetComponent<HUnityGameObjectComponent>();
            var segment2d = _segments.First().ToVector2();
            var current = gObj.GetPosition();
            var current2d = current.ToVector2();
            var distanceSquared = current2d.DistanceSquared(segment2d);
            if (distanceSquared <= ArriveDistSquared)
            {
                _segments.Dequeue();

                if (!HasDestination)
                {
                    OnArrived?.Invoke();
                }
                return;
            }
            
            var delta = (segment2d - current2d).normalized * _speed;
            var nextPos = new Vector3(current.x + delta.x * dt, current.y, current.z + delta.y * dt);
            
            gObj.SetPosition(nextPos);
        }
    }
}