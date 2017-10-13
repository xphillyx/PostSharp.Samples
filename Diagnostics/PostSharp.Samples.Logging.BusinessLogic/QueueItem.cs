using System.Collections.Generic;
using System.Text;

namespace PostSharp.Samples.Logging.BusinessLogic
{
    public class QueueItem
    {
        public int Id;

        public QueueItem(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return "SyncRequest EntityId=" + this.Id;

        }
    }
}
