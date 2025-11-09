using TMPro;
using UnityEngine;

namespace HUnity.Components
{
    public class HUnityAppearanceComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetName(string name)
        {
            _text.text = name;
        }
    }
}