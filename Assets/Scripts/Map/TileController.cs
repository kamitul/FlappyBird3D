using Spawner;

namespace Map
{
    public sealed class TileController
    {
        private readonly Payload payload;

        public TileController(Payload payload)
        {
            this.payload = payload;
        }

        public void Update()
        {
            if (payload == null) return;

            for(int i = 0; i < payload.MapController.SpawnedTiles.Count; ++i)
            {
                payload.MapController.SpawnedTiles[i].HandleVisibility(payload.PlayerController.transform);
            }
        }

        public class Payload
        {
            public Payload(Player.PlayerController playerController, MapController mapController)
            {
                PlayerController = playerController;
                MapController = mapController;
            }

            public Player.PlayerController PlayerController { get; private set; }
            public MapController MapController { get; private set; }
        }
    }
}
