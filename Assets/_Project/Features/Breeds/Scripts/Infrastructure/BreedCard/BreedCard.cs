using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.Breeds.Scripts.Infrastructure.BreedCard
{
    public class BreedCard: MonoBehaviour, IBreedCard
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _breedNameText;
        [SerializeField] private Image _loadingIcon;
        
        private Subject<string> _cardClicked = new Subject<string>(); 

        public string Id { get; private set; }
        public IObservable<string> CardClicked => _cardClicked;

        private void Awake()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _cardClicked.OnNext(Id))
                .AddTo(this);
        }
        
        public void Initialize(string breedId, string text)
        {
            Id = breedId;
            _breedNameText.text = text;
            HideLoading();
        }

        public void Show(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void Hide(Transform parent)
        {
            HideLoading();
            transform.SetParent(parent, false);
        }

        public void ShowLoading()
        {
            _loadingIcon.gameObject.SetActive(true);
        }

        public void HideLoading()
        {
            _loadingIcon.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _cardClicked.OnCompleted();
        }
    }
}
