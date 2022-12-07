using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletSpawner : NetworkBehaviour
{
    public Rigidbody bullet;

    private float bulletSpeed = 20f;

    public NetworkVariable<int> netBulletDamage = new NetworkVariable<int>(1);

    int maxDmg = 20;

    [ServerRpc]
    public void FireServerRpc(ServerRpcParams rpcParams = default)
    {
        Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation);

        newBullet.velocity = transform.forward * bulletSpeed;
        newBullet.gameObject.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
        newBullet.GetComponent<BulletClass>().netDamage.Value = netBulletDamage.Value;

        Destroy(newBullet.gameObject, 3);
    }

    public void IncreaseDamage()
    {
        if(netBulletDamage.Value == 1)
        {
            netBulletDamage.Value = 5;
        }
        else
        {
            netBulletDamage.Value += 5;
        }

        if(netBulletDamage.Value > maxDmg)
        {
            netBulletDamage.Value = maxDmg; //29:51
        }
    }

    public bool IsAtMaxDmg()
    {
        return netBulletDamage.Value == maxDmg;
    }
}
