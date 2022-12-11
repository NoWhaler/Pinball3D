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

    [SerializeField] private AudioClip _audioClip;
    
    private JointSpring _jointSpring;

    private IFlipperPresenter _flipperPresenter;
    private Touch _touch;

    [SerializeField] private FlipperType _flipperType;
    
    public float SpringForce
    {
        get => _springForce;
        set => _springForce = value;
    }

    private void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _jointSpring = new JointSpring();
        _flipperPresenter = new FlipperPresenter(this);
    }

    private void FixedUpdate()
    {
        _flipperPresenter.AddTorque();
    }

    public void AddSpringToFlipper()
    {
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);
            CheckForFlipperType();
        }
        CheckTypeFlipper();
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

    private void CheckTypeFlipper()
    {
        switch (_flipperType)
        {
            case FlipperType.LeftFlipper:
                if (Input.mousePosition.x < Screen.width / 2f)
                {
                    AddSpringForceForInput();
                }
                break;
            case FlipperType.RightFlipper:
                if (Input.mousePosition.x > Screen.width / 2f)
                {
                    AddSpringForceForInput();
                }
                break;
        }
    }

    private void AddSpringForceForInput()
    {
        _jointSpring.spring = _springForce;
        _jointSpring.damper = _springDamper;
        _jointSpring.targetPosition = Input.GetAxis("Fire1") >= 1 ? _maxLimit : _minLimit;
        _hingeJoint.spring = _jointSpring;
    }

    private void AddSpringForce()
    {
        _jointSpring.spring = _springForce;
        _jointSpring.damper = _springDamper;

        _jointSpring.targetPosition = _touch.phase switch
        {
            TouchPhase.Stationary => _maxLimit,
            TouchPhase.Ended => _minLimit,
            _ => _jointSpring.targetPosition
        };

        _hingeJoint.spring = _jointSpring;
    }
}
