using System.Globalization;

namespace ResumMake.Business.Objects
{
    public class Settings
    {
        private int _maximumAge;
        private int _minimumAge;

        public int MaximumAge
        { 
            get 
            {
                if (_maximumAge > 100) return 35;
                return _maximumAge; 
            }
        }        

        public int MinimumAge
        {
            get 
            {
                if (_minimumAge <= 12) return 21;
                return _minimumAge; 
            }
            set { _minimumAge = value; }
        }

        public RegionInfo TargetRegion
        {
            get; set;
        }

        public Settings(int minAge = 18, int maxAge = 60, string regionName = "AU")
        {
            _minimumAge = minAge;
            _maximumAge = maxAge;
            TargetRegion = new RegionInfo(regionName);
        }
    }
}
