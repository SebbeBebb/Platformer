using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float jumpForce = 300;

    [SerializeField]
    Transform feet;

    [SerializeField]
    LayerMask groundLayer;

    Rigidbody2D rbody;

    bool hasReleasedJumpButton = false;

    [SerializeField]
    float groundRadius = 0.2f;

    void Awake() {
        rbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(Vector2.zero, Vector2.down * 8, Color.green);

    float moveX = Input.GetAxisRaw("Horizontal");

    Vector2 movement = new Vector2(moveX, 0);
    movement = movement.normalized * speed * Time.deltaTime;

    transform.Translate(movement);

    bool isGrounded = Physics2D.OverlapBox(getFootPosition(), GetFootSize(), 0, groundLayer);

    Debug.Log(isGrounded);

    if (Input.GetAxisRaw("Jump") > 0 && hasReleasedJumpButton == true && isGrounded){
        Debug.Log("JUMP!");
        rbody.AddForce(Vector2.up * jumpForce);
        hasReleasedJumpButton = false;
    }

    if (Input.GetAxisRaw("Jump") == 0){
        hasReleasedJumpButton = true;
    }
    }



    private Vector2 getFootPosition(){
        float height = GetComponent<Collider2D>().bounds.size.y;
        return transform.position + Vector3.down * height/2;
    }

    private Vector2 GetFootSize(){
        return new Vector2(GetComponent<Collider2D>().bounds.size.x * 0.9f, 0.1f);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(getFootPosition(), GetFootSize());
        //Gizmos.DrawSphere(Vector2.zero, 3);
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(getFootPosition(), groundRadius);
    }
}
