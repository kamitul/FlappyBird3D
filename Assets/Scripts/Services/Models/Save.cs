using Newtonsoft.Json;

namespace Services.Models
{
    public sealed class SaveEntity
    {
        public SaveEntity(string date, float score)
        {
            Date = date;
            Score = score;
        }

        [JsonProperty("date")] public string Date { get; private set; }
        [JsonProperty("score")] public float Score { get; private set; }
    }
}
