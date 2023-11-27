using UnityEditor;

[CustomEditor(typeof(ObstacleSpawner))]
public class ObstacleSpawnerEditor : Editor
{
    private SerializedProperty obstacles;
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
        obstacles = serializedObject.FindProperty("obstacles");
        room = serializedObject.FindProperty("room");
        door = serializedObject.FindProperty("door");
        doorPositionOffsetProp = serializedObject.FindProperty("doorPositionOffset");
        spawnRequirementProp = serializedObject.FindProperty("spawnRequirement");
        startingSpawnDelayProp = serializedObject.FindProperty("startingSpawnDelay");
        spawnRateProp = serializedObject.FindProperty("spawnRate");
        spawnPositionOffsetProp = serializedObject.FindProperty("spawnPositionOffset");
        colliderSizeProp = serializedObject.FindProperty("colliderSize");
        colliderPositionOffSetProp = serializedObject.FindProperty("colliderPositionOffSet");
        spawnOnHitProp = serializedObject.FindProperty("spawnOnHit");
    }

    public override void OnInspectorGUI()
    {
        ObstacleSpawner script = (ObstacleSpawner)target;
        serializedObject.Update();

        // Draw default inspector property fields
        SpawnRequirement spawnRequirement = (SpawnRequirement)spawnRequirementProp.enumValueIndex;
        EditorGUILayout.PropertyField(obstacles);
        EditorGUILayout.PropertyField(room);
        EditorGUILayout.PropertyField(door);
        
        if (door.objectReferenceValue != null)
            EditorGUILayout.PropertyField(doorPositionOffsetProp);

        // Check if any element in the array is assigned
        bool anyObjectAssigned = false;

        for (int i = 0; i < obstacles.arraySize; i++)
        {
            SerializedProperty arrayElement = obstacles.GetArrayElementAtIndex(i);

            if (arrayElement.objectReferenceValue != null)
            {
                anyObjectAssigned = true;
                break;
            }
        }

        if (anyObjectAssigned == true && room.objectReferenceValue != null && door.objectReferenceValue != null)
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
