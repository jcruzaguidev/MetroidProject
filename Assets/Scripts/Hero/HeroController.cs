using System;
using System.Collections;
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
        [SerializeField] private bool canDoubleJump;

        [Header("Movement Variables")]
        [SerializeField] private float movHor;
        [SerializeField] private float jumpForce;
        [SerializeField] private float doubleJumpForce;

        [Header("Checker Variables")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool facingRight = true;
        [SerializeField] private bool jumpPressed;
        [SerializeField] private LayerChecker layerChecker;

        private Rigidbody2D _rigidbody2D;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animatorController.Play(AnimationId.Idle);
        }

        private void Update()
        {
            HandleControls();
            HandleJump();
        }

        private void HandleControls()
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movHor = Input.GetAxisRaw("Horizontal");
            jumpPressed = Input.GetButtonDown("Jump");
            isGrounded = layerChecker.isTouching;

            if (movHor < 0f && facingRight)
                HandleFlip();
            else if (movHor > 0f && !facingRight)
                HandleFlip();

            var velocity = Math.Abs(_rigidbody2D.velocity.x);
            _animatorController.Play(velocity > 0 ? AnimationId.Run : AnimationId.Idle);
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

        private void HandleJump()
        {
            if (!jumpPressed) return;


            if (isGrounded)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
            }
            else
            {
                if (!canDoubleJump) return;

                _rigidbody2D.velocity = Vector2.up * doubleJumpForce;
                StartCoroutine(HandleJumpAnimation());
                canDoubleJump = false;
            }
        }

        private IEnumerator HandleJumpAnimation()
        {
            yield return new WaitForSeconds(0.1f);
            _animatorController.Play(AnimationId.Jump);
        }

    }
}