using Loader;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    [DisallowMultipleComponent()]
    [RequireComponent(typeof(Canvas))]
    public class BootstrapUI : MonoBehaviour, IObserver<LoaderData>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI percentageText;

        public void OnCompleted()
        {
            Debug.Log("Completed");
        }

        public void OnError(Exception error)
        {
            Debug.LogError($"{error.Message}");
        }

        public void OnNext(LoaderData value)
        {
            slider.value = value.Progress;
            percentageText.text = $"{value.Progress * 100}%";
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
