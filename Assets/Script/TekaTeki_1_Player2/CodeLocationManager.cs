using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeLocationManager : MonoBehaviour
{
    [SerializeField] CodeConfig codeConfig;
    [SerializeField] List<Transform> codeLocations; // Daftar posisi kode di scene
    [SerializeField] GameObject codeDisplayPrefab; // Prefab untuk menampilkan kode (misalnya teks 3D)
    [SerializeField] Transform parentCode;

    GameObject currentCodeDisplay;
    Transform currentLocation;

    void OnEnable()
    {
        GameEventSystem.Instance.OnCodeGenerated += PlaceCodeInRandomLocation;
    }

    void OnDisable()
    {
        GameEventSystem.Instance.OnCodeGenerated -= PlaceCodeInRandomLocation;
    }

    void PlaceCodeInRandomLocation(string code)
    {
        // Hapus kode sebelumnya jika ada
        if (currentCodeDisplay != null)
            Destroy(currentCodeDisplay);

        // Pilih lokasi acak
        currentLocation = codeLocations[Random.Range(0, codeLocations.Count)];
        
        // Instansiasi prefab untuk menampilkan kode
        currentCodeDisplay = Instantiate(codeDisplayPrefab, currentLocation.position, currentLocation.rotation);
        currentCodeDisplay.transform.SetParent(parentCode);
        var textComponent = currentCodeDisplay.GetComponentInChildren<TextMeshPro>();
        if (textComponent != null)
            textComponent.text = code;
    }
}
