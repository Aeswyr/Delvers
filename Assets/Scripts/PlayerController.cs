using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler input;
    private PlayerCharacter character;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private Animator animator;
    [SerializeField] private List<AnimatorOverrideController> animators;

    [Header("Stats")]
    [SerializeField] private float speed;

    private bool prevGrounded, grounded;
    private bool inputLockout;
    private int facing = 1;
    private float gravity;
    private bool motionDecay;
    private float decayMod = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        gravity = rbody.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prevGrounded = grounded;
        grounded = ground.CheckGrounded(); 

        if (!inputLockout)
            UpdateInputs();
        else if (grounded || motionDecay) {
            Vector2 velocity = rbody.velocity;
            velocity.x *= decayMod;
            rbody.velocity = velocity;
        }

            
    }

    private void UpdateInputs() {
        Vector2 velocity = Vector2.zero;
        velocity.y = rbody.velocity.y;

        if (input.dir.x != 0) {
            if (input.dir.x > 0) {
                sprite.flipX = false;
                velocity.x = speed;
                facing = 1;
            } else if (input.dir.x < 0) {
                sprite.flipX = true;
                velocity.x = -speed;
                facing = -1;
            }
        }

        if (grounded && input.jump.pressed) {
            velocity.y = 40;
        }

        if (input.primary.pressed) {
            animator.SetTrigger("primary");
            StartAction();
        }

        if (CanSecondary() && input.secondary.pressed) {
            TrySecondary(ref velocity);
        }

        if (input.item.pressed) {
            TryUseItem();
        }

        if (input.interact.pressed) {
            TryInteract();
        }


        rbody.velocity = velocity;
    }

    private void TrySecondary(ref Vector2 velocity) {
        switch (character) {
            case PlayerCharacter.DUELIST:
                animator.SetTrigger("secondary");
                velocity.x = 60 * facing;
                motionDecay = true;
                decayMod = 0.90f;

                velocity.y = 0;
                rbody.gravityScale = 0;
                StartAction();
                break;
            case PlayerCharacter.MAGE:
                animator.SetTrigger("secondary");
                velocity = Vector2.zero;
                rbody.gravityScale = 0;
                StartAction();

                float dist = 0;
                for (float i = 5; i > 0; i -= 0.5f) {
                    if (!Utils.Boxcast(transform.position + Vector3.up, new Vector2(1, 2), input.dir, i, LayerMask.GetMask("World"))) {
                        dist = i;
                        break;
                    }
                }

                transform.position += (Vector3)(dist * input.dir);
                break;
            case PlayerCharacter.WARRIOR:

                break;
            case PlayerCharacter.ARCHER:

                break;
            
        }
    }

    private bool CanSecondary() {
        return true;
    }

    private void TryUseItem() {

    }

    private void TryInteract() {

    }

    public void AddCoins(int amt) {

    }

    public bool OnHit() {
        return false;
    }

    private void StartAction() {
        inputLockout = true;
    }

    private void EndAction() {
        inputLockout = false;
        rbody.gravityScale = gravity;
        motionDecay = false;
        decayMod = 0.8f;
    }

    public void Init(PlayerCharacter character, InputHandler input) {
        this.character = character;
        this.input = input;

        animator.runtimeAnimatorController = animators[(int)character];
    }
}
