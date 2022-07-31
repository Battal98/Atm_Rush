using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Controllers
{
    public class StackDecreaseController : MonoBehaviour
    {
        #region Decrease Jobs

        [SerializeField]GameObject  collectables;
        public void StackHitTheObstacleDecrease(GameObject _obj, List<GameObject> _stackList)
        {   
            int index = _stackList.IndexOf(_obj);
           
            if (index != _stackList.Count - 1)
            {
                
                

                
                ThrowRandomObj( _stackList, index);
                
            }
            else
            {
               // Debug.Log("Count");
                _stackList.Remove(_obj);
                //_stackList[_obj].GetComponentInChildren<CollectablePhysicsController>().isITaken = false;
                Destroy(_obj);
                _stackList.TrimExcess();
                
                //_obj.SetActive(false);
            }

            
        }

        public void StackGeneralDecrease(GameObject _obj, List<GameObject> _stackList, Transform _targetParent)
        {
            _obj.transform.parent = _targetParent;
            _stackList.Remove(_obj);
            //_stackList.TrimExcess();
        }

        public void ThrowRandomObj( List<GameObject> _stackList,int index)
        {
            //3 4   
            // 4 5 6 7 8 9
            //5 6 7 8 9
            //6    7   8     9
            for (int i = index; i < _stackList.Count-1; i++)
            {   

                if (index != -1)
                {

                    Debug.Log(index);
    
                    GameObject ddd= _stackList[index];
                                            
                    float xRandom = Random.Range(-3, 3);
                    float zRandom = Random.Range(ddd.transform.position.z + 8f, ddd.transform.position.z + 16f);
                    ddd.GetComponentInChildren<Collider>().enabled = false;
                    ddd.GetComponentInChildren<CollectablePhysicsController>().isITaken = false;
                    
                    ddd.transform.DOJump(new Vector3(xRandom, 0.65f, zRandom), 1f, 2, 0.6f, snapping: false).OnComplete(() => {

                        ddd.GetComponentInChildren<Collider>().enabled = true;
                        
                        
                        ddd.transform.parent = collectables.transform;


                    });
                    _stackList.Remove(ddd);




                }

            }
            _stackList.TrimExcess();
        }
        #endregion
    }

}