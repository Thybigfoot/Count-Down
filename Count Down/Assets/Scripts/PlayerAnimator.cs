using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Drives the Animator from the existing IPlayerController.
    /// Attach to the same GameObject as PlayerController (or a child with the Animator).
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _playerControllerBehaviour; // drag your PlayerController here
        [SerializeField] private Rigidbody2D _rb; // drag the same Rigidbody2D used by PlayerController

        private static readonly int GroundedHash = Animator.StringToHash("Grounded");
        private static readonly int MovingHash = Animator.StringToHash("Moving");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int VerticalVelocityHash = Animator.StringToHash("VerticalVelocity");

        private Animator _animator;
        private IPlayerController _player;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _player = _playerControllerBehaviour as IPlayerController;

            if (_player == null)
                Debug.LogError("Assigned PlayerController does not implement IPlayerController", this);
        }

        private void OnEnable()
        {
            if (_player == null) return;
            _player.GroundedChanged += OnGroundedChanged;
            _player.Jumped += OnJumped;
        }

        private void Start()
        {
            // Force-sync the animator to the real current grounded state.
            if (_player is PlayerController pc)
            {
                _animator.SetBool(GroundedHash, pc.Grounded);
            }
        }

        private void OnDisable()
        {
            if (_player == null) return;
            _player.GroundedChanged -= OnGroundedChanged;
            _player.Jumped -= OnJumped;
        }

        private void Update()
        {
            if (_player == null) return;

            // Walk vs Idle
            bool moving = Mathf.Abs(_player.FrameInput.x) > 0.01f;
            _animator.SetBool(MovingHash, moving);

            // Flip sprite to face movement direction
            if (Mathf.Abs(_player.FrameInput.x) > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Sign(_player.FrameInput.x), 1, 1);
            }

            // Feed vertical velocity so the controller can decide Jump -> Fall itself
            if (_rb != null)
            {
                _animator.SetFloat(VerticalVelocityHash, _rb.linearVelocity.y);
            }
        }

        private void OnGroundedChanged(bool grounded, float impactVelocity)
        {
            _animator.SetBool(GroundedHash, grounded);
        }

        private void OnJumped()
        {
            _animator.SetTrigger(JumpHash);
        }
    }
}