using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    private Terrain _terrain;

    public void PaintTextureArea(Vector3 worldPosition, int textureIndex, float radius, float opacity)
    {
        TerrainData terrainData = _terrain.terrainData;
        Vector3 terrainPos = _terrain.transform.position;

        float normalizedX = (worldPosition.x - terrainPos.x) / terrainData.size.x;
        float normalizedZ = (worldPosition.z - terrainPos.z) / terrainData.size.z;

        if (normalizedX < 0 || normalizedX > 1 || normalizedZ < 0 || normalizedZ > 1)
        {
            normalizedX = Mathf.Clamp01(normalizedX);
            normalizedZ = Mathf.Clamp01(normalizedZ);
        }

        int mapX = (int)(normalizedX * terrainData.alphamapWidth);
        int mapZ = (int)(normalizedZ * terrainData.alphamapHeight);

        int size = (int)(radius * terrainData.alphamapWidth / terrainData.size.x);
        size = Mathf.Clamp(size, 1, 50);

        int startX = Mathf.Clamp(mapX - size, 0, terrainData.alphamapWidth - 1);
        int startZ = Mathf.Clamp(mapZ - size, 0, terrainData.alphamapHeight - 1);
        int width = Mathf.Clamp(size * 2, 1, terrainData.alphamapWidth - startX);
        int height = Mathf.Clamp(size * 2, 1, terrainData.alphamapHeight - startZ);

        float[,,] alphamap = terrainData.GetAlphamaps(startX, startZ, width, height);

        int pixelsPainted = 0;
        float maxDistance = Mathf.Min(width, height) / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float centerX = width / 2f;
                float centerZ = height / 2f;
                float distance = Vector2.Distance(new Vector2(x, z), new Vector2(centerX, centerZ));

                if (distance <= maxDistance)
                {
                    for (int i = 0; i < terrainData.alphamapLayers; i++)
                    {
                        alphamap[z, x, i] = (i == textureIndex) ? opacity : 0f;
                    }
                    pixelsPainted++;
                }
            }
        }

        terrainData.SetAlphamaps(startX, startZ, alphamap);
    }
}
