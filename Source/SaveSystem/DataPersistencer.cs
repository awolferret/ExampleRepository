using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace SaveSystem
{
    public class DataPersistencer : MonoBehaviour
    {
        [SerializeField] private float _saveDelay;

        private List<IDataPersistence> _dataPersistanceOdjects;

        private void Awake()
        {
            _dataPersistanceOdjects = FindAllDataPersistenceObjects();
            StartCoroutine(SaveTimer());
            LoadGame();
        }

        private void LoadGame()
        {
            foreach (IDataPersistence dataPersistenceObject in _dataPersistanceOdjects)
            {
                dataPersistenceObject.Load();
            }
        }

        private void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObject in _dataPersistanceOdjects)
            {
                dataPersistenceObject.Save();
            }
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }

        private IEnumerator SaveTimer()
        {
            yield return new WaitForSeconds(_saveDelay);
            SaveGame();
            StartCoroutine(SaveTimer());
        }
    }
}