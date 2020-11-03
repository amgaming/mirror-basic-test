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
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(int clipSoundId){
        if(clipSoundId >= 0 && clipSoundId < clips.Length){
            CmdServerPlaySoundId(clipSoundId);
        }   
    }

    public void StopSound(){
        CmdServerStopSoundId();
    }

    [Command]
    void CmdServerPlaySoundId(int clipSoundId){
        RpcSendPlaySoundIdToClients(clipSoundId);
    }

    [ClientRpc]
    void RpcSendPlaySoundIdToClients(int clipSoundId){
        source.PlayOneShot(clips[clipSoundId]);
    }

    [Command]
    void CmdServerStopSoundId(){
        RpcSendStopSoundIdToClients();
    }

    [ClientRpc]
    void RpcSendStopSoundIdToClients(){
        if(source.isPlaying){
            source.Stop();
        }
    }
}
