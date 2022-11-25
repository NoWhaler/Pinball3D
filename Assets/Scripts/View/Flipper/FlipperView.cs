using Pinball.Presenter;
using UnityEngine;
using View;

public class FlipperView : MonoBehaviour, IFlipperView
{
    [Header("Physics fields")]
    [SerializeField] private float _minLimit;
    [SerializeField] private float _maxLimit;
    [SerializeField] private float _springForce;
    [SerializeField] private float _springDamper;
    [SerializeField] private HingeJoint _hingeJoint;
    
    private JointSpring _jointSpring;

    private IFlipperPresenter _flipperPresenter;
    private Touch _touch;

    [SerializeField] private FlipperType _flipperType;

    private void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _jointSpring = new JointSpring();
        _flipperPresenter = new FlipperPresenter(this);
        Input.multiTouchEnabled = true;
    }

    private void Update()
    {
        _flipperPresenter.AddTorque();
    }

    public void AddSpringToFlipper()
    {
        if (Input.touchCount <= 0) return;
        _touch = Input.GetTouch(0);
        CheckForFlipperType();
    }

    private void CheckForFlipperType()
    {
        switch (_flipperType)
        {
            case FlipperType.LeftFlipper:
                if (_touch.position.x < Screen.width / 2f)
                {
                    AddSpringForce();
                }
                break;
            case FlipperType.RightFlipper:
                if (_touch.position.x > Screen.width / 2f)
                {
                    AddSpringForce();
                }
                break;
        }
    }

    private void AddSpringForce()
    {
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
