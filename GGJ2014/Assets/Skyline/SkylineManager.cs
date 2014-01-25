using UnityEngine;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {
	public Transform prefab;
	public int numberOfObjects;
	public Vector3 startPosition;
	public float recycleOffset;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	int count = 0;
	
	void Start () {
		objectQueue = new Queue<Transform> (numberOfObjects);
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Transform o = (Transform) Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.x += o.localScale.x;
			objectQueue.Enqueue(o);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
		//if(Runner.distanceTraveled > prefab.localScale.x){
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.x += prefab.localScale.x*2;

			if (count%2 == 0){
				o.transform.localScale += new Vector3(-0,0,0);
			}
//			count++;
//			Runner.resetDistance() == 0;

			objectQueue.Enqueue(o);
		}
	}
}
