using System;

namespace Loader
{
    public struct LoaderData
    {
        public float Progress;

        public LoaderData(float progress)
        {
            Progress = progress;
        }

        public void Update(float value)
        {
            Progress = value;
        }
    }
}
