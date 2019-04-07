//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TrainFollow : MonoBehaviour
//{
//    [HideInInspector] public Transform followTransform;
//    [SerializeField] private int queueMaxCount = 50;

//    private LineRenderer lineRender;

//    public Queue<HeadTransform> queue = new Queue<HeadTransform>();

//    private void Start()
//    {
//        lineRender = GetComponent<LineRenderer>();
//    }

//    private void Update()
//    {
//        if (followTransform != null)
//        {
//            lineRender.SetPosition(0,transform.position + transform.forward * transform.lossyScale.z / 2);
//            lineRender.SetPosition(1, followTransform.position - followTransform.forward * followTransform.lossyScale.z / 2);
//        }
//        else
//        {
//            lineRender.enabled = false;
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (followTransform != null)
//        {

//            HeadTransform headTransform = new HeadTransform();
//            headTransform.rotation.eulerAngles = new Vector3(followTransform.eulerAngles.x, followTransform.eulerAngles.y, followTransform.GetChild(0).eulerAngles.z);
//            headTransform.position = followTransform.position;

//            queue.Enqueue(headTransform);


//            if (queue.Count >= queueMaxCount)
//            {
//                HeadTransform target = queue.Dequeue();

//                transform.position = target.position;
//                transform.eulerAngles = new Vector3(0, target.rotation.eulerAngles.y, 0);

//                transform.GetChild(0).transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, target.rotation.eulerAngles.z);
//            }
//        }
//        else
//        {

//            if (queue.Count > 0)
//            {
//                HeadTransform target = queue.Dequeue();

//                transform.position = target.position;
//                transform.eulerAngles = new Vector3(0, target.rotation.eulerAngles.y, 0);

//                transform.GetChild(0).transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, target.rotation.eulerAngles.z);
//            }
//            else
//            {
//                Destroy(gameObject);
//            }
//        }

//    }


//}



//public class HeadTransform
//{
//    public Vector3 position;
//    public Quaternion rotation;
//}


