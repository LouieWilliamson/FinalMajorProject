using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    public Material teleportMat;
    private float progressValue;

    internal bool dissapearing;
    internal bool reappearing;

    public float effectSpeed;

    private float maxDissapear;
    private float maxReappear;

    internal bool disappearFXFinished;
    internal bool reappearFXFinished;

    private PlayerMovement pMove;
    private PlayerAttacks pAttack;
    private Rigidbody2D pRB;

    private float normalGravity;
    private float normalSpeed;
    private int normalJump;

    private float enableMvmtValue;
    // Start is called before the first frame update
    void Start()
    {
        pMove = GetComponent<PlayerMovement>();
        pAttack = GetComponent<PlayerAttacks>();
        pRB = GetComponent<Rigidbody2D>();

        normalGravity = pRB.gravityScale;
        normalSpeed = pMove.speed;
        normalJump = pMove.jumpHeight;

        disappearFXFinished = false;
        reappearFXFinished = false;

        progressValue = 2.5f;

        maxReappear = 2.5f;
        maxDissapear = 0;

        enableMvmtValue = 1.5f;

        dissapearing = false;
        reappearing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dissapearing)
        {
            progressValue -= effectSpeed * Time.deltaTime;
        }
        else if (reappearing)
        {
            progressValue += effectSpeed * Time.deltaTime;
        }

        if (teleportMat.GetFloat("EffectProgress") != progressValue)
        {
            teleportMat.SetFloat("EffectProgress", progressValue);
        }

        CheckProgressValue();
    }
    public void Dissapear()
    {
        dissapearing = true;
        reappearing = false;
        disappearFXFinished = false;
        DisableMovement();
    }
    public void Reappear()
    {
        dissapearing = false;
        reappearing = true;
        reappearFXFinished = false;
    }
    private void CheckProgressValue()
    {
        if (dissapearing)
        {
            if (progressValue <= maxDissapear)
            {
                dissapearing = false;
                disappearFXFinished = true;
            }
        }
        else if (reappearing)
        {
            if (progressValue >= maxReappear)
            {
                reappearing = false;
                reappearFXFinished = true;
            }
            else if (progressValue >= enableMvmtValue)
            {
                EnableMovement();
            }
        }
    }
    private void DisableMovement()
    {
        //stop movement, jumping, gravity and shooting
        pRB.gravityScale = 0;
        pMove.speed = 0;
        pMove.jumpHeight = 0;
        pMove.playerActive = false;
    }
    private void EnableMovement()
    {
        pRB.gravityScale = normalGravity;
        pMove.speed = normalSpeed;
        pMove.jumpHeight = normalJump;
        pMove.playerActive = true;
    }
}
