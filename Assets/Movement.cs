using UnityEngine;
[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Movement : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    public float yForce = -0.1f;
    private Vector3 moveDirection;
    public bool groundedChecker;

    [SerializeField] 
    private float moveSpeedModifier = 3;
    [SerializeField] 
    private float jumpForce = 10;
    [SerializeField]
    private float maxFallSpeed = -10f;


    void Start()
    {
        _controller = GetComponent<CharacterController> ();
        _animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        groundedChecker = _controller.isGrounded;
        if (yForce > maxFallSpeed) yForce -= (Time.deltaTime * 10);

        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        if (Input.GetButtonDown("Jump") && _controller.isGrounded)
        {
            yForce = jumpForce;
            _animator.Play("Jumping");
        }

        //moveDirection = new Vector3(0, yForce, 0);
        //moveDirection = transform.rotation * moveDirection;

        moveDirection = new Vector3(0, yForce, Input.GetAxis("Vertical") * moveSpeedModifier) * Time.deltaTime;
        moveDirection = transform.rotation * moveDirection;

        _animator.SetBool("isGrounded", _controller.isGrounded);
        _controller.Move(moveDirection);
        _animator.SetFloat("Blend", Input.GetAxis("Vertical"));
    }
}
