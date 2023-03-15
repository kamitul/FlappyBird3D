namespace Services.Saves
{
    public sealed class SaveKey
    {
        public SaveKey(string key)
        {
            Key = key;
        }

        public string Key { get; private set; }
    }
}
