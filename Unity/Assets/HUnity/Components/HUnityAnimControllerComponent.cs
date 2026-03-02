using System;
using UnityEngine;

namespace HUnity.Components
{
    public class HUnityAnimControllerComponent : MonoBehaviour
    {
        public event Action OnFinished;

        public void FinishAnimation()
        {
            OnFinished?.Invoke();
        }
    }
}