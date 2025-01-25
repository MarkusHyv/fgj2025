using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void OnScoreChanged(int value)
        {
            _text?.SetText(value.ToString());
        }
    }
}
