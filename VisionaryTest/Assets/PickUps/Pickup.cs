using UnityEngine;
using System.Collections;
using System;

public class Pickup : MonoBehaviour {

	//enemies subscribe to boost to get a boost when the boost pickup is activated
	public static Action Boost;

	public enum pickType {
		SLOW,
		GHOST,
		PIERCE,
		ENEMY_BOOST,
	}

	//this will be set in the editor
	public pickType curType;

	public void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Player> ()) {

			switch (curType) {
			case pickType.ENEMY_BOOST:
				//all enemies are sped up
				if (Boost != null) {
					Boost ();
				}
				break;
			case pickType.GHOST:
				//the player becomes invincible
				Player.player.BeginGhost();
				break;
			case pickType.PIERCE:
				//the next 10 bullets pierce 3 targets
				Player.player.piercingBullets = 10;
				break;
			case pickType.SLOW:
				//slows the player back down to original speeds
				Player.player.RevertSpeed ();
				break;
			default:
				break;
			}

			//drestroy the pickup
			Destroy (gameObject);

		}
	}
}
