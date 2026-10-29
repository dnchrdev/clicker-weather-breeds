using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedDetailsPopup: MonoBehaviour
    {
        [SerializeField] private TMP_Text _breedName;
        [SerializeField] private TMP_Text _breedDescription;
        [SerializeField] private Button _closeButton;
        
        private readonly Subject<Unit> _closeClicked = new Subject<Unit>();

        public IObservable<Unit> CloseClicked => _closeClicked;

        private void Awake()
        {
            _closeButton.OnClickAsObservable()
                .Subscribe(_ => _closeClicked.OnNext(Unit.Default))
                .AddTo(this);
        }
        
        public void Show(string breedName,  string description)
        {
            _breedName.text = breedName;
            _breedDescription.text = description;
            
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _closeClicked.OnCompleted();
        }
    }
}
