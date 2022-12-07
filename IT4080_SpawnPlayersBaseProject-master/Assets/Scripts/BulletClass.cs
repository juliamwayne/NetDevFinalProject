using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BulletClass : NetworkBehaviour
{
    public NetworkVariable<int> netDamage = new NetworkVariable<int>(1);
}
