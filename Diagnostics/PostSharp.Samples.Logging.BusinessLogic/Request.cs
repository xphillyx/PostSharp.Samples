namespace PostSharp.Samples.Logging.BusinessLogic
{
    public class Request
    {
        public int Id;

        public Request(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return "Request Id=" + this.Id;

        }
    }
}