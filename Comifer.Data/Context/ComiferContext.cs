using Comifer.Data.Service.ChangeTrackerService;
using System;
using System.Linq;
using System.Threading;
using System.Web;

namespace Comifer.Data.Context
{
    public class ComiferContext : ComiferContextLog
    {
        public const string ContextName = "ComiferContext";
        private readonly IChangeTrackerService _changeTrackerService;

        public ComiferContext() : base(ContextName)
        {
            _changeTrackerService = new ChangeTrackerService();
        }
    }
}



