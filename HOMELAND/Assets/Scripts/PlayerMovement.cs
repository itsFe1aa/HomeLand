using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;

    public float groundDrag;
    [Header("Ground Checker")]
    public float PlayerHeight;
    public LayerMask TheGround;
    bool IsGrounded;

    public List<GameObject> enemyList;
    public GameObject CurrentTarget;
    public Transform StartPoints;
    public Transform MiddlePoint;


    public bool StartThrow;

    public float CurrentThrowLerpTime;
    public float MaxThrowLerpTime = 0.7f;

    public GameObject RockPrefap;
    public GameObject Rock;

    public int RockCount;
    public TMP_Text RockUIBox;

    public int enemyKillCount;
    //public float JumpForce;
    //public float JumpCoolDown;
    //public float AirMultiplier;
    //bool GoingToJump;

    //[Header("Keybinds")]
    //public KeyCode jumpKey = KeyCode.Space;

    public Transform orientation; 

    float HoriInput;
    float VertiInput;
    Vector3 MoveDireciton;

    Rigidbody Rbody;

    private void Start()
    {
        Rbody = GetComponent<Rigidbody>();
        Rbody.freezeRotation = true;

        enemyList = new List<GameObject>();

        StartThrow = false;
        CurrentThrowLerpTime = 0;

        RockCount = 0;
        enemyKillCount = 0; 
    }
    private void Update()
    {
        //ground checker
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, TheGround);


        if (StartThrow == true)
        {
            DoLerpProcess(CurrentTarget);

        }
        else 
        {
            Inputs();
            SpeedLimit();
        }
       

        // drag handler 
        if (IsGrounded)
        {
            Rbody.drag = groundDrag;
        }
        else
        {
            Rbody.drag = 0;
        }

       RockUIBox.text = RockCount.ToString();

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void Inputs()
    {
        HoriInput = Input.GetAxisRaw("Horizontal");
        VertiInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enemyList.Count > 0 && RockCount > 0)
            {
                CurrentTarget = enemyList[0];
                enemyList.RemoveAt(0);
                CurrentThrowLerpTime = 0;
                Rock = Instantiate(RockPrefap);
                Rock.transform.position = transform.position;
                RockCount--;
                StartThrow = true;

            }
            else 
            {
                Debug.Log("I dont have any rocks");
            }
        }
        

        //if (Input.GetKey(jumpKey) && GoingToJump && IsGrounded)
        //{
        //    GoingToJump = false;
        //    Jump();
        //    Invoke(nameof(ResetJump), JumpCoolDown);
        //}
    }

    private void PlayerMove()
    {
        // calc Direction
        MoveDireciton = orientation.forward * VertiInput + orientation.right * HoriInput;
        ////on ground
        //if (IsGrounded)
        //{
               Rbody.AddForce(MoveDireciton.normalized * Speed * 10f, ForceMode.Force);
        //}
        //else if (!IsGrounded)
        //{
        //    Rbody.AddForce(MoveDireciton.normalized * Speed * 10f * AirMultiplier, ForceMode.Force);
        //}
    }

    private void SpeedLimit()
    {
        Vector3 flatVelocity = new Vector3(Rbody.velocity.x, 0f, Rbody.velocity.z);

        if (flatVelocity.magnitude > Speed)
        { 
            Vector3 LimitVel = flatVelocity.normalized * Speed;
            Rbody.velocity = new Vector3(LimitVel.x, Rbody.velocity.y, LimitVel.z);
        
        }
    }

    public void DoLerpProcess(GameObject target)
    {
        float t = CurrentThrowLerpTime / MaxThrowLerpTime;
        if (t > 1)
        {
            t = 1;
        }

        Rock.transform.position = QuadraticBezierCurves(StartPoints.position, MiddlePoint.position, target.transform.position, t);

        if (t == 1)
        {
            Destroy(Rock);
            CurrentThrowLerpTime = 0;
            StartThrow = false;
            Destroy(CurrentTarget);
            enemyKillCount++;
        }
        else 
        {
            CurrentThrowLerpTime += Time.deltaTime;
            if (CurrentThrowLerpTime > MaxThrowLerpTime)
            {
                CurrentThrowLerpTime = MaxThrowLerpTime;
            }
        }
    }

    public  Vector3 QuadraticBezierCurves(Vector3 start, Vector3 middle, Vector3 end, float t)
    {
        //P = (1 - t)^2 * p0 + 2(1 - t) * tp1 + t^2 * p2

        Vector3 result;

        Vector3 p0 = start;
        Vector3 p1 = middle;
        Vector3 p2 = end;


        //(1 - t)^2 * p0
        float u = 1 - t;
        float uu = u * u;
        Vector3 uup0 = uu * p0;

        //2(1 - t) * tp1
        float u2 = 2 * u;
        Vector3 tp1 = t * p1;
        Vector3 u2tp1 = u2 * tp1;

        //t^2 * p2
        float tt = t * t;
        Vector3 ttp2 = tt * p2;

        result = uup0 + u2tp1 + ttp2;

        return result;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Chest"))
        {
            if (enemyKillCount == 4)
            {
                SceneManager.LoadScene(5); 
            }
        }
    }














    //private void Jump()
    //{
    //    // reset y 
    //    Rbody.velocity = new Vector3(Rbody.velocity.x, 0f, Rbody.velocity.z);

    //    Rbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    //}

    //private void ResetJump()
    //{
    //    GoingToJump = true;
    //}






}// class end
