
namespace Services.Saves
{
    public static class SaveConfig
    {
        public const int MAX_SAVED_SCORES = 20;
        public static SaveKey SCORE_KEY => new SaveKey("HIGHSCORES_KEY");
    }
}
