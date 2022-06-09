using System;
using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float _launchForce = 500;
    [SerializeField] private float _maxDragDistance = 5;
    private Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriterRederer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void OnMouseDown()
    {
        _spriterRederer.color = Color.red;
    }

     void OnMouseUp()
     {
         Vector2 currentPosition = _rigidbody2D.position;
         Vector2 direction = _startPosition - currentPosition;
         direction.Normalize();
         
         _rigidbody2D.isKinematic = false;
         _rigidbody2D.AddForce(direction * _launchForce);
         
         _spriterRederer.color = Color.white;
    }

     private void OnMouseDrag()
     {
         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

         Vector2 desiredPosition = mousePosition;

         float distance = Vector2.Distance(desiredPosition, _startPosition);
         if (distance > _maxDragDistance)
         {
             Vector2 direction = desiredPosition - _startPosition;
             direction.Normalize();
             desiredPosition = _startPosition + (direction * _maxDragDistance);
         }
         
         if (desiredPosition.x > _startPosition.x)
             desiredPosition.x = _startPosition.x;
             
         
         _rigidbody2D.position = desiredPosition;
     }


     private void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(2);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D .velocity = Vector2.zero;
    }
}
