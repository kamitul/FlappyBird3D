using System.Collections.Generic;
using System.Linq;
using UI.Contexts;
using UI.Utils;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(UIConfig), menuName = "UI/Config")]
    public sealed class UIConfig : ScriptableObject
    {
        [field: SerializeField] public List<Context> Contexts { get; private set; }
        [field: SerializeField] public Toast Toast { get; private set; }
        public Dictionary<ContextIdentifier, Context> ContextsDictionary => Contexts.ToDictionary(x => x.Identifier, y => y);
    }
}
