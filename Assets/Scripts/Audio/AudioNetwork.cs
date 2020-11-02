using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(AudioSource))]
public class AudioNetwork : NetworkBehaviour
{

    private AudioSource source;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(int clipSoundId){
        if(clipSoundId >= -1 && clipSoundId < clips.Length){
            CmdSendServerSoundId(clipSoundId);
        }   
    }

    [Command]
    void CmdSendServerSoundId(int clipSoundId){
        RpcSendSoundIdToClients(clipSoundId);
    }

    [ClientRpc]
    void RpcSendSoundIdToClients(int clipSoundId){
        if(clipSoundId == -1){
            source.Stop();
        }
        else
        {
            source.PlayOneShot(clips[clipSoundId]);
        }
    }

}
