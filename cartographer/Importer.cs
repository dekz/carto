using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cartographer
{
    public interface Importer
    {
        bool ParseMID(string filename);
        bool ParseMIF(string filename);
        bool ParseXLS(string filename);
    }
}
