using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int Speed = 5;
    public bool Enabled = true;

    public EntityStats EntityStats { get; set; }

    private float AppliedSpeed => (float)this.EntityStats.GetAppliedValueForTag(this.Speed, AbilityTag.MovementSpeed);

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Enabled)
            return;

        var xDir = 0f;
        var yDir = 0f;

        if(Input.GetKey(KeyCode.W)) yDir = 1f;
        if(Input.GetKey(KeyCode.S)) yDir = -1f;
        if(Input.GetKey(KeyCode.D)) xDir = 1f;
        if(Input.GetKey(KeyCode.A)) xDir = -1f;

        var movement = new Vector3(xDir, yDir).normalized;
        var appliedSpeed = this.AppliedSpeed;
        transform.position += movement * appliedSpeed * Time.deltaTime;
    }
}