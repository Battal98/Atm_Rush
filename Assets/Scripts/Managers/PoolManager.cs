using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class ParticlePoolManager : MonoBehaviour
    {
        #region Self Variables

            #region Public Variables

            public List<PoolData> PoolData;

            #endregion


            #endregion

            private void Awake()
            {
                PoolData=GetPoolDatas();
                InitPoolObjects(PoolData);
            }
            private List<PoolData> GetPoolDatas() => Resources.Load<CD_Pool>("Data/CD_Particle").PoolData;

            private void InitPoolObjects(List<PoolData> PoolDataParam)
            {
                for (int index = 0; index < PoolData.Count; index++)
                {
                    for (int i = 0; i < PoolData[index].PoolAmount; i++)
                    {
                        GameObject particle = Instantiate(PoolData[index].ObjectType, transform.position, Quaternion.identity);
                        PoolData[index].PoolObjects.Add(particle);
                        particle.SetActive(false);
                    }
                }
                    

            }
            

            #region Event Subscription

            private void OnEnable()
            {
                SubscribeEvents();
            }

            private void SubscribeEvents()
            {
                ParticlePoolSignals.Instance.onEffectNeed += LoadParticles;
            }

            private void LoadParticles(int ParticleType,Transform EffectPositon)
            {
                
                PoolData[ParticleType].PoolObjects[0].transform.position=EffectPositon.position;
                PoolData[ParticleType].PoolObjects[0].SetActive(true);
                
            }

            #endregion

        }
    }