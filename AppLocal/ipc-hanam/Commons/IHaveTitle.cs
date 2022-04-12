using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam
{
    public interface IHaveTitle
    {
        string Title { get; }
        Uri IconSource { get; }
    }
}
