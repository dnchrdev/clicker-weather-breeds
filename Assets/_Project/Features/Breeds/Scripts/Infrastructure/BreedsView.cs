using System;
using System.Collections.Generic;
using _Project.Core.Scripts.Infrastructure;
using _Project.Features.Breeds.Scripts.Entities;
using _Project.Features.Breeds.Scripts.Infrastructure.BreedCard;
using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedsView: MonoBehaviour, IBreedsView
    {
        [SerializeField] private BreedDetailsPopup _breedDetailsPopup;
        [SerializeField] private GameObject _listLoader;
        [SerializeField] private Transform _showedContainer;
        [SerializeField] private Transform _hiddenContainer;
        
        private readonly Subject<string> _cardClicked = new Subject<string>(); 
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private readonly ObjectPool<IBreedCard> _pool = new ObjectPool<IBreedCard>();
        private readonly List<IBreedCard> _activeCards = new List<IBreedCard>();

        private IBreedCardFactory _cardFactory;

        public IObservable<string> CardClicked => _cardClicked;
        public IObservable<Unit> PopupCloseClicked => _breedDetailsPopup.CloseClicked;

        [Inject]
        public void Construct(IBreedCardFactory cardFactory)
        {
            _cardFactory = cardFactory;
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            _breedDetailsPopup.Hide();
            gameObject.SetActive(false);
        }

        public void GenerateBreedButtons(IReadOnlyList<Breed> breeds)
        {
            RemoveBreedsButtons();

            for (int i = 0; i < breeds.Count; i++)
            {
                var card = GetCard();

                card.Initialize(breeds[i].Id, $"{i + 1} - {breeds[i].Name}");
                card.Show(_showedContainer);

                _activeCards.Add(card);
            }
        }

        public void RemoveBreedsButtons()
        {
            for (int i = _activeCards.Count - 1; i >= 0; i--)
            {
                var card = _activeCards[i];

                card.Hide(_hiddenContainer);
                _pool.Return(card);
            }

            _activeCards.Clear();
        }

        public void SetListLoading(bool isLoading)
        {
            _listLoader.SetActive(isLoading);
        }
        
        public void SetCardLoading(string breedId)
        {
            for (int i = 0; i < _activeCards.Count; i++)
            {
                var card = _activeCards[i];

                if (breedId != null && card.Id == breedId)
                    card.ShowLoading();
                else
                    card.HideLoading();
            }
        }

        public void ShowBreedPopup(string breedName, string breedDescription)
        {
            _breedDetailsPopup.Show(breedName, breedDescription);
        }

        public void HideBreedPopup()
        {
            _breedDetailsPopup.Hide();
        }

        private IBreedCard GetCard()
        {
            if (_pool.TryGet(out var card))
                return card;

            card = _cardFactory.Create(_hiddenContainer);

            card.CardClicked
                .Subscribe(breedId => _cardClicked.OnNext(breedId))
                .AddTo(_disposables);

            return card;
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
            _cardClicked.OnCompleted();
        }
    }
}
