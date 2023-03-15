using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.Utils
{
    [DisallowMultipleComponent()]
    public sealed class Toast : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private RectTransform panel;

        private Payload payload;
        private RectTransform rTransform;

        private void Awake()
        {
            rTransform = GetComponent<RectTransform>();
        }

        public void Initialize(Payload payload)
        {
            this.payload = payload;
            SetUI();
        }

        private void SetUI()
        {
            infoText.text = payload.Description;
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            yield return new WaitForSeconds(payload.Duration);
            var endPosition = new Vector2(transform.position.x, rTransform.rect.height - panel.position.y);
            var beginPosition = transform.position;
            var length = Mathf.Abs(endPosition.y - beginPosition.y);

            float timer = 0f;
            while(panel.position.y - endPosition.y < Mathf.Epsilon)
            {
                timer += Time.deltaTime;
                float fraction = (timer * payload.Speed)/length;
                transform.position = Vector2.Lerp(beginPosition, endPosition, fraction);
            }
        }

        public class Payload
        {
            public Payload(string description, float duration, float speed)
            {
                Description = description;
                Duration = duration;
                Speed = speed;
            }

            public string Description { get; private set; }
            public float Duration { get; private set; }
            public float Speed { get; private set; }
        }
    }
}