using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Controllers
{
    public class StackDecreaseController : MonoBehaviour
    {
        #region Decrease Jobs

        [SerializeField] private GameObject  collectables;

        public void StackHitTheObstacleDecrease(GameObject _obj, List<GameObject> _stackList)
        {

            if (!collectables)
            {
                collectables = GameObject.FindGameObjectWithTag("CollectableParent");
            }

            int index = _stackList.IndexOf(_obj);
           
            if (index != _stackList.Count - 1)
            {
                ThrowRandomObj( _stackList, index);
                
            }
            else
            {
                _stackList.Remove(_obj);
                Destroy(_obj);
                _stackList.TrimExcess();
            }
        }

        public void StackGeneralDecrease(GameObject _obj, List<GameObject> _stackList, Transform _targetParent)
        {
            if (!collectables)
            {
                collectables = GameObject.FindGameObjectWithTag("CollectableParent");
            }

            int index = _stackList.IndexOf(_obj);
            if (index != _stackList.Count - 1)
            {
                ThrowRandomObj(_stackList, index);
            }
            else
            {
                _obj.transform.parent = _targetParent;
                _stackList.Remove(_obj);
            }

            //_stackList.TrimExcess();
        }

        public void ThrowRandomObj( List<GameObject> _stackList,int index)
        {
            for (int i = index; i < _stackList.Count-1; i++)
            {   

                if (index != -1)
                {
                    GameObject collectableInStack= _stackList[index];
                                            
                    float xRandom = Random.Range(-3, 3);
                    float zRandom = Random.Range(collectableInStack.transform.position.z + 8f, collectableInStack.transform.position.z + 16f);
                    collectableInStack.GetComponentInChildren<Collider>().enabled = false;
                    collectableInStack.GetComponentInChildren<CollectablePhysicsController>().isITaken = false;
                    
                    collectableInStack.transform.DOJump(new Vector3(xRandom, 0.65f, zRandom), 1f, 2, 0.6f, snapping: false).OnComplete(() => {

                        collectableInStack.GetComponentInChildren<Collider>().enabled = true;
                        
                        
                        collectableInStack.transform.parent = collectables.transform;

                    });
                    _stackList.Remove(collectableInStack);

                }
            }
            _stackList.TrimExcess();
        }
        #endregion
    }

}