using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMotor : MonoBehaviour
{
    //Movement
    [SerializeField] TimerAndScore timerAndScore;
    [SerializeField] Image hudHoldingTrash;
    [SerializeField] Color[] trashColors;
    [SerializeField] TextMeshProUGUI GUITrashCounter;
    [SerializeField] TrashSpawn trashSpawn;
    private Rigidbody rb;
    private CharacterController cc;
    private Vector3 moveDirection;
    public float moveSpeed;
    public float boostSpeed;
    public bool isBoosted;
    public float walkSpeed = 5f;
    public float gravity = 6;
    public bool isFirstPlayer;
    [SerializeField] private Transform otherPlayer;
    [SerializeField] private float playerDistanceLimit = 30;
    [SerializeField] private GameObject trashBag;
    public int holdingTrashID;
    public int holdingTrashCount;
    [SerializeField]AudioSource throwAudio, grabAudio, wrongCanAudio;

    //Animation
    private Animator anim;

    //Rotation
    [SerializeField] private GameObject direction;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isFirstPlayer)
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    //o grab é pra ser chamado por algo, tipo um trigger, e deve ser passado o trash que ele deve pegar. 
    //Eu coloquei um limite de lixo que o jogador pode segurar e um int de ID do lixo que ele tá segurando 
    // a ideia desse ID do lixo é ser 0 para não estar segurando lixo e ser 1,2,3... para os tipos de lixo
    //para ele não poder pegar outro tipo
    //ao pegar um lixo um saco de lixo aparece (ainda não modelei e coloquei esse saco no jogo) e ao pegar mais lixo o saco cresce

    public void Grab(TrashBehaviour trash)
    {
        if (trash.trashID == holdingTrashID || holdingTrashID == 0)
        {
            anim.SetTrigger("grab");
            anim.SetBool("hold", true);
            Destroy(trash.gameObject);
            holdingTrashCount++;
            trashBag.SetActive(true);
            trashBag.transform.localScale = Vector3.one * holdingTrashCount * 0.5f;
            holdingTrashID = trash.trashID;
            hudHoldingTrash.color = trashColors[trash.trashID];
            GUITrashCounter.text = "" + holdingTrashCount;
            throwAudio.Play();
        }
        else
        {
            wrongCanAudio.Play();
        }
    }

    //o DeliverInTrashCan é para ser chamado na hora de jogar o lixo no lixo.
    //o jogador só pode jogar o lixo se o ID for o mesmo da lata de lixo, que é o ID passado para o metodo

    public void ThrowTrashInTrashCan(int trashCanID)
    {
        if(trashCanID == holdingTrashID)
        {
            holdingTrashID = 0;
            trashBag.transform.localScale = Vector3.one * 0.5f;
            trashBag.SetActive(false);
            anim.SetBool("hold", false);
            if(holdingTrashCount > 1)
            {
                timerAndScore.AddScore((holdingTrashCount * 2) + (holdingTrashCount * 10));
                timerAndScore.AddTime((holdingTrashCount * 2) + (holdingTrashCount * 7));
            }
            else
            {
                timerAndScore.AddScore(10);
                timerAndScore.AddTime(7);
            }
            holdingTrashCount = 0;
            hudHoldingTrash.color = trashColors[0];
            GUITrashCounter.text = "";
            trashSpawn.SpawnTrash();
            grabAudio.Play();
        }
        else
        {
            wrongCanAudio.Play();
        }
    }

    void Move()
    {
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection.sqrMagnitude > 1f) moveDirection = moveDirection.normalized;

        if (!cc.isGrounded)
        {
            moveDirection.y -= gravity;
        }

        if (isBoosted)
        {
            moveSpeed = boostSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if (cc.velocity != Vector3.zero & !isBoosted)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        moveDirection *= moveSpeed;

        if (Vector3.Distance(transform.position + moveDirection.normalized, otherPlayer.position) > playerDistanceLimit)
        {
            //cant go there
        }
        else
        {
            cc.Move(moveDirection * Time.deltaTime);
        }

        //rotation
        if (isFirstPlayer)
        {
            Vector3 facingRotation = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
            if (facingRotation != Vector3.zero)
            {
                direction.transform.forward = facingRotation;
            }
        }
        else
        {
            Vector3 facingRotation = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal2"), 0f, Input.GetAxis("Vertical2")));
            if (facingRotation != Vector3.zero)
            {
                direction.transform.forward = facingRotation;
            }
        }
    }
}

