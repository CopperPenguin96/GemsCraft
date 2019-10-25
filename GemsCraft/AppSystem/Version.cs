using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GemsCraft.Utils.StringUtil;

namespace GemsCraft.AppSystem
{
    public class Version
    {
        public static Version LatestStable = new Version("Alpha", 0, 0, -1, -1);

        private const string CheckURL = "https://www.gemscraft.net/version.html";

        public static Version CheckLatest()
        {
            string rsp = Network.Tools.GetPageTitle(CheckURL);
            string[] separated = rsp.Split("\\".ToCharArray()[0]);
            string name = separated[1];
            int major = int.Parse(separated[2]);
            int minor = int.Parse(separated[3]);
            int rev = int.Parse(separated[4]);
            int build = int.Parse(separated[5]);
            return new Version(name, major, minor, rev, build);
        }

        public string Name;
        public int Major;
        public int Minor;
        public int Revision;
        public int Build;

        public Version(string name, int major, int minor, int rev, int build)
        {
            Name = name;
            Major = major;
            Minor = minor;
            Revision = rev;
            Build = build;
        }

        /// <summary>
        /// Prints back the specified version object
        /// </summary>
        /// <returns>In the simple version representation {Name} x.x.x.x</returns>
        /// <param name="showName">Whether or not to print the name i.e. Alpha, Beta, Cobblestone</param>
        public string ToString(bool showName)
        {
            string finalStr = "";
            if (showName)
            {
                finalStr += Name + " ";
            }

            finalStr += Major + "." + Minor;

            if (Revision > -1)
            {
                finalStr += "." + Revision;
            }

            if (Build > -1)
            {
                finalStr += "." + Build;
            }

            return finalStr;
        }

        /// <summary>
        /// Prints back the specified version object with the name attached
        /// </summary>
        /// <returns>In the simple version representation {Name} x.x.x.x</returns>
        public override string ToString()
        {
            return ToString(true);
        }

        /// <summary>
        /// Determines which version is newer or if they are the same
        /// </summary>
        /// <param name="v1">The first version</param>
        /// <param name="v2">The second version</param>
        /// <returns>Returns 0 if the same, 1 if v1 if newer, 2 if v2</returns>
        public static int Compare(Version v1, Version v2)
        {
            int v1Letter = -1;
            int v2Letter = -2;

            /*
             * Needs to determine index in alphabet because GemsCraft versions Alpha, Beta, and "C"
             * will each restart the numbering system.
             */
            for (int x = 0; x <= 25; x++)
            {
                if (v1.Name.First().ToString().ToLower() == Alphabet[x])
                {
                    v1Letter = x;
                }

                if (v2.Name.First().ToString().ToLower() == Alphabet[x])
                {
                    v2Letter = x;
                }
            }

            // Check Names
            if (v1Letter > v2Letter) return 1;
            if (v1Letter < v2Letter) return 2;

            // Check Major
            if (v1.Major > v2.Major) return 1;
            if (v1.Major < v2.Major) return 2;

            // Check Minor
            if (v1.Minor > v2.Minor) return 1;
            if (v1.Minor < v2.Minor) return 2;

            // Check Revision
            if (v1.Revision > v2.Revision) return 1;
            if (v1.Revision < v2.Revision) return 2;

            // Check Build
            if (v1.Build > v2.Build) return 1;
            return v1.Build < v2.Build ? 2 : 0; // Here either v2 is greater or they are the exact same
        }
    }
}
