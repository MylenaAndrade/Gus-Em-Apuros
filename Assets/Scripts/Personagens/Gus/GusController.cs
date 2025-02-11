using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GusController : MonoBehaviour
{
    private Rigidbody2D _gusRididibody2D;
    private Animator    _gusAnimator;
    public float        _gusSpeed;
    private Vector2     _gusDirection;
    // Start is called before the first frame update
    void Start()
    {
        _gusRididibody2D = GetComponent<Rigidbody2D>();
        _gusAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _gusDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(_gusDirection.sqrMagnitude > 0.1)
        {
            MoveGus();

            _gusAnimator.SetFloat("AxisX", _gusDirection.x);
            _gusAnimator.SetFloat("AxisY", _gusDirection.y);
            
            _gusAnimator.SetInteger("Movement", 1);
        }
        else
        {
            _gusAnimator.SetInteger("Movement", 0);
        }
        
        _gusRididibody2D.MovePosition(_gusRididibody2D.position + _gusDirection * _gusSpeed * Time.fixedDeltaTime);
    }

    void MoveGus()
    {
        
    }
    void Flip()
    {
        if(_gusDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if(_gusDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }  
    }
}