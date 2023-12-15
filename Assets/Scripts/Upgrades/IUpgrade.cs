using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade
{
    void OnTriggerEnter(Collider other);

    void ApplyUpgrade(PlayerController playerController);
}
