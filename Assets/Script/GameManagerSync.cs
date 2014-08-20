using UnityEngine;
using System.Collections;

public class GameManagerSync : Photon.MonoBehaviour 
{
	GameManager gameManager;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Debug.Log (PhotonNetwork.isMasterClient);
			if (PhotonNetwork.isMasterClient)
			{
				stream.SendNext(gameManager.isPlayGame);
				Debug.Log ("同期開始");
			}
		}
		else
		{
			if (!PhotonNetwork.isMasterClient)
			{
				gameManager.isPlayGame = (bool)stream.ReceiveNext();
				Debug.Log ("同期終了");
			}
			Debug.Log (gameManager.isPlayGame);
		}
	}

	// Use this for initialization
	void Awake () 
	{
		gameManager = this.gameObject.GetComponent<GameManager> ();
		Debug.Log (gameManager);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
