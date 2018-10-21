using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class KiteCharactorContollerHandlerImpl {

    public enum MoveDirection
    {
        Forward,
        Back,
        Left,
        Right,
    }

    public KiteCharactorInfo CharactorInfo { get; set; }

    public void GoToForward()
    {
        _currentFrameNeedMoveCache.Add(MoveDirection.Forward);
    }

    public void GoToBack()
    {
        _currentFrameNeedMoveCache.Add(MoveDirection.Back);
    }

    public void GoToLeft()
    {
        _currentFrameNeedMoveCache.Add(MoveDirection.Left);
    }

    public void GoToRight()
    {
        _currentFrameNeedMoveCache.Add(MoveDirection.Right);
    }

    public void Jump()
    {
        _currentFrameJumpCache = true;
    }

    public void Update()
    {
        UpdateMove();
        UpdateJump();
    }

    protected void UpdateMove()
    {
        var needUpdate = _currentFrameNeedMoveCache.Count != 0;
        if (needUpdate)
        {
            var moveVector = Vector3.zero;
            foreach (var direction in _currentFrameNeedMoveCache)
            {
                var needMoveVector = _moveDirectionVectorDic[direction];
                moveVector += needMoveVector;
            }

            if (!Mathf.Equals(moveVector.magnitude, 0.0f))
            {
                UpdateSpeed();
                var finalMoveVector = moveVector * MoveBaseValue;
                float yOffset;
                if (_charactorMoveLimitter.IsAllowMove(finalMoveVector, out yOffset))
                {
                    finalMoveVector.y += yOffset;
                    CharactorInfo.CharactorObj.transform.Translate(finalMoveVector);
                }
            }

            _currentFrameNeedMoveCache.Clear();
        }
        else
        {
            _currentMoveSpeed = 0.0f;
        }
    }

    protected void UpdateJump()
    {
        if(_currentFrameJumpCache)
        {
            if(_charactorMoveLimitter.IsAllowJump())
            {
                CharactorRigidBody.AddForce(CharactorInfo.CharactorObj.transform.up / CharactorRigidBody.mass * 1000000.0f * CharactorInfo.JumpForce);
            }

            _currentFrameJumpCache = false;
        }
    }

    protected void UpdateSpeed()
    {
        _currentMoveSpeed += Time.deltaTime * CharactorInfo.StartAcceleratedSpeed;
        _currentMoveSpeed = Mathf.Min(_currentMoveSpeed, CharactorInfo.WalkSpeed);
    }
}

public partial class KiteCharactorContollerHandlerImpl
{
    public KiteCharactorContollerHandlerImpl()
    {
        _charactorMoveLimitter.CharactorContollerHandlerImpl = this;
    }

    protected KiteCharactorMoveLimiter _charactorMoveLimitter = new KiteCharactorMoveLimiter();

    protected HashSet<MoveDirection> _currentFrameNeedMoveCache = new HashSet<MoveDirection>();
    protected Dictionary<MoveDirection, Vector3> _moveDirectionVectorDic = new Dictionary<MoveDirection, Vector3>()
    {
        { MoveDirection.Forward, Vector3.forward },
        { MoveDirection.Back, -Vector3.forward },
        { MoveDirection.Left, -Vector3.right },
        { MoveDirection.Right, Vector3.right },
    };

    protected bool _currentFrameJumpCache = false;

    protected float _currentMoveSpeed = 0.0f;

    protected float MoveBaseValue
    {
        get
        {
            return Time.deltaTime * _currentMoveSpeed;
        }
    }

    private Rigidbody _charactorRigidBody;

    public Rigidbody CharactorRigidBody
    {
        get
        {
            if(_charactorRigidBody == null)
            {
                var charactorObj = CharactorInfo.CharactorObj;
                if(charactorObj)
                {
                    _charactorRigidBody = charactorObj.GetComponent<Rigidbody>();
                }
            }

            return _charactorRigidBody;
        }
    }
}