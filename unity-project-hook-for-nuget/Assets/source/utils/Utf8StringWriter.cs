using System.IO;
using System.Text;

namespace starikcetin.UnityProjectHookForNuget.utils
{
    // necessary for XLinq to save the xml project file in utf8
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
