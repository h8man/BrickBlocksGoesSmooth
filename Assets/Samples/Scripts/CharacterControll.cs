using Assets.Extensions;
using Camera2D.CameraExtensions;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public FollowTarget atFolow;
    new public Rigidbody2D rigidbody;
    public bool fixedUpdate;
    public bool updateTarget;
    public float speed;
    private Vector2 direction;
    public bool step;

    // Start is called before the first frame update

    private void FixedUpdate()
    {
        if (fixedUpdate)
        {
            rigidbody.MovePosition(Move(rigidbody.position, Time.fixedDeltaTime));
        }

    }
    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!fixedUpdate)
        {
            transform.position = Move(transform.position, Time.deltaTime);
        }

        if(updateTarget)atFolow?.UpdateTarget(transform.position, direction);
    }

    Vector2 Move(Vector2 position, float deltaTime)
    {
        Vector2 res = rigidbody.position + direction * speed * Time.fixedDeltaTime;
        if (step)
        {
            res = res.ToStep(32);
        }
        return res;
    }
}
