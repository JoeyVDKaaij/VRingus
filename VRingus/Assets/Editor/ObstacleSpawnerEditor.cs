using UnityEditor;

[CustomEditor(typeof(ObstacleSpawner))]
public class ObstacleSpawnerEditor : Editor
{
    private SerializedProperty room;
    private SerializedProperty door;
    private SerializedProperty doorPositionOffsetProp;
    private SerializedProperty spawnRequirementProp;
    private SerializedProperty startingSpawnDelayProp;
    private SerializedProperty spawnRateProp;
    private SerializedProperty spawnPositionOffsetProp;
    private SerializedProperty colliderSizeProp;
    private SerializedProperty colliderPositionOffSetProp;
    private SerializedProperty spawnOnHitProp;

    private void OnEnable()
    {
        room = serializedObject.FindProperty("room");
        door = serializedObject.FindProperty("door");
        doorPositionOffsetProp = serializedObject.FindProperty("doorPositionOffset");
        spawnRequirementProp = serializedObject.FindProperty("spawnRequirement");
        startingSpawnDelayProp = serializedObject.FindProperty("startingSpawnDelay");
        spawnRateProp = serializedObject.FindProperty("spawnRate");
        spawnPositionOffsetProp = serializedObject.FindProperty("spawnPositionOffset");
        colliderSizeProp = serializedObject.FindProperty("colliderSize");
        colliderPositionOffSetProp = serializedObject.FindProperty("colliderPositionOffSet");
        spawnOnHitProp = serializedObject.FindProperty("spawnOnEnter");
    }

    public override void OnInspectorGUI()
    {
        ObstacleSpawner script = (ObstacleSpawner)target;
        serializedObject.Update();

        // Draw default inspector property fields
        SpawnRequirement spawnRequirement = (SpawnRequirement)spawnRequirementProp.enumValueIndex;
        EditorGUILayout.PropertyField(room);

        bool anyObjectAssigned = false;

        for (int i = 0; i < room.arraySize; i++)
        {
            SerializedProperty arrayElement = room.GetArrayElementAtIndex(i);

            if (arrayElement.objectReferenceValue != null)
            {
                anyObjectAssigned = true;
                break;
            }
        }

        if (anyObjectAssigned)
        {
            EditorGUILayout.PropertyField(spawnPositionOffsetProp);
            EditorGUILayout.PropertyField(spawnRequirementProp);
            if (spawnRequirement == SpawnRequirement.Timed)
            {
                EditorGUILayout.PropertyField(startingSpawnDelayProp);
                EditorGUILayout.PropertyField(spawnRateProp);
            }
            else if (spawnRequirement == SpawnRequirement.Collision)
            {
                EditorGUILayout.PropertyField(colliderSizeProp);
                if (colliderSizeProp.objectReferenceValue != null)
                {
                    EditorGUILayout.PropertyField(colliderPositionOffSetProp);
                }
                EditorGUILayout.PropertyField(spawnOnHitProp);
            }
        }


        serializedObject.ApplyModifiedProperties();

    }
}
