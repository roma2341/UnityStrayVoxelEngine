using UnityEngine;
using System.Collections;

public abstract class AbstractWorldDataHandler
{
    protected WorldConfig config;
    public AbstractWorldDataHandler(WorldConfig worldConfig)
    {
        this.config = worldConfig;
    }
    public abstract void handle(WorldData worldData);
}
