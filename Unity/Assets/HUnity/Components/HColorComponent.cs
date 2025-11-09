using UnityEngine;

namespace HUnity.Components
{
    [ExecuteAlways]
    public class HUnityColorComponent : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
        
        [SerializeField] private GameObject _target;
        [SerializeField] private Color _color;

        private void OnEnable()
        {
            ApplyColor();
        }

        private void OnValidate()
        {
            ApplyColor();
        }

        private void ApplyColor()
        {
            var r = _target?.GetComponent<MeshRenderer>();
            if (!r) 
                return;

            var block = new MaterialPropertyBlock();
            r.GetPropertyBlock(block);
            block.SetColor(BaseColorId, _color);
            r.SetPropertyBlock(block);
            
#if UNITY_EDITOR
            if (!Application.isPlaying) // 게임 실행 중이 아닐 때만 적용
            {
                UnityEditor.EditorUtility.SetDirty(r);
            }
#endif
        }
    }
}