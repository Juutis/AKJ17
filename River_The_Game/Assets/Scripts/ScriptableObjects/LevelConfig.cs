using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField]
    private WorldBackground skyBackground;
    public WorldBackground SkyBackground { get { return skyBackground; } }
    [SerializeField]
    private WorldBackground waterBackground;
    public WorldBackground WaterBackground { get { return waterBackground; } }
    [SerializeField]
    private WorldBackground waterForeground;
    public WorldBackground WaterForeground { get { return waterForeground; } }
    [SerializeField]
    private WorldBackground groundBackground;
    public WorldBackground GroundBackground { get { return groundBackground; } }
    [SerializeField]
    private AudioClip music;
    public AudioClip Music { get { return music; } }
}
