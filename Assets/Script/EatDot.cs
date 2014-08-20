using UnityEngine;
using System.Collections;

public class EatDot : Photon.MonoBehaviour
{
	GameObject gameManager;

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
	}

	public void Eaten()
	{
		PhotonView gameManagerPV = gameManager.GetComponent<PhotonView> ();
		gameManagerPV.RPC ("IncreaseDotCount", PhotonTargets.All);
		if (photonView.isMine)
		{
			PhotonNetwork.Destroy (this.gameObject);
		}
	}
}
