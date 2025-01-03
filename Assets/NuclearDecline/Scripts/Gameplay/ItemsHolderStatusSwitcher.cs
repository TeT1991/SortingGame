using System;

namespace NuclearDecline.Gameplay
{
    public class ItemsHolderStatusSwitcher
    {
        private HolderStatus _status = HolderStatus.NotSelected;

        public HolderStatus Status => _status;

        public Action <HolderStatus> OnStatusChanged;

        public void SetStatus(HolderStatus status)
        {
            _status = status;
            OnStatusChanged?.Invoke(_status);
        }
    }
}

