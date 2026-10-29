using System;
using UniRx;

namespace _Project.Features.Breeds.Scripts.Entities
{
    public class BreedPopupInfo
    {
        private readonly Subject<BreedDetails> _opened = new Subject<BreedDetails>();
        private readonly Subject<Unit> _closed = new Subject<Unit>();

        public bool IsOpened { get; private set; }

        public IObservable<BreedDetails> Opened => _opened;
        public IObservable<Unit> Closed => _closed;

        public bool TryOpen(BreedDetails details)
        {
            if (!IsValid(details))
                return false;

            IsOpened = true;
            _opened.OnNext(details);

            return true;
        }

        public void Close()
        {
            if (!IsOpened)
                return;

            IsOpened = false;
            _closed.OnNext(Unit.Default);
        }

        private bool IsValid(BreedDetails details)
        {
            return details != null
                   && !string.IsNullOrEmpty(details.Name)
                   && !string.IsNullOrEmpty(details.Description);
        }
    }
}
