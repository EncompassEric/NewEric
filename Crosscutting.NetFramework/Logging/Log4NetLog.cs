namespace Crosscutting.NetFramework.Logging
{
    using System;
    using Crosscutting.Logging;

    public class Log4NetLog:ILogger
	{
		private readonly log4net.ILog _logger;

		public Log4NetLog(Type type)
		{
			log4net.Config.XmlConfigurator.Configure();
			_logger = log4net.LogManager.GetLogger(type);
		}

		public void Debug(string message, params object[] args)
		{
			_logger.DebugFormat(message, args);
		}

		public void Debug(string message, Exception exception, params object[] args)
		{
			_logger.DebugFormat(message + "," + exception.Message, args);
		}

		public void Debug(object item)
		{
			_logger.Debug(item);
		}

		public void Fatal(string message, params object[] args)
		{
			_logger.FatalFormat(message, args);
		}

		public void Fatal(string message, Exception exception, params object[] args)
		{
			_logger.FatalFormat(message + "," + exception.Message, args);
		}

		public void LogInfo(string message, params object[] args)
		{
			_logger.InfoFormat(message,args);
		}

		public void LogWarning(string message, params object[] args)
		{
			_logger.WarnFormat(message,args);
		}

		public void LogError(string message, params object[] args)
		{
			_logger.ErrorFormat(message,args);
		}

		public void LogError(string message, Exception exception, params object[] args)
		{
			_logger.ErrorFormat(message+","+exception.Message,args);
		}
	}
}
