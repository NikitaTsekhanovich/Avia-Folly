using DG.Tweening;
using UnityEngine;

namespace Airplanes
{
    public class Helicopter : Aircraft
    {
        [SerializeField] private Transform _propeller;
        private DG.Tweening.Core.TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> _anim;

        private void Start()
        {
            RotatePropeller();
        }

        private void RotatePropeller()
        {
            _anim = _propeller.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear);
        }

        public override void DoDestroy()
        {
            KillAnimationPropeller();
            Destroy(gameObject);
        }

        private void KillAnimationPropeller()
        {
            _anim?.Kill();
        }
    }
}

