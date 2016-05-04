namespace Crosscutting.NetFramework.Logging
{
    using System.Diagnostics;
    using Crosscutting.Logging;

    public class Log4NetLogFactory : ILoggerFactory
	{
		public ILogger Create()
		{
			var stack = new StackTrace();
			var frame = stack.GetFrame(1);

			return new Log4NetLog(frame.GetMethod().DeclaringType);
		}
	}
}
