using Config;
using System.Collections.Generic;
using UI.Contexts;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent()]
    public class UIController : MonoBehaviour
    {
        private Context currentContext;
        private readonly Dictionary<ContextIdentifier, Context> spawnedContext = 
            new Dictionary<ContextIdentifier, Context>();

        public HUDContext GetHUD() => spawnedContext[ContextIdentifier.HUD] as HUDContext;
        public DeathContext GetDeathContext() => spawnedContext[ContextIdentifier.Death] as DeathContext;

        public void Initialize()
        {
            var uiConfig = Configuration.GetConfig<UIConfig>();
            foreach (var val in uiConfig.ContextsDictionary)
            {
                var context = Instantiate(val.Value, transform);
                context.Close();
                context.Initalize(this);
                spawnedContext.Add(val.Key, context);
            }
        }

        public void Open(ContextIdentifier identifier)
        {
            if(currentContext != null) 
                currentContext.Close();

            if (spawnedContext.TryGetValue(identifier, out var nextContext))
            {
                currentContext = nextContext;
                if (currentContext != null) currentContext.Open();
                else Debug.LogError($"Context is null: {identifier}");
            }
            else Debug.LogError($"No context for type: {identifier}");
        }

        public void Close()
        {
            if (currentContext != null)
            {
                currentContext.Close();
            }
        }

        public void Enable()
        {
            if (currentContext != null)
            {
                currentContext.gameObject.SetActive(true);
            }
        }

        public void Disable()
        {
            if (currentContext != null)
            {
                currentContext.gameObject.SetActive(false);
            }
        }
    }
}
