using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelImportSettings : AssetPostprocessor {
    void OnPreprocessModel() {
        ModelImporter modelImporter = (ModelImporter)assetImporter;

        modelImporter.importMaterials = false;

        modelImporter.animationType = ModelImporterAnimationType.None;


    }
}
