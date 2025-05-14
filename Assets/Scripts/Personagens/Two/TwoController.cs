using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TwoController : MonoBehaviour
{
    public GameObject _gus;
    public Animator _twoAnimator;

    public float _speed = 5.0f;
    public float _keepDistance = 1.0f;

    bool _isWalking;
    
    float _inputX;
    float _inputY;
    float _lastDirectionX;
    float _lastDirectionY;

    Vector2 _twoPos;
    Vector2 _gusPos;
    
    private void Start()
    {
        _twoAnimator = GetComponent<Animator>();
        _gus = GameObject.FindGameObjectWithTag("Player");

        // Direção inicial (ex: seguindo por trás de Gus, para baixo)
        _lastDirectionX = 0;
        _lastDirectionY = -1;

        // Posição inicial com base na direção e distância
        Vector2 initialPos = SetDirection(_lastDirectionX, _lastDirectionY, _gus.transform.position);

        // Posiciona Two diretamente
        transform.position = initialPos;
    }

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
        _isWalking = _inputX != 0 || _inputY != 0;

        if(_isWalking)
        {
            _twoAnimator.SetFloat("AxisX", _inputX);
            _twoAnimator.SetFloat("AxisY", _inputY);
        }

        if(_inputX > 0 || _inputX < 0)
        {
            _lastDirectionX = _inputX;
        }

        if(_inputY > 0 || _inputY < 0)
        {
            _lastDirectionY = _inputY;
        }
    
        _twoAnimator.SetBool("Movement", _isWalking);

        _twoPos = transform.position;
        _gusPos = SetDirection(_lastDirectionX, _lastDirectionY, _gus.transform.position);

        transform.position = Vector2.MoveTowards(_twoPos, _gusPos, _speed * Time.deltaTime);
    }

    Vector2 SetDirection(float _inputX, float _inputY, Vector2 _gusPos)
    {
        if(_inputX < 0)
        {
            _gusPos.x += _keepDistance;
        }
        else if(_inputX > 0)
        {
            _gusPos.x -= _keepDistance;
        }

        if(_inputY < 0)
        {
            _gusPos.y += _keepDistance;
        }
        else if(_inputY > 0)
        {
            _gusPos.y -= _keepDistance;
        }
        return _gusPos;
    }
}
