﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public float width = 10f;
	public float height = 5f;
	private bool movingRight = false;
	public GameObject enemyPrefab;
	public float speed = 5f;
	
	private float xmax;
	private float xmin;
	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xmin = leftBoundary.x;
		xmax = rightBoundary.x;
		
		SpawnEnemies();
	}
	
	void SpawnEnemies() {
		foreach( Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}	
	}
	
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightEdgeOfFormation = transform.position.x + (width * 0.5f);
		float leftEdgeOfFormation = transform.position.x - (width * 0.5f);
		
		if ( leftEdgeOfFormation < xmin) { 
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}
		
		if(AllMembersDead()) {
			Debug.Log("Empty Formation");
			SpawnEnemies();
			
		}
	}
	
	bool AllMembersDead() {
		foreach(Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
