using UnityEngine;
using System.Collections;

public class Synchonizer : Photon.MonoBehaviour 
{
	private Vector3 receivePosition = Vector3.zero;
	private Quaternion receiveRotation = Quaternion.identity;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} 
		else 
		{
			receivePosition = (Vector3)stream.ReceiveNext();
			receiveRotation = (Quaternion)stream.ReceiveNext();
		}
	}


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!photonView.isMine) 
		{
			float dis = Vector3.SqrMagnitude(receivePosition-transform.position);
			if(dis < 1f)
			{
				transform.position = Vector3.Lerp(transform.position, receivePosition, Time.deltaTime*10);
			}
			else
			{
				transform.position = receivePosition;
			}
			transform.rotation = Quaternion.Lerp (transform.rotation, receiveRotation, Time.deltaTime*10);
		}
	}
}
