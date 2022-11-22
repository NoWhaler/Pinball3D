using Pinball.Presenter;
using UnityEngine;
using View;

public class FlipperView : MonoBehaviour, IFlipperView
{
    // [SerializeField] private float _springForce;

    [SerializeField] private float _minLimit;
    [SerializeField] private float _maxLimit;
    [SerializeField] private float _springForce;
    [SerializeField] private float _springDamper;

    private IFlipperPresenter _flipperPresenter;

    [SerializeField] private HingeJoint _hingeJoint;
    private JointSpring _jointSpring;
    // private Rigidbody _rigidbody;
    private Touch _touch;

    private void Start()
    {
        // _rigidbody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
        _jointSpring = new JointSpring();
        _flipperPresenter = new FlipperPresenter(this);
    }

    private void Update()
    {
        _flipperPresenter.AddTorque();
    }

    public void AddTorqueToFlipper()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _jointSpring.spring = _springForce;
            _jointSpring.damper = _springDamper;
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    _jointSpring.targetPosition = _maxLimit;
                    break;
                case TouchPhase.Ended:
                    _jointSpring.targetPosition = _minLimit;
                    break;
            }
            _hingeJoint.spring = _jointSpring;
        }
    }
}
