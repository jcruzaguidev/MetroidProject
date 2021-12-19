using System;
using UnityEngine;

namespace Hero
{
   public class HeroController : MonoBehaviour
   {
      [Header("Hero Variables")] 
      [SerializeField] private float speed;

      [Header("Animation Variables")] 
      [SerializeField] private Vector2 movementDirection;
      [SerializeField] private AnimatorController _animatorController;

      [SerializeField] private float movHor;

      [Header("Checker Variables")] 
      [SerializeField] private bool facingRight = true;

      private Rigidbody2D _rigidbody2D;


      private void Start()
      {
         _rigidbody2D = GetComponent<Rigidbody2D>();
         _animatorController.Play(AnimationId.Idle);
      }

      private void Update()
      {
         HandleControls();
      }

      private void HandleControls()
      {
         movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
         movHor = Input.GetAxisRaw("Horizontal");

         if (movHor < 0f && facingRight)
            HandleFlip();
         else if (movHor > 0f && !facingRight)
            HandleFlip();

         var velocity = Math.Abs(_rigidbody2D.velocity.x);
         _animatorController.Play( velocity > 0 ? AnimationId.Run : AnimationId.Idle );
      }

      private void FixedUpdate()
      {
         _rigidbody2D.velocity = new Vector2(movementDirection.x * speed, _rigidbody2D.velocity.y);
      }

      private void HandleFlip()
      {
         facingRight = !facingRight;
         var localScaleX = transform.localScale.x;
         localScaleX *= -1;
         transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
      }
   }
}