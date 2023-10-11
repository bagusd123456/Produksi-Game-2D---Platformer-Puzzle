using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Properties")]
    public Ease easePreset = Ease.OutSine;
    [SerializeField]
    private Transform position1, position2;
    public ButtonPlatform button;

    [Header("Platform Parameter")]
    public float _duration = 3.0f;
    public bool _switch = false;

    public ButtonData buttonData;
    public buttonType _buttonType;
    public enum buttonType { Button, Pressure };

    public bool isPlaying;
    public float timeRemaining = 3;

    void FixedUpdate()
    {
        isPlaying = DOTween.IsTweening(1, true);

        /*
        if (_switch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, position1.position,
                _speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, position2.position,
                _speed * Time.deltaTime);
        }

        if (transform.position == position1.position)
        {
            _switch = true;
        }
        else if (transform.position == position2.position)
        {
            _switch = false;
        }*/

        if (_buttonType == buttonType.Button)
        {
            UseButton();
        }
        else if (_buttonType == buttonType.Pressure)
        {
            UsePressure();
        }


        if (_switch)
        {
            Tween myTween = gameObject.transform.DOMove(position2.position, _duration).SetEase(easePreset).SetId(1).SetAutoKill(true);
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            
            if (DOTween.IsTweening(myTween, true) && timeRemaining <= 0)
            {
                //DOTween.Kill(1,false);
            }
        }
        else
        {
            timeRemaining = _duration;
            Tween endingTween = gameObject.transform.DOMove(position1.position, _duration).SetEase(easePreset).SetId(2).SetAutoKill(true);
        }
    }

    void UseButton()
    {
        if (button != null)
        {
            _switch = button.isActive;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K) && !_switch)
            {
                _switch = true;
            }
            else if (Input.GetKeyDown(KeyCode.K) && _switch)
            {
                _switch = false;
            }
        }
    }

    void UsePressure()
    {
        _switch = buttonData.isPressed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (!collision.transform.CompareTag("Player"))
        {
            DOTween.TogglePause(1);
            position2.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }*/

        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(position2.position, new Vector3(5f, 0.8f, 1f));
    }
}
