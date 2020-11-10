using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.FoNeke.Common
{
    public static class LogExtensions
    {
        private static readonly Lazy<ConcurrentDictionary<string, ILog>> LogDictionary =
            new Lazy<ConcurrentDictionary<string, ILog>>(() => new ConcurrentDictionary<string, ILog>());

        public static ILog Log<T>(this T type)
        {
            return LogDictionary.Value.GetOrAdd(typeof(T).Name, LogManager.GetLogger(typeof(T)));
        }
    }
}

//public class Log<T>
//{
//    private static ILog _logger;

//    private Log()
//    {
//        _logger = LogManager.GetLogger(typeof(T));
//    }



//    /// <summary>  
//    /// Used to log Debug messages in an explicit Debug Logger  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    public static void Debug(string message)
//    {
//        _logger.Debug(message);
//    }


//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    /// <param name="exception">The exception to log, including its stack trace </param>  
//    public static void Debug(string message, System.Exception exception)
//    {
//        _logger.Debug(message, exception);
//    }


//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    public static void Info(string message)
//    {
//        _logger.Info(message);
//    }


//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    /// <param name="exception">The exception to log, including its stack trace </param>  
//    public static void Info(string message, System.Exception exception)
//    {
//        _logger.Info(message, exception);
//    }

//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    public static void Warn(string message)
//    {
//        _logger.Warn(message);
//    }

//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    /// <param name="exception">The exception to log, including its stack trace </param>  
//    public static void Warn(string message, System.Exception exception)
//    {
//        _logger.Warn(message, exception);
//    }

//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    public static void Error(string message)
//    {
//        _logger.Error(message);
//    }

//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    /// <param name="exception">The exception to log, including its stack trace </param>  
//    public static void Error(string message, System.Exception exception)
//    {
//        _logger.Error(message, exception);
//    }


//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    public static void Fatal(string message)
//    {
//        _logger.Fatal(message);
//    }

//    /// <summary>  
//    ///  
//    /// </summary>  
//    /// <param name="message">The object message to log</param>  
//    /// <param name="exception">The exception to log, including its stack trace </param>  
//    public static void Fatal(string message, System.Exception exception)
//    {
//        _logger.Fatal(message, exception);
//    }


//}
