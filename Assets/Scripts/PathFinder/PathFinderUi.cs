using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Infrastructure
{
    internal class PathFinderUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stationsText;
        [SerializeField] private TMP_InputField _startPathInput;
        [SerializeField] private TMP_InputField _endPathInput;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _findPathButton;

        public event Action<string, string> OnFindPathRequest;


        public void Awake()
        {
            _findPathButton.onClick.AddListener(InvokeFindPathRequest);
            _startPathInput.onValueChanged.AddListener(ClearResult);
        }

        public void ShowCurrentStations(string stationsString)
        {
            _stationsText.text = stationsString;
        }

        public void ShowPath(PathInfo pathInfo)
        {
            _resultText.text = GetResultText(pathInfo);
        }

        private void InvokeFindPathRequest()
        {
            OnFindPathRequest?.Invoke(_startPathInput.text.ToUpper(), _endPathInput.text.ToUpper());
        }

        private string GetResultText(PathInfo pathInfo)
        {
            if (pathInfo.IsError)
                return pathInfo.ErrorMessage;

            if (pathInfo.StationsPath.Count == 1)
                return Constants.ALREADY_HERE_MESSAGE;

            return pathInfo.GetPathString();
        }

        private void ClearResult(string _)
        {
            _resultText.text = string.Empty;
        }
    }
}